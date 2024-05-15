using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ExitEnding : MonoBehaviour
{
    public string NextSceneName = "Level 1"; //scene to load, most likely "Level 1"
    public float exitInput;

    [SerializeField]
    private InputActionReference exit;
    void Update()
    {
        exitInput = exit.action.ReadValue<float>();
        if(exitInput == 1)
        {
            SceneManager.LoadScene(NextSceneName);
            Timer.instance.ResetTimer();
        }
    }
}
