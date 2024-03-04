using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private AudioSource audioSource;

    public AudioClip[] audioClips;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    private void onPlaySound(int soundIndex)
    {
        if (soundIndex >= 0 && soundIndex < audioClips.Length)
        {
            audioSource.PlayOneShot(audioClips[soundIndex]);
        }
        else
        {
            Debug.LogWarning("Sound index out of range");
        }
    }

    public static void PlaySound(int soundIndex)
    {
        Instance.onPlaySound(soundIndex);
    }


}
