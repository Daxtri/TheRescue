using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsCamera : MonoBehaviour
{
    public Menu menu;
    public Transform player;
    public float mouseSensitivity = 2.5f;
    float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (menu.paused == false)
        {
            Cursor.lockState = CursorLockMode.Locked;

            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;//* Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;//* Time.deltaTime;

            xRotation -= mouseY;

            xRotation = Mathf.Clamp(xRotation, -85f, 85f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            player.Rotate(Vector3.up * mouseX);
        }
        else 
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}