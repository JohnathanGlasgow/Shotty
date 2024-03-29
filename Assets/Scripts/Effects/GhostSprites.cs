/*
 * File: GhostSprites.cs
 * -------------------------
 * This file contains the implementation of the ghost sprites effect.
 *
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSprites : MonoBehaviour
{
    public int numberOfGhosts = 5;  // Number of ghost sprites to create
    public float timeBetweenGhosts = 0.1f;  // Time between creating ghost sprites
    public float ghostLifetime = 0.5f; // This is the global ghost lifetime
    public float ghostScale = 1.0f; // This is the global ghost scale
    public Color ghostColor = Color.white;  // This is the global ghost color
    public float initialTransparency = 0.5f; // Initial transparency of the ghost sprite
    public bool sameColor = true; // If true, the ghost sprite will have the same color as the main sprite
    public bool isActive = true; // If true, the ghost effect is active
    public int poolSize = 10; // The size of the ghost pool, determines how many ghost sprites can be created at once
    private Queue<SpriteRenderer> ghostPool = new Queue<SpriteRenderer>(); // Queue to hold the ghost sprites in the pool


    private SpriteRenderer spriteRenderer; // Reference to the main sprite renderer
    private List<SpriteRenderer> ghostSprites = new List<SpriteRenderer>(); // List to hold all the ghost sprites


    /// <summary>
    /// Set up the object pool and start creating ghost sprites.
    /// </summary>
    void Start()
    {
        // Initialize the sprite renderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Initialize the ghost pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject ghost = new GameObject("Ghost");
            SpriteRenderer ghostSpriteRenderer = ghost.AddComponent<SpriteRenderer>();
            ghostSpriteRenderer.sprite = spriteRenderer.sprite;
            ghostSpriteRenderer.color = ghostColor;
            ghostSpriteRenderer.sortingLayerID = spriteRenderer.sortingLayerID;
            ghostSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder;
            ghostSpriteRenderer.transform.localScale = Vector3.one * ghostScale;
            ghost.SetActive(false); // Initially, the ghost is not active
            ghostPool.Enqueue(ghostSpriteRenderer);
        }

        StartCoroutine(CreateGhosts());
    }

    /// <summary>
    /// Create ghost sprites at regular intervals.
    /// </summary>
    IEnumerator CreateGhosts()
    {
        while (isActive)
        {
            // Check if there are any ghost sprites available in the pool
            if (ghostPool.Count > 0)
            {
                // Take a ghost sprite from the pool
                SpriteRenderer ghostSpriteRenderer = ghostPool.Dequeue();
                // Make the ghost sprite active in the scene
                ghostSpriteRenderer.gameObject.SetActive(true);
                // If sameColor is true, set the color of the ghost sprite to the color of the main sprite
                if (sameColor)
                {
                    ghostColor = spriteRenderer.color;
                }
                // Set the initial transparency of the ghost sprite
                ghostColor.a = initialTransparency;
                // Apply the color (with the set transparency) to the ghost sprite
                ghostSpriteRenderer.color = ghostColor;
                // Set the position of the ghost sprite to the position of the main sprite
                ghostSpriteRenderer.transform.position = transform.position;
                // Set the rotation of the ghost sprite to the rotation of the main sprite
                ghostSpriteRenderer.transform.rotation = transform.rotation;
                // Start the coroutine that fades the ghost sprite over time
                StartCoroutine(FadeGhost(ghostSpriteRenderer, ghostLifetime));
            }

            // Wait for the specified time between creating ghost sprites
            yield return new WaitForSeconds(timeBetweenGhosts);
        }
    }

    /// <summary>
    /// Fade the ghost sprite over time.
    /// </summary>
    IEnumerator FadeGhost(SpriteRenderer ghostSpriteRenderer, float lifetime)
    {
        // Save the initial lifetime of the ghost sprite
        float startLifetime = lifetime;
        // Save the initial transparency of the ghost sprite
        float startAlpha = ghostSpriteRenderer.color.a;
        // This loop will continue as long as the ghost sprite's lifetime is greater than 0
        while (lifetime > 0)
        {
            // Decrease the lifetime of the ghost sprite by the time passed since the last frame
            lifetime -= Time.deltaTime;
            // Get the current color of the ghost sprite
            Color color = ghostSpriteRenderer.color;
            // Set the transparency of the ghost sprite's color based on its remaining lifetime
            color.a = startAlpha * (lifetime / startLifetime);
            // Apply the color (with the updated transparency) to the ghost sprite
            ghostSpriteRenderer.color = color;
            // Yield control back to Unity until the next frame
            yield return null;
        }

        // Make the ghost sprite inactive in the scene
        ghostSpriteRenderer.gameObject.SetActive(false);
        // Return the ghost sprite to the pool
        ghostPool.Enqueue(ghostSpriteRenderer);
    }
}