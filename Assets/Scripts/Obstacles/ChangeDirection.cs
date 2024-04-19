using UnityEngine;

/// <summary>
/// This script changes the movement direction of the obstacle when a collision occurs
/// In the editor you can choose the new values for the MovingForward and MovingVertical flags
/// </summary>

public class ChangeMovementOnCollision : MonoBehaviour
{
    public bool NewMovingForward = false; // New value for MovingForward flag
    public bool NewMovingVertical = false; // New value for MovingVertical flag

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object has the MoveStraight script attached
        Obstacle moveStraightScript = other.gameObject.GetComponent<Obstacle>();
        if (moveStraightScript != null)
        {
            // Change the movement flags
            moveStraightScript.ChangeMovementFlags(NewMovingForward, NewMovingVertical);
        }
    }
}
