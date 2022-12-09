using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouselook : MonoBehaviour
{
    float mousesensitifity = 50f;
    public Transform playerbody;
    [SerializeField] FixedJoystick analog;
    public bool lockcamera = false;

    float xrotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(!lockcamera)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            xrotation -= mouseY * mousesensitifity * Time.deltaTime;
            xrotation = Mathf.Clamp(xrotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xrotation, 0f, 0f);
            playerbody.Rotate(Vector3.up * mouseX * mousesensitifity * Time.deltaTime);
        }
    }
}
