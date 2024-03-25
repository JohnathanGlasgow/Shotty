using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <remarks>
/// Generated using help from ChatGPT.
/// Using prompt "unity 2d camera follow"
/// </remarks>

/// <summary>
/// This script allows the camera to smoothly follow the players movement.
/// </summary>

public class CameraFollow : MonoBehaviour
{
    public Transform target;        //Which object should the camera follow (usually the player)
    public float smoothSpeed = 0.05f; //how fast the camera moves towards the target (1 is almost instant, 0 doesn't move at all)
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
