using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreenTime : MonoBehaviour
{
    public TMP_Text endScreenTime;
    // Start is called before the first frame update
    void Start()
    {
        endScreenTime.text = Timer.instance.FormatTime();
    }
}
