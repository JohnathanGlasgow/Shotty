using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <remarks>
/// Code taken from:
/// https://www.youtube.com/watch?v=Xtfe5S9n4SI
/// </remarks>

/// <summary>
/// 
/// </summary>
public class DontDestroyLoad : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] musicObject = GameObject.FindGameObjectsWithTag("Music");
        if(musicObject.Length > 1)
        {
            Destroy(this.gameObject);
        }

        GameObject[] audioObject = GameObject.FindGameObjectsWithTag("Audio");
        if(audioObject.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
