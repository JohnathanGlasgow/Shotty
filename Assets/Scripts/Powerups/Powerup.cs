/*
 * File: Powerup.cs
 * -------------------------
 * This file contains the implementation of the powerup.
 *
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using System.Collections;
using UnityEngine;

/// <summary>
/// This script is the base class for all powerups.
/// It defines shared powerup behaviour such as collision detection and cooldown.
/// </summary>
public abstract class Powerup : MonoBehaviour
{
    public float Cooldown = 1f; // the time it takes for the powerup to reset
    public float FadeSpeed = 0.01f; // the speed at which the powerup fades in
    protected bool active;
    protected Color color;
    protected SpriteRenderer spriteRenderer;
    


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
        active = true;
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    protected virtual void Update()
    {
        /// if the powerup is not active decrement the Cooldown
        if (active == false)
        {
            Cooldown -= Time.deltaTime;
            // when the Cooldown reaches 0, reset the powerup
            if (Cooldown <= 0)
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
    protected abstract void PowerUpEffect(Collider2D other);

    /// <summary>
    /// This method is called when the powerup collides with another object.
    /// It calls the PowerUpEffect method and begins the deactivation process.
    /// </summary>
    /// <param name="other">The collider of the object that triggered the powerup, likely the player</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PowerUpEffect(other);
            deactivate();
        }
    }

    /// <summary>
    /// This method deactivates the powerup by making it invisible and disabling its collider.
    /// </summary>
    protected void deactivate()
    {
        color.a = 0;
        spriteRenderer.color = color;
        active = false;
        GetComponent<Collider2D>().enabled = false;
    }

    /// <summary>
    /// This method resets the powerup by making it visible and enabling its collider.
    /// </summary>
    public void Reset()
    {
        // fade in the sprite
        StartCoroutine(fadeIn());
        active = true;
        GetComponent<Collider2D>().enabled = true;
    }

    /// <summary>
    /// This method fades in the sprite of the powerup.
    /// </summary>
    /// <returns></returns>
    private IEnumerator fadeIn()
    {
        // while the color is not fully opaque
        while (color.a < 1)
        {
            // increment the alpha value
            color.a += FadeSpeed;
            // set the new color
            spriteRenderer.color = color;
            // wait for a frame
            yield return null;
        }
    }
}