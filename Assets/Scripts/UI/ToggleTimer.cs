using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTimer : MonoBehaviour
{
    public bool onButton;
    // Start is called before the first frame update
    void Start()
    {
        //This whole settings system is bad but I don't know how to fix it
        if (Settings.Instance != null)
        {
            if (onButton == true)
            {
                this.gameObject.SetActive(Settings.Instance.timerOn);
            }
            else
            {
                this.gameObject.SetActive(!Settings.Instance.timerOn);
            }
        }else{
            Debug.LogWarning("Settings.Instance is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleTimerOn(bool toggle)
    {
        Settings.Instance.timerOn = toggle;
    }
}
