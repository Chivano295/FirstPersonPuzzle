using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupNet : NetworkBehaviour
{
    public Transform equipPos;
    public Camera cam;
    public GameObject Currentgrab;
    [SerializeField] private float distance;
    [SerializeField] private bool grab;
    [SerializeField] private GameObject player;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Currentgrab == null)
        {
            Debug.Log("ik doe iets");
            if (!isLocalPlayer) return;
            if (!hasAuthority) return;
            CmdServCheck();                                           
        }
        //roept drop functie aan
        if(Currentgrab != null && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
            
        }
      
            
    }

    [Command]
    public void CmdServCheck()
    {
        RaycastHit hit; // schiet raycast vanuit camera naar waar je kijkt
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance))
        {
            if (hit.transform.tag == "Grab") //checkt of hij grab tag heeft
            {
                // stopt output van raycast in gameobject en zet de player als parent
                Currentgrab = hit.transform.gameObject;
                PickUp();
                hit.transform.parent = player.transform;

            }
        }
    }

    [ClientRpc]
    public void PickUp()
    {
        // zet grab op true, de rigidbody van het item op kinematic en zet het object op een set positie
        grab = true;
        Currentgrab.GetComponent<Rigidbody>().isKinematic = true;
        Currentgrab.transform.position = equipPos.transform.position;
    }
    public void Drop()
    {
        // currentgrab gameobject word geleegd, en de player word niet meer als parent gezien en zet rigidbody op niet kinematic
        Currentgrab.GetComponent<Rigidbody>().isKinematic = false;
        Currentgrab.transform.parent = null;
        Currentgrab = null;
        grab = false;
        
    }
}

