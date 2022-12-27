using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class gamemanagerscript : MonoBehaviour
{
    public GameObject[] waypoint;
    public GameObject waypointslot;
    public GameObject buku;
    public static gamemanagerscript instance;
    public int rand;

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
    private void Start()
    {
        waypoint = new GameObject[waypointslot.transform.childCount];
        for (int i = 0; i < waypoint.Length; i++)
            waypoint[i] = waypointslot.transform.GetChild(i).gameObject;

        rand = Random.Range(0, waypoint.Length);
        PhotonNetwork.Instantiate(buku.transform.name, waypoint[rand].transform.position, waypoint[rand].transform.rotation);
    }
}
