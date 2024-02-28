using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    private DefaultPlayerActions playerActions;

    public ParticleSystem particleSystem; // Reference to the Particle System

    void Awake()
    {
        playerActions = new DefaultPlayerActions();
        playerActions.Player.Fire.performed += _ => Fire();
    }

    void OnEnable()
    {
        playerActions.Enable();
    }

    void OnDisable()
    {
        playerActions.Disable();
    }

    void Fire()
    {
        // Play the Particle System
        particleSystem?.Play();
    }
}
