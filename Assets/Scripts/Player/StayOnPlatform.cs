/*
 * File: StayOnPlatform.cs
 * -------------------------
 * This file contains the implementation of the moving platform interaction.
 * The player becomes a child of the moving platform when colliding with it.
 *
 * Author: Johnathan
 */


using UnityEngine;

/// <summary>
/// This class handles the interaction between the player and moving platforms.
/// </summary>
public class StayOnPlatform : MonoBehaviour
{
    // Add your variables here...

    /// <summary>
    /// This method is called when the player collides with another object.
    /// If the other object is a moving platform, the player becomes a child of the platform.
    /// </summary>
    /// <param name="other">The other object that the player collided with.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MovingPlatform"))
        {
            this.transform.parent = other.transform;
        }
    }

    /// <summary>
    /// This method is called when the player stops colliding with another object.
    /// If the other object is a moving platform, the player stops being a child of the platform.
    /// </summary>
    /// <param name="other">The other object that the player stopped colliding with.</param>
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MovingPlatform"))
        {
            this.transform.parent = null;
        }
    }
}