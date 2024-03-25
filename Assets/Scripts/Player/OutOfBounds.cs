/*
 * File: OutOfBounds.cs
 * -------------------------
 * This file contains the implementation of the out of bounds area.
 *
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using UnityEngine;

/// <summary>
/// This script manages collisions with the out of bounds area.
/// </summary>
public class OutOfBounds : MonoBehaviour
{
    private GameManager gameManager; // Reference to the GameManager

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        gameManager = GameManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.ResetLevel();
        }
    }
}
