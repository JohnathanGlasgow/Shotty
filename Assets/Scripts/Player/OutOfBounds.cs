using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script manages collisions with the out of bounds area.
/// </summary>

public class OutOfBounds : MonoBehaviour
{
    private GameManager gameManager; // Reference to the GameManager

    void Start()
    {
        gameManager = GameManager.instance;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.ResetPlayer();
        }
    }
}
