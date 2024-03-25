/*
 * File: CrosshairMovement.cs
 * -------------------------
 * This file contains the implementation of the crosshair movement.
 *
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot, MoreBBlakeyyy(https://www.youtube.com/watch?v=-bkmPm_Besk)
 */
 
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This script makes the crosshair follow the mouse position.
/// </summary>
public class CrosshairMovement : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    void Start()
    {
        mainCam = Camera.main;
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
        // Get the current position of the mouse cursor in world coordinates
        mousePos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        // Calculate a Vector3 that points from the current position of the object to the position of the mouse cursor 
        Vector3 rotation = mousePos - transform.position;
        // Calculate the angle of rotation needed to make the object face the mouse cursor
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        // Apply the rotation to the object
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}