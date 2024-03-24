/*
 * File: Rotate.cs
 * -------------------------
 * This file contains the implementation of the rotating object.
 *
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed = 10f; // the speed of rotation
    public Vector3 axis = Vector3.up; // the axis to rotate around

    // Update is called once per frame
    private void Update()
    {
        // rotate the object around the chosen axis
        transform.Rotate(axis, speed * Time.deltaTime);
    }
}
