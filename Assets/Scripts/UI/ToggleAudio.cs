using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAudio : MonoBehaviour
{
    public enum AudioType
    {
        SFX,
        Music
    }
    public AudioType audioType;

    public bool onButton;

    void Start()
    {
        if (audioType == AudioType.SFX)
        {
            //This whole settings system is bad but I don't know how to fix it
            if (Settings.Instance != null)
            {
                if (onButton == true)
                {
                    this.gameObject.SetActive(Settings.Instance.muteSFX);
                }
                else
                {
                    this.gameObject.SetActive(!Settings.Instance.muteSFX);
                }
            }
            else
            {
                Debug.LogWarning("Settings.Instance is null");
            }
        }else
        {
            if (Settings.Instance != null)
            {
                if (onButton == true)
                {
                    this.gameObject.SetActive(Settings.Instance.muteMusic);
                }
                else
                {
                    this.gameObject.SetActive(!Settings.Instance.muteMusic);
                }
            }
            else
            {
                Debug.LogWarning("Settings.Instance is null");
            }
        }
    }

    public void ToggleMuteSFX(bool toggle)
    {
        Settings.Instance.muteSFX = toggle;
    }

    public void ToggleMuteMusic(bool toggle)
    {
        Settings.Instance.muteMusic = toggle;
    }
}
