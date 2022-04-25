using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float cooldown = 0.3f;



    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundmask;

    Vector3 velocity;
    [SerializeField]bool isGrounded;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // defines what isgrounded means
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundmask);
        //checks if your grounded if not you fall slowly 
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -4f;
        }
        //variables for x and z movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //the actual movement code
        Vector3 movement = transform.right * x + transform.forward * z;

        controller.Move(movement * speed * Time.deltaTime);



        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);

        }

        // smooth gravity
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }
}
