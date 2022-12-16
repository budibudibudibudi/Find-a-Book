using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class weaponplayer : MonoBehaviourPun
{
    public itemclass mainWeapon;
    public GameObject peluru;
    public Transform gunContainer;
    public Text peluruteks;
    public float bulletSpeed = 200;
    public float range = 100;

    public bool canshoot = false ;
    bool canreload;

    private void Start()
    {
        if(GetComponent<PhotonView>().IsMine)
            if(mainWeapon != null)
                peluruteks.text = mainWeapon.currentmagazine + "/" + mainWeapon.stockmagazine;
    }
    private void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (mainWeapon != null)
                {
                    if (mainWeapon.currentmagazine > 0)
                        shoot();
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (canreload)
                {
                    StartCoroutine(Reload());
                }
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.tag == "senjata")
    //    {
    //        if(mainWeapon == null)
    //        {
    //            mainWeapon = Resources.Load<itemclass>("scriptableobject/"+other.gameObject.name);
    //            GameObject go = PhotonNetwork.Instantiate("senjata/" + other.gameObject.name, transform.position, Quaternion.identity);
    //            PhotonNetwork.Destroy(other.gameObject);
    //            go.transform.SetParent(gunContainer);
    //            go.transform.localPosition = Vector3.zero;
    //            go.transform.localRotation = Quaternion.Euler(Vector3.zero);
    //            canshoot = true;
    //        }
    //    }
    //}

    //[PunRPC]
    //void DestroyObject(GameObject item)
    //{
    //    PhotonNetwork.Destroy(item);
    //}
    void shoot()
    {
        if (canshoot)
        {
            RaycastHit hit;
            if (Physics.Raycast(GetComponentInChildren<Camera>().transform.position, GetComponentInChildren<Camera>().transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);
            }
            //GameObject temp = PhotonNetwork.Instantiate(peluru.name, gunContainer.GetChild(0).GetChild(0).transform.position, gunContainer.GetChild(0).GetChild(0).rotation); RaycastHit hit;
            //if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
            //{
            //    temp.GetComponent<Rigidbody>().velocity = hit.point * bulletSpeed;
            //}
            //else
            //{
            //    temp.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * bulletSpeed;
            //}

            //mainWeapon.currentmagazine = Mathf.Clamp(mainWeapon.currentmagazine - 1, 0, mainWeapon.maxmagazine);
            //peluruteks.text = mainWeapon.currentmagazine + "/" + mainWeapon.stockmagazine;
            //canreload = true;
        }
    }
    public IEnumerator Reload()
    {
        canshoot = false;
        yield return new WaitForSeconds(3);
        mainWeapon.stockmagazine += mainWeapon.currentmagazine - mainWeapon.maxmagazine;
        mainWeapon.currentmagazine = Mathf.Clamp(mainWeapon.maxmagazine, 0, mainWeapon.stockmagazine);
        peluruteks.text = mainWeapon.currentmagazine + "/" + mainWeapon.stockmagazine;
        canreload = false;
        canshoot = true;
    }

}
