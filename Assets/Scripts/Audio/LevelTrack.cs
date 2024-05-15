using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrack : MonoBehaviour
{
    public int trackIndex;
    // Start is called before the first frame update
    void Start()
    {
        if(MusicManager.instance != null)
        {
            MusicManager.instance.ChangeTrackIndex(trackIndex);
        }
    }
}
