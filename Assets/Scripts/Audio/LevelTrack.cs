using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script tells the music manager to change the track index when the level is loaded.
/// </summary>
public class LevelTrack : MonoBehaviour
{
    public int trackIndex;
    void Start()
    {
        if(MusicManager.instance != null)
        {
            MusicManager.instance.ChangeTrackIndex(trackIndex);
        }
    }
}
