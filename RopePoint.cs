using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePoint : MonoBehaviour
{
    public Transform RopePointOnTheSun;
    public TheSun theSun;


    public void Update()
    {
        if(theSun.sunIsActivaded && theSun.gameObject.transform.position.y >= -5.5f)
            transform.position = RopePointOnTheSun.position;
    }
  

}
