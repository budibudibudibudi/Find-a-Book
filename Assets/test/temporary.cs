using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class temporary : MonoBehaviour
{
    NavMeshAgent nm;
    GameObject target;
    public Transform cam;
    GameObject[] way;

    public float waittime;
    public float startwait = 3;
    bool ischase = false;

    AudioSource audioo;

    public int randomspot;
    private void Start()
    {
        waittime = startwait;
        nm = GetComponent<NavMeshAgent>();
        way = GameObject.FindGameObjectsWithTag("hantu");
        randomspot = Random.Range(0, way.Length);
        audioo = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {

        nm.SetDestination(way[randomspot].transform.position);
        cariway();
        Debug.Log(Vector3.Distance(transform.position, way[randomspot].transform.position));

    }

    bool cariway()
    {
        if (Vector3.Distance(transform.position, way[randomspot].transform.position) < 5f)
        {
            if (waittime <= 0)
            {
                randomspot = Random.Range(0, way.Length);
                nm.SetDestination(way[randomspot].transform.position);
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