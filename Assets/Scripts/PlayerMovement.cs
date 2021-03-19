using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject gun0, gun1, gun2;
    public CharacterController controller;
    public GameObject camera;

    bool weaponSwitched = false;

    int currentWeapon, previousWeapon;

    Vector3 velocity;

    public float speed = 7.0f;
    public float gravity = -9.8f;
    public float sprintSpeed = 10.0f;
    public float crouchSpeed = 4.0f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;

    float dist;

    public float jumpHeight = 3f;

    private float originalHeight, crouchHeight;

    private void Start()
    {
        previousWeapon = currentWeapon = 1;
        dist = controller.height / 2;
        originalHeight = controller.height;
        crouchHeight = 0.9f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchWeapon(1);

        else if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchWeapon(2);

        else if (Input.GetKeyDown(KeyCode.Alpha3))
            SwitchWeapon(3);

        if (Input.GetKeyDown(KeyCode.Q))
            SwitchWeapon(previousWeapon);

        Debug.Log("Curr" + currentWeapon.ToString() + "," + "Prev" + previousWeapon.ToString());

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        Sprint();
        Crouch();
        
    }
    private void Sprint()
    {
        float _zMovement = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift) && _zMovement == 1 && isGrounded)
        {
            speed = sprintSpeed;
            Debug.Log("Sprinting");
        }
        else
        {
            speed = 7.0f;
        }
    }
    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            controller.height = crouchHeight;
            speed = crouchSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            controller.height = originalHeight;
            speed = 7.0f;
        }

        Vector3 tmpScale = transform.localScale;
        Vector3 tmpPosition = transform.position;

        float ultScale = transform.localScale.y;

        tmpScale.y = Mathf.Lerp(transform.localScale.y, 1, Time.deltaTime);
        transform.localScale = tmpScale;

        tmpPosition.y += dist * (transform.localScale.y - ultScale); // fix vertical position        
        transform.position = tmpPosition;
    }

    void SwitchWeapon(int curWeapon)
    { 
        switch (curWeapon)
        {
            case 1:
                gun0.SetActive(true);
                gun1.SetActive(false);
                gun2.SetActive(false);
                break;

            case 2:
                gun0.SetActive(false);
                gun1.SetActive(true);
                gun2.SetActive(false);
                break;

            case 3:
                gun0.SetActive(false);
                gun1.SetActive(false);
                gun2.SetActive(true);
                break;
        }

        previousWeapon = currentWeapon;
        currentWeapon = curWeapon;
    }
}