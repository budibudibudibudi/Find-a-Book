using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class spawnplayer : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject musuh;
    [SerializeField] Transform[] spawnposmusuh;
    PhotonView view;
    // Start is called before the first frame update
    public void Start()
    {
        Vector3 randompost = new Vector3(Random.Range(52.4f, -60.5f), 30, Random.Range(52.4f, -34.9f));
        PhotonNetwork.Instantiate(player.name, randompost, Quaternion.identity);
    }

    public void bangkit(GameObject _player)
    {
        Vector3 randompost = new Vector3(Random.Range(52.4f, -60.5f), 30, Random.Range(52.4f, -34.9f));
        PhotonNetwork.Instantiate("player", randompost, Quaternion.identity);
        PhotonNetwork.Destroy(_player);
    }
    // spawn hantu
    public void spawningmusuh()
    {
        for (int i = 0; i < 2; i++)
        {
            int rand = Random.Range(0, spawnposmusuh.Length);
            PhotonNetwork.InstantiateRoomObject(musuh.name, spawnposmusuh[rand].position, Quaternion.identity);
        }
    }
}
