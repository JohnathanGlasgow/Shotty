// this rotates on z-axis at the speed of the ChronoManager

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronoFan : MonoBehaviour
{
    private float rotationSpeed;
    public bool clockwise = true;

    private void Update()
    {
        rotationSpeed = ChronoManager.instance.obstacleSpeed;
        if (clockwise)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }
    }
}