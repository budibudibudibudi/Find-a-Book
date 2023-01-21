using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Photon.Pun;
public class cooldownrespawn : MonoBehaviourPun
{
    float cooldown = 5;
    private void Start()
    {
    }

    private void Update()
    {
        cooldown -= Time.deltaTime;
        int seconds = (int)cooldown;
        this.GetComponent<Text>().text = seconds.ToString();
        if (cooldown<= 0)
        {
            cooldown = 0;
            this.GetComponent<Text>().text = "0";
            FindObjectOfType<spawnplayer>().Start();
            Destroy(this.transform.parent.transform.parent.gameObject);
        }
    }
}
