using UnityEngine;

public class GameCompletePanel : MonoBehaviour
{
    public GameObject gameCompletePanel;
    public GameObject player;

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level 1");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0;
            gameCompletePanel.SetActive(true);
            // disable player movement script
            player.GetComponent<PlayerMovement>().enabled = false;
            // disable shoot controller script
            player.GetComponent<ShootController>().enabled = false;
            // disable player rb
            player.GetComponent<Rigidbody2D>().simulated = false;
            // disable crosshair movement script of child rotate point
            player.transform.Find("RotatePoint").GetComponent<CrosshairMovement>().enabled = false;
        }
    }
}
