using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script manages the game state.
/// It controls the player reset.
/// </summary>

public class GameManager : MonoBehaviour
{
    public GameObject player; // Reference to the player
    
    private Vector3 initialPosition; // this variable stores the player's initial position

    // singleton pattern
    public static GameManager instance;

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

    void Start()
    {
        // store the player's initial position
        initialPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Respawns the player at the last known checkpoint
    /// </summary>
    public void ResetPlayer()
    {
        // reset the player's position
        player.transform.position = initialPosition;
        // set cooldown to false
        player.GetComponent<ShootController>().ResetCooldown();
        // reset movement
        player.GetComponent<PlayerMovement>().ResetMovement();
    }
}
