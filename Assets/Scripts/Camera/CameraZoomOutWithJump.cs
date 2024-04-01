/*
 * File: CameraZoomOutWithJump.cs
 * -------------------------
 * This file contains the implementation of the camera zoom out with jump effect.
 *
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using UnityEngine;

/// <summary>
/// This script zooms out the camera when the player jumps and zooms in when the player falls.
/// </summary>
public class CameraZoomOutWithJump : MonoBehaviour
{
    public Camera mainCamera;
    public Transform player;
    public float zoomOutSpeed = 0.1f;   /// speed at which the camera zooms out
    public float buffer = 1f;   /// player has to be this close to the top edge of the screen to trigger zoom out
    public float maxCameraSize = 50f;   /// maximum camera size to zoom out to
    public float minFallSpeedBeforeZoomIn = 1f;   /// wait until the player is falling at this speed before zooming in

    private float initialCameraSize;
    private Rigidbody2D playerRb;
    private float previousPlayerY; 

    /// <summary>
    /// Initialise values
    /// </summary>
    private void Start()
    {
        initialCameraSize = mainCamera.orthographicSize;
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Update the camera size based on player position
    /// </summary>
    private void Update()
    {
        // get the player's position
        Vector3 playerPos = player.position;

        // check if the player is moving up and within the buffer area
        if (playerRb.velocity.y > 0 && playerPos.y > mainCamera.transform.position.y + mainCamera.orthographicSize - buffer)
        {
            // increase the camera size by the zoom out speed
            mainCamera.orthographicSize += zoomOutSpeed;

            // check if the camera size is greater than the max size
            if (mainCamera.orthographicSize > maxCameraSize)
            {
                // set the camera size to the max size
                mainCamera.orthographicSize = maxCameraSize;
            }
        }
        // check if the player is falling and falling faster than the minimum speed
        else if (playerRb.velocity.y < -minFallSpeedBeforeZoomIn)
        {
            // decrease the camera size by change in player y position
            mainCamera.orthographicSize -= previousPlayerY - playerPos.y;

            // check if the camera size is less than the initial size
            if (mainCamera.orthographicSize < initialCameraSize)
            {
                // set the camera size to the initial size
                mainCamera.orthographicSize = initialCameraSize;
            }
        }

        // store the player's y position
        previousPlayerY = playerPos.y;
    }
}