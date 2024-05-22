using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <remarks>
/// NOTE: This script could probably be deleted and DontDestroyOnLoad could be added to the respective manager scripts.
/// </remarks>

/// <summary>
/// This script is attached to gameobjects that should persist between scenes.
/// Specifically used for timer and audio managers
/// </summary>
public class DontDestroyLoad : MonoBehaviour
{
    private void Awake()
    {
        //All these checks are to ensure that only one instance is created.
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

        GameObject[] timerObject = GameObject.FindGameObjectsWithTag("Timer");
        if(timerObject.Length > 1)
        {
            Destroy(this.gameObject);
        }

        //This line is doing all the work.
        DontDestroyOnLoad(this.gameObject);
    }
}
