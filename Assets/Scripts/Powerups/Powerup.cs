/*
 * File: Powerup.cs
 * -------------------------
 * This file contains the implementation of the powerup.
 *
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is the base class for all powerups.
/// It defines shared powerup behaviour such as collision detection and cooldown.
/// </summary>
public abstract class Powerup : MonoBehaviour
{
    public float CooldownAmt = 5.0f;    // the cooldown of the powerup
    public float FadeSpeed = 0.01f; // the speed at which the powerup fades in
    public bool isAvailable = true; // whether the powerup is is available or not

    public ParticleSystem breakParticles;

    protected float cooldown;   // the current cooldown of the powerup
    protected Coroutine fadeInCoroutine;
    protected bool allFadedIn = false;
    protected SpriteRenderer spriteRenderer;
    protected List<SpriteRenderer> spriteRenderers;
    protected List<float> initialAlphas;        // List to hold the initial alpha values of the sprites

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    /// <remarks>
    /// Frankly the control flow of this class has gotten out a bit out of hand.
    /// This is mainly due to complications with the fade in coroutine, which has to simultaneously fade in multiple sprites,
    /// and the need to check if the player is in the trigger area on reset.
    /// </remarks>
    protected virtual void Awake()
    {
        // Populate the list with all SpriteRenderers in this object and its children
        spriteRenderers = new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>());

        // Initialize the list of initial alpha values and populate it with the current alpha values of the sprites
        initialAlphas = new List<float>();
        foreach (SpriteRenderer sr in spriteRenderers)
        {
            initialAlphas.Add(sr.color.a);
        }
        cooldown = CooldownAmt;
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    protected virtual void Update()
    {
        /// if the powerup is not available decrement the Cooldown
        if (isAvailable == false)
        {
            cooldown -= Time.deltaTime;
            // when the Cooldown reaches 0, reset the powerup
            if (cooldown <= 0)
            {
                Reset();
            }
        }
    }

    /// <summary>
    /// This method defines the effect of the powerup.
    /// It must be defined in the derived class, ie reset Cooldown, increase speed, etc.
    /// </summary>
    /// <param name="other">The collider of the object that triggered the powerup, likely the player</param>
    protected abstract void powerUpEffect(Collider2D other);

    /// <summary>
    /// This method is called when the powerup collides with another object.
    /// It calls the PowerUpEffect method and begins the deactivation process.
    /// </summary>
    /// <param name="other">The collider of the object that triggered the powerup, likely the player</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            breakParticles?.Play();
            StopFadeIn();
            powerUpEffect(other);
            Deactivate();
        }
    }

    /// <summary>
    /// This method Deactivates the powerup by making it invisible and disabling its collider.
    /// </summary>
    public void Deactivate()
    {
        isAvailable = false;
        GetComponent<Collider2D>().enabled = false;
        // Make all sprites invisible
        foreach (SpriteRenderer sr in spriteRenderers)
        {
            Color color = sr.color;
            color.a = 0;
            sr.color = color;
        }
    }

    /// <summary>
    /// This method stops the fade in coroutine.
    /// </summary>
    public void StopFadeIn()
    {
        if (fadeInCoroutine != null)
        {
            StopCoroutine(fadeInCoroutine);
        }
    }

    /// <summary>
    /// Setter for the CooldownAmt variable.
    /// There's a reason this isn't a property but I forgot what it was.
    /// </summary>
    public void SetCooldownAmt(float value)
    {
        CooldownAmt = value;
        cooldown = value;
    }

    /// <summary>
    /// This method resets the powerup by making it visible and enabling its collider.
    /// </summary>
    public void Reset()
    {
        // fade in the sprite
        fadeInCoroutine = StartCoroutine(fadeIn());
        isAvailable = true;
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = true;
        cooldown = CooldownAmt;
        // check if the player is in the trigger area on reset
        Collider2D playerCollider = Physics2D.OverlapBox(collider.bounds.center, collider.bounds.size, 0, LayerMask.GetMask("Player"));
        // if (playerCollider != null)
        // {
        //     allFadedIn = true;
        // }
    }

    /// <summary>
    /// This method fades in the sprites of the powerup.
    /// </summary>
    private IEnumerator fadeIn()
    {
        allFadedIn = false;

        while (!allFadedIn)
        {
            allFadedIn = true;

            // loop through all the sprite renderers and fade them in until they reach their initial alpha values
            // when they all fade in this ends the while loop
            for (int i = 0; i < spriteRenderers.Count; i++)
            {
                SpriteRenderer sr = spriteRenderers[i];
                Color color = sr.color;

                if (color.a < initialAlphas[i])
                {
                    // increment the alpha value
                    color.a += FadeSpeed;
                    sr.color = color;

                    if (color.a < initialAlphas[i])
                    {
                        allFadedIn = false;
                    }
                }
            }

            // wait for a frame
            yield return null;
        }
    }
}