using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//generated using help from chatgpt
//using prompt "unity 2d camera follow"


public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.05f; // Smoothing factor for camera movement
    public Vector3 offset;
 
    void FixedUpdate() 
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // You can also use LookAt if you want the camera to always look at the target
            // transform.LookAt(target);
        }
    }
}
