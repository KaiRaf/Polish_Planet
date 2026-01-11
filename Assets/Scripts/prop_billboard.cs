using System;
using System.Collections;
using System.Collections.Generic;
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
        float angle = Vector3.Angle(transform.position, new Vector3(1,0,0));

        //apply euler lock to camera for x, y, and difference angle for the z
        // transform.rotation = Camera.main.transform.rotation;
        transform.rotation = Quaternion.Euler(Camera.main.transform.rotation.eulerAngles.x, Camera.main.transform.rotation.eulerAngles.y, angle);
        //Debug.Log(transform.position);
    }
}
