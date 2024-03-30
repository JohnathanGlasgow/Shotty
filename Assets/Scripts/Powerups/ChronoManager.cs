// this script manages obstacles effected by the chrono powerup
// it has a method to slow down the speed of obstacles for a set amount of time
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChronoManager : MonoBehaviour
{
    // get the player object
    public GameObject player;
    // Global speed variable
    public float obstacleSpeed = 1.0f;

    // Duration of the chrono powerup
    public float chronoDuration = 5.0f;

    // Speed reduction factor when the chrono powerup is active
    public float speedReductionFactor = 0.5f;

    private List<ChronoPowerup> chronoPowerups = new List<ChronoPowerup>();
    private bool isSlomoActive = false;
    private float initialSpeed;

    // Singleton instance
    public static ChronoManager instance;

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

    // on start initialise the chronoPowerups list
    private void Start()
    {
        chronoPowerups.AddRange(FindObjectsOfType<ChronoPowerup>());
        initialSpeed = obstacleSpeed;
    }

    private float slomoEndTime;

    public void ActivateSlomo()
    {
        isSlomoActive = true;
        // for all chronoPowerups where isActive is true, deactivate
        foreach (ChronoPowerup cp in chronoPowerups)
        {
            if (cp.isAvailable)
            {
                cp.StopFadeIn();
                cp.Deactivate();
            }
        }
        player.GetComponent<GhostSprites>().isActive = true;
        // Reduce the speed of the obstacles
        obstacleSpeed *= speedReductionFactor;
        // Set the time when the slow-motion effect should end
        slomoEndTime = Time.time + chronoDuration;
    }

    public void DeactivateSlomo()
    {
        isSlomoActive = false;
        player.GetComponent<GhostSprites>().isActive = false;
        // Restore the speed of the obstacles
        obstacleSpeed = initialSpeed;
    }

    private void Update()
    {
        // If the slow-motion effect is active and the current time is past the end time
        if (isSlomoActive && Time.time >= slomoEndTime)
        {
            DeactivateSlomo();
        }
    }
}