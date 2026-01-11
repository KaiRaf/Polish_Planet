using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simple_rotate : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 10f; 

    void Update()
    {
        // Rotate around the Y-axis (Vector3.up) by 'rotateSpeed' degrees per second
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
    
}
