using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script changes the respawn position when trigger events are called.
/// </summary>
public class Checkpoint : MonoBehaviour
{
    public GameObject flagUnactivated; //the red flag

    /// <summary>
    /// When the player passes through a checkpoint trigger
    /// </summary>
    /// <param name="col"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.respawnPosition = this.transform.position; //set gamemanager respawn position to this objects position
            Destroy(flagUnactivated); //destroys the red unactivated flag object
            Destroy(gameObject); //destroys this gameobject which deletes the trigger.
        }   
    }
}
