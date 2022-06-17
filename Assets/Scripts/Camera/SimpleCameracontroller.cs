using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameracontroller : MonoBehaviour
{
    public int speed;
    public Transform transform;
    public float flySpeed;
    public playerMovement p_move;

    // Update is called once per frame
    void Update()
    {
        if (p_move.camController == true)
        {


            //variables for x and z movement
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");


            //the actual movement code
            Vector3 movement = transform.right * x + transform.forward * z;

            transform.position = transform.position + (movement * speed * Time.deltaTime);

            if (Input.GetKey(KeyCode.Space))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + flySpeed, transform.position.z);
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - flySpeed, transform.position.z);
            }
            
        }
       
    }
}
