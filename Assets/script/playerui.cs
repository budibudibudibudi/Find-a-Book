using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class playerui : MonoBehaviour
{
    public void resume()
    {
        GetComponent<playermovement>().pausepanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        GetComponent<playermovement>().canrun = true;
        GetComponent<weaponplayer>().canshoot = true;
        GetComponent<weaponplayer>().canreload = true;
        GetComponent<playermovement>().pause = false;
    }
    public void respawn()
    {
        if (!GetComponent<PhotonView>().IsMine)
            return;
        if (GetComponent<playermovement>().impostor)
        {
            gamemanagerscript.instance.Start();
        }
        GetComponent<playermovement>().death();
    }
    public void quit()
    {
        Application.Quit();
    }
}
