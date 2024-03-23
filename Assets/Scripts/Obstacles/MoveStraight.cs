/*
 * File: MoveStraight.cs
 * -------------------------
 * This file contains the implementation of the obstacle movement.
 * There are a few options to move the obstacle: vertically, horizontally, or start on contact with the player.
 *
 * Author: Johnathan
 */

using UnityEngine;

/// <summary>
/// This script moves the obstacle in a straight line
/// and changes the movement direction when the ChangeMovementOnCollision script is triggered
/// It can move either vertically or horizontally   
/// There is also an option to start the movement upon contact with the player 
/// </summary>
public class MoveStraight : MonoBehaviour
{
    public float MovementSpeed = 2f; // Speed at which the obstacle moves

    public bool MovingForward = true; // Flag to track the direction of movement

    public bool MovingVertical = true; // Flag to track if the obstacle moves vertically

    public bool StartOnContact = false; // Flag to track if the obstacle starts moving upon contact with the player

    private bool started = false; // Flag to track if the obstacle has started moving

    void Start()
    {
        // if startoncontact is false, started is true
        if (!StartOnContact)
        {
            started = true;
        }
    }

    void Update()
    {
        // Check if the obstacle should start moving upon contact with the player
        if (StartOnContact && !started)
        {
            // Check if the player has collided with the object this script is attached to
            // so check if the player has collided with the obstacle
            if (GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
            {
                started = true;
            }
        }

        // Check if the obstacle has started moving and if so, move it
        if (started)
        {
            Move();
        }
    }

    void Move()
    {
        // Calculate the next position based on the movement direction and speed
        Vector3 nextPosition = transform.position + (MovingVertical ? Vector3.up : Vector3.right) * (MovingForward ? MovementSpeed : -MovementSpeed) * Time.deltaTime;

        // Move the obstacle to the next position
        transform.position = nextPosition;
    }

    /// <summary>
    /// Change the movement flags
    /// This method is called by the ChangeMovementOnCollision script
    /// </summary>
    public void ChangeMovementFlags(bool newMovingForward, bool newMovingVertical)
    {
        MovingForward = newMovingForward;
        MovingVertical = newMovingVertical;
    }
}
