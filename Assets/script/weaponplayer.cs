using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class weaponplayer : MonoBehaviour
{
    private itemclass mainWeapon;
    public GameObject peluru;
    public Transform gunContainer;
    public Text peluruteks;

    bool canshoot;
    bool canreload;

    private void Start()
    {
        peluruteks.text = "";
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(mainWeapon != null)
            {
                if(mainWeapon.currentmagazine > 0)
                    shoot();
            }
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            if(canreload)
            {
                StartCoroutine(Reload());
            }
        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            if(mainWeapon != null)
            {
                unequipingweap();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "senjata")
        {
            if(mainWeapon == null)
            {
                mainWeapon = Resources.Load<itemclass>("scriptableobject/"+other.gameObject.name);
                other.transform.SetParent(gunContainer);
                other.GetComponent<PhotonView>().RPC("HideObject", RpcTarget.AllBuffered);
                gunContainer.GetChild(0).transform.localPosition = Vector3.zero;
                gunContainer.GetChild(0).transform.localRotation = Quaternion.Euler(Vector3.zero);
                canshoot = true;
            }
        }
    }
    void shoot()
    {
        if (canshoot)
        {
            GameObject temp = PhotonNetwork.Instantiate(peluru.name, gunContainer.GetChild(0).GetChild(0).transform.position, gunContainer.GetChild(0).GetChild(0).rotation);
            temp.GetComponent<Rigidbody>().AddForce(gunContainer.transform.GetChild(0).GetChild(0).transform.forward * 100, ForceMode.Impulse);
            mainWeapon.currentmagazine = Mathf.Clamp(mainWeapon.currentmagazine - 1, 0, mainWeapon.maxmagazine);
            peluruteks.text = mainWeapon.currentmagazine + "/" + mainWeapon.stockmagazine;
            canreload = true;
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

    public void unequipingweap()
    {
        Destroy(gunContainer.GetChild(0).gameObject);
        mainWeapon = null;
        canshoot = false;
        canreload = false;
        peluruteks.text = "";
    }
}
