using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsCamera : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 100f;
    float xRotation = 0f;
    PlayerMovement playerm;
    // Start is called before the first frame update
    void Start()
    {
        playerm = GameObject.Find("Player").GetComponent<PlayerMovement>();
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -85f, 85f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        player.Rotate(Vector3.up * mouseX);
        Crouch();
    }
    private void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.position = new Vector3(transform.position.x, 1.0f, transform.position.z);
            
           
        }
        else if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.position = new Vector3(transform.position.x, 2.0f/*transform.position.y + 1.0f*/, transform.position.z);
        }

    }
}