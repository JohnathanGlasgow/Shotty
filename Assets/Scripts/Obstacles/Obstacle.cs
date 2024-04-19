/*
 * File: Obstacle.cs
 * -------------------------
 * Base class for all obstacles in the game.
 *
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using UnityEngine;

/// <summary>
/// Base class for all obstacles in the game.
/// Allows for movement of the obstacle in a straight line.
/// GameManagers can call the Reset method to reset the obstacle to its initial position.
/// The ChronoManager can call the OnChronoEvent method to change the movement speed of the obstacle.
/// </summary>
public class Obstacle : MonoBehaviour
{
    protected bool started = false; // Flag to track if the obstacle has started moving
    public float MovementSpeed = 2f; // Speed at which the obstacle moves
    public float chronoReductionFactor = 0.5f; // Speed reduction factor when the chrono powerup is active
    public bool MovingForward = true; // Flag to track the direction of movement
    public bool MovingVertical = true; // Flag to track if the obstacle moves vertically
    public Collider2D playerCollider = null; // Collider of the player object
    public Collider2D startOnTrigger = null; // Collider of the trigger that starts the obstacle, if this is null obstacle starts immediately

    private ChronoManager chronoManager;
    private Vector3 initPos;
    private Quaternion initRot;

    /// <summary>
    /// Initialize the obstacle
    /// </summary>
    void Awake()
    {
        initPos = transform.position;
        initRot = transform.rotation;
        // if startoncontact is false, started is true
        started = startOnTrigger == null;
        // get chrono manager
        chronoManager = ChronoManager.instance ?? chronoManager;
        // add listener for chrono event
        chronoManager?.chronoEvent.AddListener(OnChronoEvent);
    }

    /// <summary>
    /// Check if obstacle movement has been triggered by the player
    /// Applies movement to obstacle if is started
    /// </summary>
    void Update()
    {
        // Check if the obstacle should start moving upon contact with the player and if the player has entered the trigger
        if (startOnTrigger != null && !started && startOnTrigger.bounds.Intersects(playerCollider.bounds))
        {
            started = true;
        }

        // Check if the obstacle has started moving and if so, move it
        if (started)
        {
            Move();
        }
    }

    /// <summary>
    /// Move the obstacle in a straight line
    /// If MovingVertical is true, the obstacle moves vertically
    /// </summary>
    protected virtual void Move()
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

    /// <summary>
    /// Reset the obstacle to its initial position
    /// </summary>
    public virtual void Reset()
    {
        transform.position = initPos;
        transform.rotation = initRot;
        started = startOnTrigger == null;
    }

    /// <summary>
    /// Change the movement speed of the obstacle based on the chrono powerup
    /// </summary>
    private void OnChronoEvent(bool isChronoActive)
    {
        if (isChronoActive)
        {
            MovementSpeed *= chronoReductionFactor;
        }
        else
        {
            MovementSpeed /= chronoReductionFactor;
        }
    }
}