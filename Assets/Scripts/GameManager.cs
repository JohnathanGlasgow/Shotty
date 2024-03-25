using UnityEngine;

/// <summary>
/// This script manages the game state.
/// It controls the player reset.
/// </summary>

public class GameManager : MonoBehaviour
{
    public GameObject player; // Reference to the player
    
    private Vector3 initialPosition; // this variable stores the player's initial position
    private GameObject[] powerups; // array of game objects that will be reset

    #region Singleton
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
    #endregion

    private void Start()
    {
        // store the player's initial position
        initialPosition = player.transform.position;
        // initialize the powerups array
        powerups = GameObject.FindGameObjectsWithTag("Powerup");
    }

    private void resetPlayer()
    {
        // reset the player's position
        player.transform.position = initialPosition;
        // set cooldown to false
        player.GetComponent<ShootController>().ResetCooldown();
        // reset movement
        player.GetComponent<PlayerMovement>().ResetMovement();
    }

    private void resetPowerups()
    {
        // loop through powerups and reset them if inactive
        foreach (GameObject powerup in powerups)
        {
            Powerup powerupComponent = powerup.GetComponent<Powerup>();

            if (powerupComponent.Active)
            {
                powerupComponent.Reset();
            }
        }
    }

    public void ResetLevel()
    {
        resetPowerups();
        resetPlayer();
    }
}
