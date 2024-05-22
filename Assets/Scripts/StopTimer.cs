using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTimer : MonoBehaviour
{
    void Awake()
    {
        Timer.instance.StopTimer();
    }
}
