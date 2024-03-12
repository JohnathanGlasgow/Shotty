using UnityEngine;

/// <summary>
/// This script moves the obstacle in a straight line
/// and changes the movement direction when the ChangeMovementOnCollision script is triggered
/// It can move either vertically or horizontally   
/// </summary>

public class MoveStraight : MonoBehaviour
{
    public float MovementSpeed = 2f; // Speed at which the obstacle moves
    
    public bool MovingForward = true; // Flag to track the direction of movement

    public bool MovingVertical = true; // Flag to track if the obstacle moves vertically
    
    void Update()
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
