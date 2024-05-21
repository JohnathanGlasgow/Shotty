using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <remarks>
/// NOTE: an audio source component is not require as one will be created at runtime.
/// </remarks>
 
/// <summary>
/// This script will play a sound effect when the player collides with the trigger this is attached to.
/// </summary>
public class ObjectSoundEffect : MonoBehaviour
{
    public AudioClip soundEffect; ///Make sure to assign this variable in the editor  
    public float audioVolume = 0.5f;
    private AudioSource audioSource;

    private void Start()
    {
        /*
        //sets audioSource to the audiosource component of the GameObject this script is attached to.
        //if there is no audioSource object, one is created.
        audioSource = audioSource ? gameObject.GetComponent<AudioSource>() : gameObject.AddComponent<AudioSource>();*/

        audioSource = AudioManager.Instance.audioSource; //Originally I created audiosource at runtime, but this didn't work with the mute SFX system

        audioSource.clip = soundEffect; //The audio source is set to the soundEffect variable. NOTE: don't set audioClip on the AudioSource in editor
        audioSource.volume = audioVolume;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Player") && audioSource) //Checks if the collided object is the player, and if the audioSource variable is not null
        {
            audioSource.Play();
        }
    }
}
