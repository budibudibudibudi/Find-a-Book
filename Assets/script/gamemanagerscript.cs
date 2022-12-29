using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class gamemanagerscript : MonoBehaviourPunCallbacks
{
    public GameObject[] waypoint;
    public GameObject waypointslot;
    public GameObject buku;
    public static gamemanagerscript instance;
    public int rand;
    public GameObject alert;
    public bool gameStart;
    public timer _timer;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        Application.targetFrameRate = 60;
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
  Debug.unityLogger.logEnabled = false;
#endif
    }
    public void Start()
    {
        waypoint = new GameObject[waypointslot.transform.childCount];
        for (int i = 0; i < waypoint.Length; i++)
            waypoint[i] = waypointslot.transform.GetChild(i).gameObject;

        rand = Random.Range(0, waypoint.Length);
        PhotonNetwork.InstantiateRoomObject(buku.transform.name, waypoint[rand].transform.position, waypoint[rand].transform.rotation);
        
    }

    private void Update()
    {
        if(gameStart)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (var item in players)
            {
                item.GetComponent<playermovement>().alert();
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
