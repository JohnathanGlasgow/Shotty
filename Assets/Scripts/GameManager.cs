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
    // this variable stores the player's initial position
    private Vector3 initialPosition;

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
