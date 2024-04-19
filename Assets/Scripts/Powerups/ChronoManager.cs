/*
 * File: ChronoManager.cs
 * -------------------------
 * This file contains the implementation of the ChronoManager.
 *
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This script manages the slow-motion effect of the game.
/// It controls the activation and deactivation of the slow-motion effect.
/// Upon activation the obstacle speed is reduced by the speed reduction factor.
/// All the chronoPowerups are deactivated when the slow-motion effect is activated.
/// </summary>
public class ChronoManager : MonoBehaviour
{
    public GameObject player;
    public float chronoDuration = 5.0f; // Duration of the chrono powerup
    public UnityEvent<bool> chronoEvent; // Event to trigger when the chrono powerup is activated
    private bool isSlomoActive = false;
    private float slomoEndTime; // Used for tracking when the slow-motion effect should end
    private GhostSprites ghostSprites;

    #region Singleton
    // Singleton instance
    public static ChronoManager instance;

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
    /// Initialise the list of chronoPowerups and store the initial speed of the obstacles.
    /// </summary>
    void Start()
    {
        ghostSprites = player.GetComponent<GhostSprites>();
    }

    /// <summary>
    /// Check if the slow-motion effect is active and if it should be deactivated.
    /// </summary>
    void Update()
    {
        // If the slow-motion effect is active and the current time is past the end time
        if (isSlomoActive && Time.time >= slomoEndTime)
        {
            DeactivateSlomo();
        }
    }

    /// <summary>
    /// Activates the slow-motion effect
    /// </summary>
    public void ActivateSlomo()
    {
        // if the slow-motion effect is not already active
        if (!isSlomoActive)
        {
            isSlomoActive = true;
            ghostSprites.isActive = true;
            chronoEvent.Invoke(true);
        }
        // Set the time when the slow-motion effect should end
        slomoEndTime = Time.time + chronoDuration;
    }

    /// <summary>
    /// Deactivates the slow-motion effect.
    /// </summary>
    public void DeactivateSlomo()
    {
        isSlomoActive = false;
        ghostSprites.isActive = false;
        chronoEvent.Invoke(false);
    }

    public void Reset()
    {
        if (isSlomoActive == false) { return; }
        
        DeactivateSlomo();
    }
}