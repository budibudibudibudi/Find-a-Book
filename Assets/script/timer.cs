using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timer : MonoBehaviour
{
    private float timerr;
    TextMeshProUGUI timertext;
    // Start is called before the first frame update
    private void OnEnable()
    {
        timertext = GetComponent<TextMeshProUGUI>();
        timerr = 60;
    }

    // Update is called once per frame
    void Update()
    {
        timerr -= Time.deltaTime;
        if (timerr < 0)
            timerr = 0;
        int seconds = (int)timerr;
        timertext.text = seconds.ToString();
    }
}
