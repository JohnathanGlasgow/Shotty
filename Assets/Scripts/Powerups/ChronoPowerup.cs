/*
 * File: ChronoPowerup.cs
 * -------------------------
 * This file contains the implementation of the powerup that slows down time.
 *
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using UnityEngine;

/// <summary>
/// This script defines the behaviour of the ChronoPowerup.
/// It slows down the speed of obstacles effected controlled by ChronoManager.
/// It inherits from the Powerup class.
/// </summary>
public class ChronoPowerup : Powerup
{
    private ChronoManager chronoManager;

    /// <summary>
    /// Initialise ChronoManager and set cooldown amount.
    /// </summary>
    private void Start()
    {
        chronoManager = ChronoManager.instance;
        // set cooldown to match the chronoDuration from the ChronoManager
        SetCooldownAmt(chronoManager.chronoDuration + 0.1f);
    }

    /// <summary>
    /// This method defines the effect of the powerup.
    /// It activates slomo in the ChronoManager.
    /// </summary>
    protected override void powerUpEffect(Collider2D other)
    {
        chronoManager.ActivateSlomo();
    }
}