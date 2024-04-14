using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineMovement : MonoBehaviour
{
    public float speed = 1.0f;
    public float distance = 1.0f;
    public bool startMovingUp = true;
    public bool moveVertically = true; // Add this flag

    private float currentDistance = 0.0f;
    private float timeOffset;



    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.localPosition;
        timeOffset = startMovingUp ? 0.0f : Mathf.PI / speed;
    }

    // Update is called once per frame
    void Update()
    {
        currentDistance = distance * (Mathf.Sin((Time.time + timeOffset) * speed) / 2.0f + 0.5f);

        if (moveVertically)
        {
            transform.localPosition = new Vector3(startPosition.x, startPosition.y + currentDistance, startPosition.z);
        }
        else
        {
            transform.localPosition = new Vector3(startPosition.x + currentDistance, startPosition.y, startPosition.z);
        }
    }
}