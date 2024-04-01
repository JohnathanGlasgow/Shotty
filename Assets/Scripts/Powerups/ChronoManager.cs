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

/// <summary>
/// This script manages the slow-motion effect of the game.
/// It controls the activation and deactivation of the slow-motion effect.
/// Upon activation the obstacle speed is reduced by the speed reduction factor.
/// All the chronoPowerups are deactivated when the slow-motion effect is activated.
/// </summary>
public class ChronoManager : MonoBehaviour
{
    public GameObject player;
    public float obstacleSpeed = 1.0f;  // Speed of the obstacles
    public float chronoDuration = 5.0f; // Duration of the chrono powerup
    public float speedReductionFactor = 0.5f;   // Speed reduction factor when the chrono powerup is active

    private List<ChronoPowerup> chronoPowerups = new List<ChronoPowerup>();
    private bool isSlomoActive = false;
    private float initialSpeed;
    private float slomoEndTime; // Used for tracking when the slow-motion effect should end

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
        chronoPowerups.AddRange(FindObjectsOfType<ChronoPowerup>());
        initialSpeed = obstacleSpeed;
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
    /// Activates the slow-motion effect and deactivates all other chronoPowerups.
    /// </summary>
    public void ActivateSlomo()
    {
        isSlomoActive = true;
        // for all chronoPowerups where isActive is true, deactivate
        foreach (ChronoPowerup cp in chronoPowerups)
        {
            if (cp.isAvailable)
            {
                cp.StopFadeIn();
                cp.Deactivate();
            }
        }
        player.GetComponent<GhostSprites>().isActive = true;
        // Reduce the speed of the obstacles
        obstacleSpeed *= speedReductionFactor;
        // Set the time when the slow-motion effect should end
        slomoEndTime = Time.time + chronoDuration;
    }

    /// <summary>
    /// Deactivates the slow-motion effect.
    /// </summary>
    public void DeactivateSlomo()
    {
        isSlomoActive = false;
        player.GetComponent<GhostSprites>().isActive = false;
        // Restore the speed of the obstacles
        obstacleSpeed = initialSpeed;
    }
}