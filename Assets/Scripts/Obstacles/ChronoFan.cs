/*
 * File: ChronoFan.cs
 * -------------------------
 * This file contains the implementation of the ChronoFan obstacle.
 *
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using UnityEngine;

/// <summary>
/// Simple class that rotates the ChronoFan obstacle based on the ChronoManager's obstacle speed.
/// </summary>
public class ChronoFan : MonoBehaviour
{
    public bool clockwise = true;
    private float rotationSpeed;

    /// <summary>
    /// Rotate the obstacle based on the ChronoManager's obstacle speed.
    /// </summary>
    void Update()
    {
        rotationSpeed = ChronoManager.instance.obstacleSpeed;
        if (clockwise)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }
    }
}