using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Movements
    Vector2 movement;
    public float speed;
    public float jumpForce;
    public bool onAir = false;
    public bool bended = false;
    public LayerMask layerMaskGround;
    //Animator
    public Animator playerAnimator;

    //Joystick
    public Joystick myJoystick;

    //Physics
    public Rigidbody2D myRigidbody2D;
    [SerializeField]
    bool attacking = false;
    RaycastHit2D raycastHit;

    public AudioClip[] fxSounds;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        //Defend of Maui
        if (GameManager.Instance.mauiIsProtected)
        {
            transform.GetChild(3).transform.gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(3).transform.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            JumpMaui();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            AttackMaui();
        }

        //Raycast from the transform to the floor
        raycastHit = Physics2D.Raycast(transform.GetChild(0).transform.position, Vector2.down, 0.75f, layerMaskGround);

        //Fixing Velocity "Y" from jumping fast and far
        if(myRigidbody2D.velocity.y >= jumpForce)
        {
            if (onAir)
                myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x , jumpForce - 0.5f);
        }

        if (raycastHit.collider != null)
        {
            onAir = false;
            //Animator from Maui Prototype
            playerAnimator.SetBool("Jump", false);
        }
        else
        {
            onAir = true;
            //Animator from Maui Prototype
            playerAnimator.SetBool("Jump", true);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Setting movement from Joystick
        movement = new Vector2(myJoystick.Horizontal * speed, myJoystick.Vertical);
        WalkMaui(movement);

        
        //if he is falling down gravity is bigger
        if (myRigidbody2D.velocity.y <= 3.5f && onAir)
        {
            myRigidbody2D.gravityScale = 5f;
        }

        //If Maui joystick goes down
        if (movement.y <= -0.55f && !bended)
        {
            playerAnimator.SetTrigger("Bend");
            bended = true;
        }
        if (movement.y >= 0 && bended)
        {
            bended = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals(9))
        {
            myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, 0);
            playerAnimator.SetBool("Jump", false);
        }
    }

    //Get hurts for throws
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Espina"))
        {
            //hurting Maui
            if (!GameManager.Instance.mauiIsProtected)
            {
                playerAnimator.SetTrigger("Damage");
                FindObjectOfType<Camera>().GetComponent<Following>().ShakeCamera();
                GameManager.Instance.TakeOutLifeMaui();
                GameManager.Instance.mauiIsProtected = true;
            }
        }

        //talking with brother pre battle
        if (collision.tag.Equals("Battle"))
        {
            GameManager.Instance.BattlePanel();
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    //Stay and collect items
    private void OnTriggerStay2D(Collider2D collider2D)
    {
        if (bended && collider2D.tag.Equals("Heart"))
        {
            //Collecting heart
            collider2D.GetComponent<Heart>().AddLifeToMaui();
        }

        if (bended && collider2D.tag.Equals("Rope"))
        {
            //Collecting Rope
            collider2D.GetComponent<Rope>().AddRopeToMaui();
        }

        if (attacking)
        {
            if (collider2D.tag.Equals("Sun"))
            {
                collider2D.gameObject.GetComponent<TheSun>().HurtSun();
            }
        }
    }

    //Jump function
    public void JumpMaui()
    {
        if (!onAir)
        {
            myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, jumpForce);
            myRigidbody2D.gravityScale = 1f;
        }
    }

    //Maui Movement Function
    void WalkMaui(Vector2 movement)
    {
        //Animator from Maui Prototype
        if(movement.x != 0)
        {
            playerAnimator.SetBool("move", true);
            playerAnimator.SetFloat("moveX", Mathf.Abs(myRigidbody2D.velocity.x) / 5.5f);
        }
        else
        {
            playerAnimator.SetBool("move", false);
            playerAnimator.SetFloat("moveX", myRigidbody2D.velocity.x);
        }

        //Flip Character Maui
        if (movement.x > 0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else if (movement.x < 0)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }

        // Maui Walking
        myRigidbody2D.velocity = new Vector2(movement.x * Time.deltaTime * 95.5f, myRigidbody2D.velocity.y);
        if (movement.x > 0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else if (movement.x < 0)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }

    }

    //Maui Attack Function
    public void AttackMaui()
    {
        attacking = true;
        playerAnimator.SetTrigger("Attack");
        //attacking = false;
    }

    public void Celebrate()
    {
        movement = Vector2.zero;
        myRigidbody2D.velocity = new Vector2(0,-10);
        playerAnimator.SetTrigger("Win");
    }

    public void PlayJumpSoundFX()
    {
        GetComponent<AudioSource>().clip = fxSounds[0];
        GetComponent<AudioSource>().Play();
    }

    public void PlayFightSoundFX()
    {
        GetComponent<AudioSource>().clip = fxSounds[1];
        GetComponent<AudioSource>().Play();
    }

}
