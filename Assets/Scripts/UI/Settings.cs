using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public bool muteSFX = false;
    public bool muteMusic = false;
    public bool timerOn = false;

    public static Settings Instance;
    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        if (FindObjectOfType<Settings>() != null)
        {
            DontDestroyOnLoad(this.gameObject);
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
