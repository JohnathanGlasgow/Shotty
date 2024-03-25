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
    
    private Vector3 initialPosition; // this variable stores the player's initial position
    private GameObject[] powerups; // array of game objects that will be reset

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
        initialPosition = player.transform.position;
        // initialize the powerups array
        powerups = GameObject.FindGameObjectsWithTag("Powerup");
    }

    /// <summary>
    /// This method resets the player's position and cooldown.
    /// </summary>
    private void resetPlayer()
    {
        // reset the player's position
        player.transform.position = initialPosition;
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
            Powerup powerupComponent = powerup.GetComponent<Powerup>();

            if (powerupComponent.Active)
            {
                powerupComponent.Reset();
            }
        }
    }

    /// <summary>
    /// This method resets the level.
    /// It is called by the OutOfBounds script.
    /// </summary>
    public void ResetLevel()
    {
        resetPowerups();
        resetPlayer();
    }
}
