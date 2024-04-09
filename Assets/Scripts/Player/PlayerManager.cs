using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public bool hasKey = false;
    public GameObject keyFollowPrefab;
    private GameObject key; 
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    //This method is called when the player touches another trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Key")) 
        {
            hasKey = true;
            
            key = Instantiate(keyFollowPrefab, other.gameObject.transform.position, Quaternion.identity);
            key.GetComponent<CameraFollow>().target = gameObject.transform;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Door"))
        {
            if(hasKey == true)
            {
                hasKey = false;
                Destroy(key);
                Destroy(other.gameObject);

            }
        }
    }
}
