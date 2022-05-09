using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookNet : NetworkBehaviour
{
    public float MouseSensitivity = 100f;
    public GameObject CameraOfplayer;
    public Transform playerBody;

    private float xRotation = 0f;
    public float mouseX;
    public float mouseY;

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        Cursor.lockState = CursorLockMode.Locked;
        CameraOfplayer.SetActive(true);
    }

    void Update()
    {
        if (!hasAuthority) return;

        if (!isLocalPlayer) return;
        if (Cursor.lockState == CursorLockMode.Locked && Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        //stores mouse positions
        mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
        
        CmdRotatePlayer();
    }
    [Command]
    public void CmdRotatePlayer()
    {
        //clamps the position so you cant go up to infinity
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90);

        //rotates player when doing it on the x
        CameraOfplayer.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
        
    }

}
