/*
 * File: PlayerManager.cs
 * -------------------------
 * This file contains the implementation of the player manager, which manages the key and door, and player death.
 *
 * Author: Liam and Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using UnityEngine;

/// <summary>
/// This script manages the player, key, and door interactions.
/// It also handles the player's death, and the particle effect that plays when the player dies.
/// </summary>
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public GameObject door;
    public GameObject ogKey;
    public bool hasKey = false;
    public ParticleSystem deathParticles;
    private ParticleSystem.MainModule main;
    public GameObject player;
    public GameObject keyFollowPrefab;
    private GameObject key;
    private Rigidbody2D playerRb;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        playerRb = player.GetComponent<Rigidbody2D>();
        main = deathParticles.main;
        // set particles to player color (the cooldown inactive color)
        main.startColor = player.GetComponent<ShootController>().CooldownInactiveColor;
        var particleRenderer = deathParticles.GetComponent<ParticleSystemRenderer>();
        // set particles to same draw order as player
        particleRenderer.sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder;
        deathParticles.transform.parent = null;
    }

    //This method is called when the player touches another trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ogKey == null) { return; }
        if (other.gameObject.CompareTag("Key"))
        {
            hasKey = true;

            key = Instantiate(keyFollowPrefab, other.gameObject.transform.position, Quaternion.identity);
            key.GetComponent<CameraFollow>().target = gameObject.transform;
            // disable the game object
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Door"))
        {
            if (hasKey == true)
            {
                hasKey = false;
                Destroy(key);
                // disable the game object
                other.gameObject.SetActive(false);

            }
        }
    }

    /// <summary>
    /// Reset the player manager, resets the player, key, door, and key
    /// </summary>
    public void Reset()
    {
        player.SetActive(true);
        if (ogKey == null) { return; }
        hasKey = false;
        ogKey.SetActive(true);
        door.SetActive(true);
        Destroy(key);
    }

    /// <summary>
    /// This method is called when the player dies
    /// It triggers a cool particle effect
    /// </summary>
    public void Die()
    {    
        // set partcles to player position
        deathParticles.transform.position = player.transform.position;
        // particles fire opposite player momentum (looks good most of the time)
        deathParticles.transform.rotation = Quaternion.LookRotation(-playerRb.velocity);
        // speed matches player speed
        main.startSpeed = playerRb.velocity.magnitude;
        player.SetActive(false);
        deathParticles.Play();
    }
}
