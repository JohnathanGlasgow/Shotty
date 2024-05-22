using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public TMP_Text  timerText;
    public TMP_Text  timerTextShadow;
    private float elapsedTime;
    private bool isRunning = false;
    public static Timer instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else{
            Destroy(gameObject);
        }
    }

    void Update()
    {
        timerText.enabled = Settings.Instance.timerOn;
        timerTextShadow.enabled = Settings.Instance.timerOn;

        if (isRunning)
        {
            if (!SceneManager.GetActiveScene().isLoaded) // Check if a scene is loading
            {
                isRunning = false;
            }
            else
            {
                isRunning = true;
                elapsedTime += Time.deltaTime;
                UpdateTimerDisplay();
            }
        }
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        StopTimer();
        UpdateTimerDisplay();
    }

    public string FormatTime()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000) % 1000);

        string timerString = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds / 10);
        return timerString;
    }

    void UpdateTimerDisplay()
    {
        timerText.text = FormatTime();
        timerTextShadow.text = FormatTime();
    }
}
