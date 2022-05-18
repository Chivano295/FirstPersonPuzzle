using System;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupNet : NetworkBehaviour
{
    public Transform equipPos;
    public Camera cam;
    //[SyncVar]
    public GameObject Currentgrab;
    [SerializeField] private float distance;
    
    //[SyncVar]
    [SerializeField]
    private bool grab;
    
    private GameObject wantsToGrab;
    
    [ClientCallback]
    public void Update()
    {
        if (Currentgrab != null)
        {
            Currentgrab.transform.position = equipPos.position;
            Currentgrab.transform.rotation = equipPos.rotation;
        }
        if (Input.GetKeyDown(KeyCode.E) && Currentgrab == null)
        {
            Debug.Log("ik doe iets");
            if (!isLocalPlayer) return;
            //if (!hasAuthority) return;
            RaycastHit hit; // schiet raycast vanuit camera naar waar je kijkt
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance))
            {
                wantsToGrab = hit.collider.gameObject;
                CmdServCheck();
            }
        }
        //roept drop functie aan
        if(Currentgrab != null && Input.GetKeyDown(KeyCode.Q))
        {
            CmdDrop();
            
        }
    }

    [Command]
    public void CmdServCheck(NetworkConnectionToClient sender = null)
    {
        GameObject player = sender.identity.gameObject;
        if (sender.identity.GetComponent<PickupNet>().wantsToGrab.CompareTag("Grab")) //checkt of hij grab tag heeft
        {
            Currentgrab = sender.identity.GetComponent<PickupNet>().wantsToGrab;
            PickUp();
        }
        Span<byte> buffer = stackalloc byte[1024];
        buffer[0] = 0x68;
        buffer[1] = 0x00;
        Debug.Log(buffer.ToString());
    }

    [Server]
    public void PickUp()
    {
        // zet grab op true, de rigidbody van het item op kinematic en zet het object op een set positie
        grab = true;
        Currentgrab.GetComponent<Rigidbody>().isKinematic = true;
        Currentgrab.GetComponent<Collider>().enabled = false;
        Currentgrab.transform.position = equipPos.transform.position;
        //Currentgrab.transform.parent = player.transform;
    }
    [Command]
    public void CmdDrop()
    {
        // currentgrab gameobject word geleegd, en de player word niet meer als parent gezien en zet rigidbody op niet kinematic
        Currentgrab.GetComponent<Rigidbody>().isKinematic = false;
        Currentgrab.GetComponent<Collider>().enabled = true;
        //Currentgrab.transform.parent = null;
        Currentgrab = null;
        grab = false;
        
    }
}

