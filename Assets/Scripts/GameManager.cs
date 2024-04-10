/*
 * File: GameManager.cs
 * -------------------------
 * This file contains the implementation of the game state management.
 *
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using UnityEngine;

/// <summary>
/// This script manages the game state.
/// It controls resetting the player and other game objects when the player goes out of bounds or dies.
/// </summary>
public class GameManager : MonoBehaviour
{
    public GameObject player; // Reference to the player
    
    public Vector3 respawnPosition; // this variable stores the player's initial position
    private GameObject[] powerups; // array of game objects that will be reset
    private float initCameraSize; // initial camera size

    #region Singleton
    // singleton pattern
    public static GameManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        // store the player's initial position
        respawnPosition = player.transform.position;
        // initialize the powerups array
        powerups = GameObject.FindGameObjectsWithTag("Powerup");
        // store the initial camera size
        initCameraSize = Camera.main.orthographicSize;
    }

    /// <summary>
    /// Respawns the player at the last known checkpoint
    /// </summary>
    public void ResetPlayer()
    {
        // reset the player's position
        player.transform.position = respawnPosition;
        // set cooldown to false
        player.GetComponent<ShootController>().ResetCooldown();
        // reset movement
        player.GetComponent<PlayerMovement>().ResetMovement();
    }

    /// <summary>
    /// This method resets the powerups.
    /// </summary>
    private void resetPowerups()
    {
        // loop through powerups and reset them if inactive
        foreach (GameObject powerup in powerups)
        {
            // if chronomanager is not null, reset chronomanager
            if (powerup.GetComponent<ChronoPowerup>() != null)
            {
                ChronoManager chronoManager = ChronoManager.instance;
                chronoManager.DeactivateSlomo();
            }
            Powerup powerupComponent = powerup.GetComponent<Powerup>();
            powerupComponent.Reset();
        }
    }

    /// <summary>
    /// This method resets the level.
    /// It is called by the OutOfBounds script.
    /// </summary>
    public void ResetLevel()
    {
        // reset the camera size
        if (Camera.main.orthographicSize != initCameraSize)
        {
            Camera.main.orthographicSize = initCameraSize;
        }
        resetPowerups();
        ResetPlayer();
    }
}
