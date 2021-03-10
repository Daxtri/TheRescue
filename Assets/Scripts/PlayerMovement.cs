using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public CapsuleCollider collider;
    Vector3 velocity;

    public float speed = 7.0f;
    public float gravity = -9.8f;
    public float sprintSpeed = 10.0f;
    public float crouchSpeed = 4.0f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;

    public float jumpHeight = 3f;
    private void Start()
    {
        collider.height = 2.0f;
        
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 
        groundDistance, groundMask);

        if(isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        
        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
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
        if (Input.GetKey(KeyCode.LeftControl))
        {
            collider.height = 1.0f;
            speed = crouchSpeed;

        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            collider.height = 2.0f;
            speed = 7.0f;

        }
    }
}