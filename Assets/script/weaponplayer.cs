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
    public GameObject muzzle_flash;
    public GameObject crosshair;

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
                    {
                        StartCoroutine(shoot());

                        this.GetComponent<playermovement>().audioa.PlayOneShot(GetComponent<playermovement>().all[2]);
                    }
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

    IEnumerator shoot()
    {
        if (canshoot)
        {
            RaycastHit hit;
            if (Physics.Raycast(GetComponentInChildren<Camera>().transform.position, GetComponentInChildren<Camera>().transform.forward, out hit, range))
            {
                if(hit.transform.tag == "Player")
                {
                    if (hit.transform.GetComponent<playermovement>().CanbeTarget)
                    {
                        hit.collider.gameObject.GetComponent<playermovement>().view.RPC("changehealth", RpcTarget.All, -2.5f);
                        crosshair.SetActive(true);
                    }
                }
            }
            muzzle_flash.SetActive(true);
            mainWeapon.currentmagazine = Mathf.Clamp(mainWeapon.currentmagazine - 1, 0, mainWeapon.maxmagazine);
            peluruteks.text = mainWeapon.currentmagazine + "/" + mainWeapon.stockmagazine;
            canreload = true;
            yield return new WaitForSeconds(0.1f);
            muzzle_flash.SetActive(false);
            crosshair.SetActive(false);
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
