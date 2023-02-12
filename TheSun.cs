using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TheSun : MonoBehaviour
{
    public bool sunIsActivaded = false;
    public Slider sliderLife;

    public GameObject theRopesWraps;
    public GameObject[] eyes;
    public Transform target;
    bool hurtbymaui = false;

    public GameObject projectile;
    public Transform spawnPoint;
    float timeToShoot = 15f;

    private void Start()
    {
        target = FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        if (sunIsActivaded)
        {

            //Create the collider to limited the fight area
            GameObject.Find("End").gameObject.GetComponent<Collider2D>().isTrigger = false;

            //If has the high to be tided
            if (transform.position.y >= -5.25f)
                theRopesWraps.SetActive(true);

            //rising
            if (transform.position.y <= 0)
            {
                transform.position = transform.position + Vector3.up * Time.deltaTime;
            }
            else if( transform.position.y <= 3.5f)
            {
                transform.position = transform.position + Vector3.up * Time.deltaTime * 0.1f;
            }
            else
            {
                transform.position = transform.position;
            }

            //Shooting Projectile
            timeToShoot -= Time.deltaTime;
            if (timeToShoot <= 0)
            {
                ShootProjectile();
                if (sliderLife.value <= .65f)
                {
                    timeToShoot = 3.5f;
                }
                else if (sliderLife.value <= .35f)
                {
                    timeToShoot = 2;
                }
                else
                {
                    timeToShoot = 5;
                }
            }
 
        }

        //sun life is almost empty
        if (sliderLife.value <= 0.05f)
        {
            sunIsActivaded = false;
            target.gameObject.GetComponent<Player>().Celebrate();
            GameManager.Instance.WinTheBattle();
            StartCoroutine(OpenGameOverPanelPostBattle());
        }

        //moving eyes
        eyes[0].transform.position = Vector2.Lerp(new Vector2(eyes[0].transform.position.x, eyes[0].transform.position.y), new Vector2(target.position.x, eyes[0].transform.position.y), 0.05f);
        eyes[0].transform.position = new Vector2(Mathf.Clamp(eyes[0].transform.position.x, eyes[0].transform.root.position.x - 2.5f, eyes[0].transform.root.position.x - 1.5f), eyes[0].transform.position.y);

        eyes[1].transform.position = Vector2.Lerp(new Vector2(eyes[1].transform.position.x, eyes[1].transform.position.y), new Vector2(target.position.x, eyes[1].transform.position.y), 0.05f);
        eyes[1].transform.position = new Vector2(Mathf.Clamp(eyes[1].transform.position.x, eyes[1].transform.root.position.x + 1.5f, eyes[1].transform.root.position.x  + 2.5f), eyes[1].transform.position.y);
    }

    public void HurtSun()
    {
        if (hurtbymaui)
        {
            GetComponent<Animator>().SetTrigger("Hurt");
            sliderLife.value -= 0.005f;
            hurtbymaui = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Attack"))
        {
            hurtbymaui = true;
        }
    }

    public void ShootProjectile()
    {
        Instantiate(projectile, spawnPoint.position, Quaternion.identity);
    }

    public IEnumerator OpenGameOverPanelPostBattle()
    {
        yield return new WaitForSeconds(3.5f);
        GameManager.Instance.GoToMenuPanel();

    }

    public void DeactivateSunForAWhile()
    {
        StartCoroutine(NotActivateForAWhile());
    }

    public IEnumerator NotActivateForAWhile()
    {
        if (sunIsActivaded)
        {
            sunIsActivaded = false;
            yield return new WaitForSeconds(3f);
            sunIsActivaded = true;
        }
    }
}
