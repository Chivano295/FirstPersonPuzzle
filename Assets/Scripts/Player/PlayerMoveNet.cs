using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveNet : NetworkBehaviour
{
    public CharacterController Controller;
    public Transform groundCheck;
    public LayerMask groundmask;
    public bool SuperJump;

    [SerializeField] private float speed = 12f;
    [SerializeField]private float gravity = -9.81f;
    [SerializeField]private float jumpHeight = 3f;
    [SerializeField]private float cooldown = 0.3f;
    [SerializeField] private float superJumpHeight = 10f;

    [SerializeField]private float groundDistance = 0.4f;

    Vector3 velocity;
    [SerializeField]bool isGrounded;

    private float mouseX;
    private float mouseY;
    private Vector3 movement = Vector3.zero;

    //[Client]
    void Update()
    {

        CmdMoveJump();

    }

    //EW[Command]
    public void CmdMoveJump()
    {
        if (isLocalPlayer)
        {
            // defines what isgrounded means
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundmask);
            //checks if your grounded if not you fall slowly 
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -4f;
            }
            //variables for x and z movement
            mouseX = Input.GetAxis("Horizontal");
            mouseY = Input.GetAxis("Vertical");

            movement = transform.right * mouseX + transform.forward * mouseY;
            UpdatePosition();

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);

            }

            // smooth gravity
            velocity.y += gravity * Time.deltaTime;

            Controller.Move(velocity * Time.deltaTime);

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
    
    //[ClientRpc]
    public void UpdatePosition()
    {
        //the actual movement code
        Controller.Move(movement * speed * Time.deltaTime);
    }

}
