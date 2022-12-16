using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temporary : MonoBehaviour
{
    public float sensi = -1;
    public Vector3 rotate;
    private void Update()
    {
        float x = Input.GetAxisRaw("Mouse X");
        float y = Input.GetAxisRaw("Mouse Y");
        rotate = new Vector3(x, y * sensi, 0);
        transform.eulerAngles = transform.eulerAngles - rotate;
    }
}