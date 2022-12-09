using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class createandjoin : MonoBehaviourPunCallbacks
{
    public Text jumlahplayertext;
    public InputField input_nama;
    public GameObject inputerror;
    public void CreateRoom()
    {
        if (input_nama.text == "")
            inputerror.SetActive(true);
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

    private void Start()
    {
        jumlahplayertext.text = jumlahplayertext.text + PhotonNetwork.CountOfPlayersInRooms.ToString() + "/20";

    }
}
