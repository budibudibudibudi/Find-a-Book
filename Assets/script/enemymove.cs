using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class enemymove : MonoBehaviour
{
    NavMeshAgent nm;
    GameObject[] target;
    public Transform cam;
    gamemanagerscript gms;

    public float waittime;
    public float startwait = 3;

    public int randomspot;
    private void Awake()
    {
    }
    private void Start() {
        waittime = startwait;
        gms = FindObjectOfType<gamemanagerscript>();
        nm = GetComponent<NavMeshAgent>();
        randomspot = Random.Range(0, gms.waypoint.Length);
    }

    private void Update() {
        
        target = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < target.Length; i++)
        {
            if (target[i] != null)
            {
                if (Vector3.Distance(transform.position, target[i].transform.position) <= 30f)
                {
                    nm.SetDestination(target[i].transform.position);
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
            if (Vector3.Distance(transform.position, target[i].transform.position) <= 10)
            {
                StartCoroutine(jumpscare(target[i]));
            }
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
    IEnumerator jumpscare(GameObject target)
    {
        this.transform.SetParent(target.transform.Find("jumpscare"));
        this.transform.localPosition = Vector3.zero;
        target.GetComponent<playermovement>().enabled = false;
        yield return new WaitForSeconds(2);
        target.transform.Find("jumpscare").DetachChildren();
        gamemanagerscript.instance.Start();
        FindObjectOfType<spawnplayer>().bangkit(target);
    }
}