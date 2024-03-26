using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject flagUnactivated;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("hi");
        Debug.Log(GameManager.instance.respawnPosition);
        GameManager.instance.respawnPosition = this.transform.position;
        Destroy(flagUnactivated);
        Destroy(gameObject);
    }

}
