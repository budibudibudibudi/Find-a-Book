using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class spawnplayer : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform[] spawnposmusuh;
    // Start is called before the first frame update
    public void Start()
    {
        Vector3 randompost = new Vector3(Random.Range(52.4f, -60.5f), 30, Random.Range(52.4f, -34.9f));
        PhotonNetwork.Instantiate(player.name, randompost, Quaternion.identity);
    }

    [PunRPC]
    public void bangkit(GameObject _player)
    {
        Vector3 randompost = new Vector3(Random.Range(52.4f, -60.5f), 30, Random.Range(52.4f, -34.9f));
        PhotonNetwork.Instantiate(_player.name, randompost, Quaternion.identity);
    }
    // spawn hantu
    //void spawningmusuh()
    //{
    //    int rand = Random.Range(0, spawnposmusuh.Length);
    //    PhotonNetwork.Instantiate(musuh.name, spawnposmusuh[rand].position, Quaternion.identity);
    //}
}
