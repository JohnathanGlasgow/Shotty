using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] variations;
    private int currentVariationIndex = 0;
    public int desiredVariationIndex = 0;
    public float trackLength;
    public float sectionLength;
    private float timer = 0;
    private void Start()
    {   
        if (variations.Length == 0) //There are no songs loaded
        {
            return;   
        }

        audioSource.clip = variations[currentVariationIndex];
        audioSource.Play();
        trackLength = audioSource.clip.length;
        sectionLength = trackLength / 16; //Divide track into 16 sections
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= sectionLength && sectionLength != 0)
        {
            Debug.Log("hi");
            timer = 0;
        }
    }

    public void ChangeTrack(int index)
    {
        currentVariationIndex = index;

        float currentPosition = audioSource.time;

        audioSource.clip = variations[currentVariationIndex];
        audioSource.time = currentPosition;
        audioSource.Play();
    }

    
}
