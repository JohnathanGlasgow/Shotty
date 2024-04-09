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

    //This method is called when another object enters the trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Other: " + other.gameObject.name);
        
        // Check if the object that entered the trigger has the tag you're interested in
        if (other.gameObject.CompareTag("Key")) // Replace "Player" with your desired tag
        {
            Debug.Log("Key Grabbed");
            hasKey = true;
            
            key = Instantiate(keyFollowPrefab, other.gameObject.transform.position, Quaternion.identity);
            key.GetComponent<CameraFollow>().target = gameObject.transform;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Door"))
        {
            if(hasKey == true)
            {
                Debug.Log("Door Open");
                hasKey = false;
                Destroy(key);
                Destroy(other.gameObject);

            }
        }
    }
}
