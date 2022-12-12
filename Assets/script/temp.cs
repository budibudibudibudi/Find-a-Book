using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class temp : MonoBehaviour
{
    private void Start()
    {
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * 3);
    }
}
