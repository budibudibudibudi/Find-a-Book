using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboard : MonoBehaviour
{
    Transform maincameratransform;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        maincameratransform = Camera.main.transform;
        transform.LookAt(transform.position + maincameratransform.rotation * Vector3.forward, maincameratransform.rotation * Vector3.up);
    }
}
