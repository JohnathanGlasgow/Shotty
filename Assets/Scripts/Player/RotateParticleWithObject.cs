/*
 * File: RotateParticleWithObject.cs
 * -------------------------
 * This file contains the implementation of the particle system rotation with a target object.
 *
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using UnityEngine;

/// <summary>
/// This script makes the Particle System rotate with a target object.
/// This is used for the shotgun particles to rotate with the crosshair.
/// </summary>
public class RotateParticleWithObject : MonoBehaviour
{
    public Transform TargetObject; // The object whose rotation you want to match
    public Vector3 RotationOffset; // Offset to apply to the rotation

    private ParticleSystem particleSystem; // Reference to the Particle System

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        // Get the Particle System component
        particleSystem = GetComponent<ParticleSystem>();
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        // Check if the target object is not null and the Particle System is valid
        if (TargetObject != null && particleSystem != null)
        {
            // Match the rotation of the Particle System with the rotation of the target object
            Quaternion targetRotation = TargetObject.rotation * Quaternion.Euler(RotationOffset);
            transform.rotation = targetRotation;
        }
    }
}