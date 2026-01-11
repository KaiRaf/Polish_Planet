using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class prop_billboard : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //find angle between tree and 0,0,0
        float angle = Vector3.Angle(new Vector3(transform.parent.parent.position.x,transform.parent.parent.position.y,0), new Vector3(0,1,0));
        //Debug.Log("current position: " + transform.parent.parent.position + ", angle: " + angle);

        //if x is negative
        float orientationMult = 1f;
        if (transform.position.x < 0)
        {
            orientationMult = 1f;
        } else
        {
            orientationMult = -1f;
        }

        //apply euler lock to camera for x, y, and difference angle for the z
        // transform.rotation = Camera.main.transform.rotation;
        transform.rotation = Quaternion.Euler(Camera.main.transform.rotation.eulerAngles.x, Camera.main.transform.rotation.eulerAngles.y, orientationMult * angle);
        // transform.rotation = Quaternion.Euler(0, 0, orientationMult * angle);
        //Debug.Log(transform.position);
    }
}
