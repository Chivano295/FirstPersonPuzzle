using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool CanClimb => InLadderRange && LookingAtLadder;
    public bool IsClimbing = false;
    public bool InLadderRange = false;
    public bool LookingAtLadder = false;

    public CharacterController controller;
    public bool SuperJump;
    public Transform GroundCheck;
    public LayerMask Groundmask;

    [Header("Properties")]
    [SerializeField]
    private float speed = 12f;
    [SerializeField]
    private float gravity = -9.81f;
    [SerializeField]
    private float jumpHeight = 3f;
    [SerializeField]
    private float cooldown = 0.3f;
    [SerializeField]
    private float superJumpHeight = 10f;
    [SerializeField]
    private float groundDistance = 0.4f;
    [SerializeField]
    private float elevationSpeed = 0.5f;
    
    private Vector3 velocity;
    [SerializeField, ReadOnly]
    private bool isGrounded;
    private float elevation = 0f;

    public bool camController;

    void Update()
    { 
        // defines what isgrounded means
        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, Groundmask);
        //checks if your grounded if not you fall slowly 
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -4f;
        }
        //variables for x and z movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (CanClimb)
        {
            if (z == 1 && !IsClimbing)
            {
                IsClimbing = true;
                controller.Move(Vector3.up * elevationSpeed * Time.deltaTime);
                elevation += elevationSpeed * Time.deltaTime;
                return;
            }
            else if (z == 1 && IsClimbing)
            {
                controller.Move(Vector3.up * elevationSpeed * Time.deltaTime);
                elevation += elevationSpeed * Time.deltaTime;
                return;
            }
            else if (z == -1 && IsClimbing)
            {
                controller.Move(Vector3.down * elevationSpeed * Time.deltaTime);
                elevation -= elevationSpeed * Time.deltaTime;
                if (elevation <= 0)
                {
                    IsClimbing = false;
                }
                return;
            }
            else if (IsClimbing)
            {
                return;
            }
        }


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

        if (SuperJump == true)
        {
            jumpHeight = superJumpHeight;
        }
        else
        {
            jumpHeight = 1f;
        }

        
                
    }
}