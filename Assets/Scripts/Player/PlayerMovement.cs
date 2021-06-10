using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isInRangeBoss, isInRangeBoss2;
    public bool scoped;
    public GameObject sniperScope;
    public PlayerController playerController;
    public GameObject gun0, gun1, gun2;
    public Sniper sniper;
    public CharacterController controller;
    public GameObject camera;
    public AudioManager audio;

    float speed;

    int currentWeapon, previousWeapon;

    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;

    private void Start()
    {
        audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        isInRangeBoss = false;
        isInRangeBoss2 = false;
        scoped = false;
        playerController = GetComponent<PlayerController>();
        speed = playerController.normalSpeed;
        previousWeapon = currentWeapon = 1;
    }

    void Update()
    {
        Sprint();
        Crouch();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(1);
            sniper.Unscope();
            camera.GetComponent<FpsCamera>().mouseSensitivity = 2.5f;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(2);
            sniper.Unscope();
            camera.GetComponent<FpsCamera>().mouseSensitivity = 2.5f;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWeapon(3);
            sniper.Unscope();
            camera.GetComponent<FpsCamera>().mouseSensitivity = 2.5f;
        }

        if (Input.GetKeyDown(KeyCode.Q))
            SwitchWeapon(previousWeapon);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(playerController.jumpForce * -2f * playerController.gravity);
            audio.Play("Jump");
        }

        velocity.y += playerController.gravity * Time.deltaTime;

        controller.Move((move * speed + velocity) * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            audio.PlayOnce("PlayerWalk");
        
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            audio.Stop("PlayerWalk");
    }

    private void Sprint()
    {
       
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            audio.Stop("PlayerWalk");
            audio.Play("PlayerSprint");
            speed = playerController.sprintSpeed;
            Debug.Log("Sprinting");
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = playerController.normalSpeed;
            audio.Stop("PlayerSprint");
        }
    }

    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            controller.height = playerController.crouchHeight;
            speed = playerController.crouchSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            controller.height = playerController.originalHeight;
            speed = playerController.normalSpeed;
        }
    }

    void SwitchWeapon(int curWeapon)
    {
        if (GetComponent<PlayerController>().isReloading == false)
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

    private void OnTriggerStay(Collider other)
    {
        string tag = other.tag;

        switch (tag)
        {
            case "Interact":
                Debug.Log("Inside");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    other.GetComponent<Interact>().activated = true;
                }
                break;

            case "Health":
                playerController.currentHealth += 20f;
                Destroy(other.gameObject);
                break;

            case "Armor":
                playerController.currentArmor += 20f;
                Destroy(other.gameObject);
                break;

            case "RifleAmmo":
                int bullets = gun1.GetComponent<Rifle>().curReserve + (gun1.GetComponent<Rifle>().maxReserve / 6);
                if (bullets > gun1.GetComponent<Rifle>().maxReserve)
                    gun1.GetComponent<Rifle>().curReserve = gun1.GetComponent<Rifle>().maxReserve;
                else
                    gun1.GetComponent<Rifle>().curReserve += bullets;

                Destroy(other.gameObject);
                break;

            case "SniperAmmo":
                int bullets1 = gun2.GetComponent<Sniper>().curReserve + (gun2.GetComponent<Sniper>().maxReserve / 3);
                if (bullets1 > gun2.GetComponent<Sniper>().maxReserve)
                    gun2.GetComponent<Sniper>().curReserve = gun2.GetComponent<Sniper>().maxReserve;
                else
                    gun2.GetComponent<Sniper>().curReserve += bullets1;
                Destroy(other.gameObject);
                break;

            case "ArenaCollision1":
                isInRangeBoss = true;
                break;

            case "ArenaCollision2":
                isInRangeBoss2 = true;
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        string tag = other.tag;

        switch (tag)
        {
            case "ArenaCollision1":
                isInRangeBoss = false;
                break;

            case "ArenaCollision2":
                isInRangeBoss2 = false;
                break;
        }
    }
}