/*
 * File: GameManager.cs
 * -------------------------
 * This file contains the implementation of the game state management.
 *
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.Collections;


/// <summary>
/// This script manages the game state.
/// It controls resetting the player and other game objects when the player goes out of bounds or dies.
/// </summary>
public class GameManager : MonoBehaviour
{
    public float delayBeforeReset = 0.5f; // delay before resetting the level
    public GameObject player; // Reference to the player
    
    public Vector3 respawnPosition; // this variable stores the player's initial position

    // list of game objects that will be reset
    public GameObject[] toReset;
    public List<Obstacle> obstacles;
    private GameObject[] powerups; // array of game objects that will be reset
    private float initCameraSize; // initial camera size
    private ChronoManager chronoManager;
    private PlayerManager playerManager;

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

        // populate the obstacles list
        obstacles = new List<Obstacle>();
        foreach (GameObject obstacle in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            obstacles.Add(obstacle.GetComponent<Obstacle>());
        }

        chronoManager = ChronoManager.instance ?? null;
        playerManager = PlayerManager.Instance ?? null;
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
    private void ResetPowerups()
    {
        // loop through powerups and reset them if inactive
        foreach (GameObject powerup in powerups)
        {
            // if chronomanager is not null, reset chronomanager
            if (powerup.GetComponent<ChronoPowerup>() != null)
            {
                
                //chronoManager.DeactivateSlomo();
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
        StartCoroutine(ResetDelay());

    }

    private IEnumerator ResetDelay()
    {
        yield return new WaitForSeconds(delayBeforeReset);
                // reset the camera size
        if (Camera.main.orthographicSize != initCameraSize)
        {
            Camera.main.orthographicSize = initCameraSize;
        }
        obstacles.ForEach(obstacle => obstacle.Reset());
        ResetPowerups();
        ResetPlayer();
        chronoManager?.Reset();
        playerManager?.Reset();
    }
}
