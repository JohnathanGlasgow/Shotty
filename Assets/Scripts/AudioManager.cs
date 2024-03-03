using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip shotgunShoot;
    public AudioClip shotgunReload;
    public AudioClip jump;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //this code is bad, refactor later on
    public void ShotgunShoot()
    {
        audioSource.PlayOneShot(shotgunShoot, 0.7F);
    }

    public void ShotgunReload()
    {
        audioSource.PlayOneShot(shotgunReload, 0.7F);
    }

    public void Jump()
    {
        audioSource.PlayOneShot(jump, 1F);
    }
}
