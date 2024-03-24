/*
 * File: RechargePowerup.cs
 * -------------------------
 * This file contains the implementation of the powerup that resets the player's cooldown.
 *
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using UnityEngine;

/// <summary>
/// This script defines the behaviour of the RechargePowerup.
/// It resets the player's shotgun cooldown.
/// It inherits from the Powerup class.
/// </summary>
public class RechargePowerup : Powerup
{
    /// <summary>
    /// This method defines the effect of the powerup.
    /// It resets the player's shotgun cooldown.
    /// </summary>
    protected override void PowerUpEffect(Collider2D other)
    {
        // get the ShootController component from the player
        ShootController shootController = other.gameObject.GetComponent<ShootController>();
        if (shootController != null)
        {
            shootController.ResetCooldown();
        }
    }
}