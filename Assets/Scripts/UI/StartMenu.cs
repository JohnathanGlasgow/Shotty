using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public GameObject player;
    public CameraFollow cameraFollow;
    // Start is called before the first frame update
    public void StartGame()
    {
        // enable camera follow script
        cameraFollow.enabled = true;
        // disable player movement script
        player.GetComponent<PlayerMovement>().enabled = true;
        // disable shoot controller script
        player.GetComponent<ShootController>().enabled = true;
        // disable player rb
        player.GetComponent<Rigidbody2D>().simulated = true;
        // disable crosshair movement script of child rotate point
        player.transform.Find("RotatePoint").GetComponent<CrosshairMovement>().enabled = true;
    }

}
