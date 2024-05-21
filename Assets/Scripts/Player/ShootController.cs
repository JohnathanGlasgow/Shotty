/*
 * File: ShootController.cs
 * -------------------------
 * This file contains the implementation of the player's shooting mechanism.
 *
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot, Mina Pêcheux (https://www.youtube.com/watch?v=SyA4PPiXorI)
 */

using UnityEngine;

/// <summary>
/// This script triggers the Particle System when the player presses the Fire button.
/// It also manages the cooldown time for the player's shooting mechanism.
/// The player can only shoot when the cooldown is inactive.
/// The player will change color based on the cooldown time.
/// </summary>
public class ShootController : MonoBehaviour
{
    public Color CooldownInactiveColor = Color.red; // Color when the cooldown is inactive
    public Color CooldownActiveColor = Color.white; // Color when the cooldown is active
    public float CooldownTime = 0.5f; // Cooldown time in seconds
    public bool CooldownActive = false; // Is the cooldown active?
    public SpriteRenderer SpriteRenderer; // Reference to the Sprite Renderer
    public ParticleSystem ParticleSystem; // Reference to the Particle System


    public ParticleSystem ReloadParticle;

    private float cooldown = 0.0f; // Current cooldown time

    #region PlayerMovement stuff
    public PlayerMovement PlayerMovement;
    private DefaultPlayerActions playerActions;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        playerActions = new DefaultPlayerActions();
        playerActions.Player.Fire.performed += _ => Fire();
    }

    /// <summary>
    /// This method is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        playerActions.Enable();
    }

    /// <summary>
    /// This method is called when the behaviour becomes disabled.
    /// </summary>
    private void OnDisable()
    {
        playerActions.Disable();
    }
    #endregion

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        SpriteRenderer.color = CooldownInactiveColor;
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        // Check if the cooldown is active and increment it
        if (CooldownActive)
        {
            IncrementCooldown();
        }
    }

    /// <summary>
    /// This method triggers the Particle System and the player's shooting mechanism.
    /// </summary>
    private void Fire()
    {
        if (CooldownActive == false)
        {
            CooldownActive = true;
            cooldown = 0.0f;
            ParticleSystem?.Play();
            PlayerMovement.Shoot();
        }
    }

    /// <summary>
    /// This method increments the cooldown time.
    /// </summary>
    private void IncrementCooldown()
    {
        changeColor();
        cooldown += Time.deltaTime;
        if (cooldown > CooldownTime)
        {
            cooldown = 0.0f;
            AudioManager.PlaySound(2); //reload soundf
            ReloadParticle?.Play();
            CooldownActive = false;
        }
    }

    /// <summary>
    /// This method gradually changes the color of the player sprite from white to red based on the cooldown time.
    /// </summary>
    private void changeColor()
    {
        // Change the color of the sprite
        SpriteRenderer.color = Color.Lerp(CooldownActiveColor, CooldownInactiveColor, cooldown / CooldownTime);
    }

    /// <summary>
    /// This method resets the cooldown time.
    /// </summary>
    public void ResetCooldown()
    {
        CooldownActive = false;
        cooldown = 0.0f;
        SpriteRenderer.color = CooldownInactiveColor;
    }

}
