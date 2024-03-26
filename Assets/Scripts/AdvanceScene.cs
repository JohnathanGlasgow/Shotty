/*
 * File: AdvanceScene.cs
 * -------------------------
 * This file contains the implementation of the scene advancement.
 *
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script advances the scene when the player enters the goal.
/// </summary>
public class AdvanceScene : MonoBehaviour
{
    public string NextSceneName; // Name of the next scene to load

    /// <summary>
    /// OnTriggerEnter2D is called when the Collider2D other enters the trigger.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player entered the goal
        if (other.CompareTag("Player"))
        {
            // Load the next scene
            SceneManager.LoadScene(NextSceneName);
        }
    }
}