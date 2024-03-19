// this script advances the scene when the player enters the goal

using UnityEngine;
using UnityEngine.SceneManagement;

public class AdvanceScene : MonoBehaviour
{
    public string NextSceneName; // Name of the next scene to load

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player entered the goal
        if (other.CompareTag("Player"))
        {
            // Load the next scene
            SceneManager.LoadScene(NextSceneName);
        }
    }
}