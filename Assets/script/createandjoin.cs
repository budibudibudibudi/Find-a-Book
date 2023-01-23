using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class createandjoin : MonoBehaviourPunCallbacks
{
    public InputField inputnama;
    public GameObject errorteks;
    public GameObject table;
    GameObject[] tables;
    private void Start()
    {
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
  Debug.unityLogger.logEnabled = false;
#endif
        inputnama.characterLimit = 5;
        tables = new GameObject[table.transform.childCount];
        for (int i = 0; i < table.transform.childCount; i++)
        {
            tables[i] = table.transform.GetChild(i).gameObject;
        }
        refreshui();
    }

    void refreshui()
    {
        for (int i = 0; i < tables.Length; i++)
        {
            tables[i].GetComponentInChildren<Text>().text = "Room " + i;
        }
    }

    public void createorjoin(string nama)
    {
        if(inputnama.text == "")
        {
            errorteks.SetActive(true);
            errorteks.GetComponent<Text>().text = "NAMA TIDAK BOLEH KOSONG";
        }
        else
        {
            PlayerPrefs.SetString("Player_Name", inputnama.text);
            RoomOptions room = new RoomOptions { MaxPlayers = 4 };
            PhotonNetwork.JoinOrCreateRoom(nama, room, TypedLobby.Default);
        }
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("game");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        errorteks.SetActive(true);
        errorteks.GetComponent<Text>().text = "ROOM FULL";
    }

    //public void JoinRoom()
    //{
    //    string roomName = roomNameInputField.text;
    //    PhotonNetwork.JoinRoom(roomName);
    //}

    //public void CreateRoom()
    //{
    //    string roomName = roomNameInputField.text;
    //    RoomOptions roomOptions = new RoomOptions { MaxPlayers = 4 };
    //    PhotonNetwork.CreateRoom(roomName, roomOptions);
    //}

}
