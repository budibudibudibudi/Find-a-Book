using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class createandjoin : MonoBehaviourPunCallbacks
{
    public InputField input_nama;
    public GameObject inputerror;
    private void Start()
    {
        input_nama.characterLimit = 5;
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
  Debug.unityLogger.logEnabled = false;
#endif
    }
    public void CreateRoom()
    {
        if (input_nama.text == "")
        {
            inputerror.SetActive(true);
            inputerror.GetComponent<Text>().text = "NAMA TIDAK BOLEH KOSONG";
        }
        else
        {
            PlayerPrefs.SetString("Player_Name", input_nama.text);
            PhotonNetwork.CreateRoom("a");
        }
    }
    public void JoinRoom()
    {
        if (input_nama.text == "")
        {
            inputerror.SetActive(true);
            inputerror.GetComponent<Text>().text = "NAMA TIDAK BOLEH KOSONG";
        }
        else
        {
            PlayerPrefs.SetString("Player_Name", input_nama.text);
            PhotonNetwork.JoinRoom("a");
        }
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("game");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
