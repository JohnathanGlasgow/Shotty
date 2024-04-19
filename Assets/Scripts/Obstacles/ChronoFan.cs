/*
 * File: ChronoFan.cs
 * -------------------------
 * This file contains the implementation of the ChronoFan obstacle.
 *
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using UnityEngine;
using System;

/// <summary>
/// Simple class that rotates the ChronoFan obstacle based on the ChronoManager's obstacle speed.
/// </summary>
public class ChronoFan : Obstacle
{
    [SerializeField]
    private bool _clockwise;
    public bool clockwise
    {
        get { return _clockwise; }
        set
        {
            _clockwise = value;
            SetClockwise(_clockwise);
        }
    }

    private void Start()
    {
        SetClockwise(_clockwise);
    }

    private void Update()
    {
        transform.Rotate(0, 0, MovementSpeed * Time.deltaTime);
    }

    private void SetClockwise(bool value)
    {
        MovementSpeed = _clockwise ? -Math.Abs(MovementSpeed) : Math.Abs(MovementSpeed);
    }
}