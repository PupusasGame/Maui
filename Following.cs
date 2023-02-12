using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Following : MonoBehaviour
{
    public Animator cameraAnim;
    public float timeToShake;
    public Transform target;

    public Material daySprite;

    private void Start()
    {
        cameraAnim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if(FindObjectOfType<TheSun>().sunIsActivaded == true)
        {
            GetComponent<Skybox>().material = daySprite;
        }

        Vector3 targetPosition = new Vector3(target.transform.position.x, Mathf.Clamp(target.transform.position.y, 2.7f, 6.5f), transform.position.z);
        Vector3 wantedPosition = Vector3.Lerp(transform.position, targetPosition, 1.65f * Time.deltaTime);
        transform.position = wantedPosition;

        if(target.gameObject.GetComponent<Rigidbody2D>().velocity.x > 7)
        {
            transform.position = new Vector3(transform.position.x +.20f, transform.position.y, transform.position.z);
        }

        if (target.gameObject.GetComponent<Rigidbody2D>().velocity.x < -7)
        {
            transform.position = new Vector3(transform.position.x -.20f, transform.position.y, transform.position.z);
        }
    }

   
    public void ShakeCamera()
    {
        cameraAnim.SetTrigger("Shake");
    }
}
