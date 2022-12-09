using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class enemymove : MonoBehaviour
{
    NavMeshAgent nm;
    GameObject target;
    public Transform cam;
    gamemanagerscript gms;

    public float waittime;
    public float startwait = 3;

    public int randomspot;
    private void Start() {
        waittime = startwait;
        gms = FindObjectOfType<gamemanagerscript>();
        nm = GetComponent<NavMeshAgent>();
        randomspot = Random.Range(0, gms.waypoint.Length);
    }

    private void Update() {
        
        target = GameObject.FindGameObjectWithTag("Player");
        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= 30f)
            {
                nm.SetDestination(target.transform.position);
            }
            else
            {
                nm.SetDestination(gms.waypoint[randomspot].transform.position);
                cariwaypoint();
            }
        }
        else
        {
            nm.SetDestination(gms.waypoint[randomspot].transform.position);
            cariwaypoint();
        }

        if(Vector3.Distance(transform.position,target.transform.position)<=10)
        {
            this.transform.SetParent(target.transform.Find("jumpscare"));
            this.transform.localPosition = Vector3.zero;
            target.transform.Find("Spot Light").gameObject.SetActive(false);
            target.transform.Find("Main Camera").transform.position = new Vector3(0, 1.553f, 0);
            target.transform.Find("Main Camera").GetComponent<mouselook>().lockcamera = true;
            StartCoroutine(gamemanagerscript.instance.restart_game(target));
        }
    }

    bool cariwaypoint()
    {
        if (Vector3.Distance(transform.position, gms.waypoint[randomspot].transform.position) < 0.2f)
        {
            if (waittime <= 0)
            {
                randomspot = Random.Range(0, gms.waypoint.Length);
                nm.SetDestination(gms.waypoint[randomspot].transform.position);
                waittime = startwait;
            }
            else
                waittime -= Time.deltaTime;
        }
        else
        {
            return false;
        }

        return true;
    }
}