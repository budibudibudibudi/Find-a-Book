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

    public itemclass[] senjata;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        waypoint = new GameObject[waypointslot.transform.childCount];
        for (int i = 0; i < waypoint.Length; i++)
            waypoint[i] = waypointslot.transform.GetChild(i).gameObject;

        rand = Random.Range(0, waypoint.Length);
        PhotonNetwork.Instantiate(buku.transform.name, waypoint[rand].transform.position, waypoint[rand].transform.rotation);
        for (int i = 0; i < 10; i++)
        {
            int random = Random.Range(0, waypoint.Length);
            int random_senjata = Random.Range(0, senjata.Length);
            PhotonNetwork.Instantiate("senjata/" + senjata[random_senjata].gun.transform.name, waypoint[random].transform.position, senjata[random_senjata].gun.transform.rotation );

        }
    }


    public IEnumerator restart_game(GameObject target)
    {
        yield return new WaitForSeconds(2);
        Destroy(target);
        FindObjectOfType<spawnplayer>().Start();
    }

}
