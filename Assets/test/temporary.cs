using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temporary : MonoBehaviour
{
    public GameObject camholder;
    private void Update()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * 50);
        float verticalrot = 0;
        verticalrot += Input.GetAxisRaw("Mouse Y") * 50;
        verticalrot = Mathf.Clamp(verticalrot, -90, 90);

        camholder.transform.localEulerAngles = Vector3.left*verticalrot;
        print(verticalrot);
    }
}