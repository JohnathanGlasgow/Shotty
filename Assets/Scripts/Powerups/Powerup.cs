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
    public float CooldownAmt = 5.0f; // the cooldown of the powerup
    public float FadeSpeed = 0.01f; // the speed at which the powerup fades in
    protected float cooldown; // the current cooldown of the powerup
    public bool isAvailable = true; // whether the powerup is is available or not

    private Coroutine fadeInCoroutine;
    private bool allFadedIn = false;

    protected SpriteRenderer spriteRenderer;
    protected List<SpriteRenderer> spriteRenderers;
    // List to hold the initial alpha values of the sprites
    protected List<float> initialAlphas;




    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
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
        // color.a = 0;
        // spriteRenderer.color = color;
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

    public void StopFadeIn()
    {
        if (fadeInCoroutine != null)
        {
            StopCoroutine(fadeInCoroutine);
        }
    }

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

        Collider2D playerCollider = Physics2D.OverlapBox(collider.bounds.center, collider.bounds.size, 0, LayerMask.GetMask("Player"));
        if (playerCollider != null)
        {
            // The player is inside the collider
            Debug.Log("Player is inside the collider");
            //StopCoroutine(fadeInCoroutine);
            allFadedIn = true;
            // Deactivate();
        }
        //allFadedIn = true;
    }

    /// <summary>
    /// This method fades in the sprite of the powerup.
    /// </summary>
    /// <returns></returns>
    private IEnumerator fadeIn()
    {
        allFadedIn = false;

        while (!allFadedIn)
        {
            allFadedIn = true;

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