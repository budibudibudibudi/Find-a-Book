using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class gamemanagerscript : MonoBehaviourPunCallbacks
{
    public GameObject[] waypoint;
    public GameObject waypointslot;
    public GameObject buku;
    public static gamemanagerscript instance;
    public int rand;
    public GameObject alert;
    public bool gameStart;
    public int maxplayer = 2;
    public GameObject jumlahplayercanvas;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        Application.targetFrameRate = 30;
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
  Debug.unityLogger.logEnabled = false;
#endif
    }
    public void Start()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == maxplayer)
        {
            waypoint = new GameObject[waypointslot.transform.childCount];
            for (int i = 0; i < waypoint.Length; i++)
                waypoint[i] = waypointslot.transform.GetChild(i).gameObject;

            rand = Random.Range(0, waypoint.Length);
            PhotonNetwork.InstantiateRoomObject(buku.transform.name, waypoint[rand].transform.position, waypoint[rand].transform.rotation);
        }
        
    }

    private void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (gameStart)
        {
            foreach (var item in players)
            {
                item.GetComponent<playermovement>().alert();
            }
        }
        if(PhotonNetwork.CurrentRoom.PlayerCount < maxplayer)
        {
            foreach (var item in players)
            {
                item.GetComponent<playermovement>().enabled = false;
                jumlahplayercanvas.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Player : " + PhotonNetwork.CurrentRoom.PlayerCount + "/" + maxplayer;
                //item.transform.Find("Canvas/h&s/Panel jumlah p/TEXT").GetComponent<Text>().text = "Player : "+PhotonNetwork.CurrentRoom.PlayerCount + "/" +maxplayer;
            }
        }
        else
        {
            foreach (var item in players)
            {
                item.GetComponent<playermovement>().enabled = true;
                Destroy(jumlahplayercanvas);
                //Destroy(item.transform.Find("Canvas/h&s/Panel jumlah p").gameObject);
            }
            foreach (var item in GameObject.FindGameObjectsWithTag("hantu"))
            {
                item.GetComponent<enemymove>().enabled = true;
            }

        }
    }

    public IEnumerator shoutalert()
    {
        alert.SetActive(true);
        yield return new WaitForSeconds(5);
        alert.SetActive(false);
    }
}
