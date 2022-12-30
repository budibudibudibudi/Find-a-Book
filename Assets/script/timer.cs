using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timer : MonoBehaviour
{
    private float timerr;
    [HideInInspector] public TextMeshProUGUI timertext;
    // Start is called before the first frame update
    private void OnEnable()
    {
        timertext = GetComponent<TextMeshProUGUI>();
        timerr = 5;
    }

    // Update is called once per frame
    void Update()
    {
        timerr -= Time.deltaTime;
        if (timerr < 0)
        {
            timerr = 0;
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < players.Length; i++)
            {
                if(players[i].GetComponent<playermovement>().impostor)
                {
                    foreach (var item in players)
                    {
                        item.GetComponent<playermovement>().win(players[i].GetComponent<playermovement>().view.Owner.NickName);
                    }

                }
            }
        }
        else
        {
            int seconds = (int)timerr;
            timertext.text = seconds.ToString();
        }
    }
}
