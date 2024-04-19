/*
 * File: WaveArray.cs
 * -------------------------
 * Apply a wave function to an array of objects to create a wave effect.
 *
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enum to define the shape of the wave.
/// </summary>
public enum WaveShape
{
    Sine,
    Cosine,
    Triangle,
    Square,
    Sawtooth,
    Noise
}

/// <summary>
/// Apply a wave function to an array of objects to create a wave effect.
/// Useful for creating dynamic obstacles.
/// Inherits from Obstacle, so is affected by the ChronoManager and reset by the GameManager.
/// Speed is set by MovementSpeed from the parent class.
/// </summary>
public class WaveArray : Obstacle
{
    public WaveShape waveShape = WaveShape.Sine; // shape of the wave
    public List<GameObject> objects; // array of Objects
    public float waveFrequency = 1.0f; // frequency of the wave
    public float waveAmplitude = 1.0f; // amplitude of the wave
    public bool moveVertically = true; // flag to move vertically or horizontally
    public float phaseShift = 0.0f; // phase shift of the wave
    private float movementTime = 0.0f;

    private Vector3[] startPositions; // initial positions of the Objects
    
    // this dictionary stores the wave functions as values and their corresponding enum values as keys
    private Dictionary<WaveShape, Func<float, float>> waveFunctions = new Dictionary<WaveShape, Func<float, float>>()
    {
        { WaveShape.Sine, (float x) => Mathf.Sin(x) },
        { WaveShape.Cosine, (float x) => Mathf.Cos(x) },
        { WaveShape.Square, (float x) => Mathf.Sign(Mathf.Sin(x)) },
        { WaveShape.Triangle, (float x) => Mathf.PingPong(x / Mathf.PI, 1) * 2 - 1 },
        { WaveShape.Sawtooth, (float x) => Mathf.Repeat(x / (2 * Mathf.PI), 1) },
        { WaveShape.Noise, (float x) => Mathf.PerlinNoise(x, 0) * 4 - 2 }
    };

    /// <summary>
    /// Initialize the wave array.
    /// </summary>
    void Start()
    {
        startPositions = new Vector3[objects.Count]; // initialize the startPositions array
        for (int i = 0; i < objects.Count; i++)
        {
            startPositions[i] = objects[i].transform.localPosition; // store the initial positions of the Objects
        }
    }

    /// <summary>
    /// Update the position of the objects based on the wave function.
    /// </summary>
    void Update()
    {
        movementTime += MovementSpeed * Time.deltaTime; // get the current time
        for (int i = 0; i < objects.Count; i++)
        {
            // get the wave function based on the wave shape
            Func<float, float> waveFunction = waveFunctions[waveShape];
            // calculate the current distance based on the wave function
            // each item in the array is offset by a different phase
            float currentDistance = waveAmplitude * waveFunction(movementTime + phaseShift + (i * Mathf.PI * 2 * waveFrequency / objects.Count));
            // set the position of the object based on the wave
            if (moveVertically)
            {
                objects[i].transform.localPosition = new Vector3(objects[i].transform.localPosition.x, startPositions[i].y + currentDistance, objects[i].transform.localPosition.z);
            }
            else
            {
                objects[i].transform.localPosition = new Vector3(startPositions[i].x + currentDistance, objects[i].transform.localPosition.y, objects[i].transform.localPosition.z);
            }
        }

    }

    /// <summary>
    /// Reset the wave array to its initial state.
    /// </summary>
    public override void Reset()
    {
        base.Reset();
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].transform.localPosition = startPositions[i];
        }
        movementTime = 0.0f;
    }
}