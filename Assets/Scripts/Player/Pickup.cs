using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Transform equipPos;
    public Camera cam;
    [SerializeField] private float distance;
    [SerializeField] private bool grab;
    [SerializeField] private GameObject currentgrab;
    [SerializeField] private GameObject player;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentgrab == null)
        {
            Debug.Log("ik doe iets");
            RaycastHit hit; // schiet raycast vanuit camera naar waar je kijkt
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance))
            {
                if (hit.transform.tag == "Grab") //checkt of hij grab tag heeft
                {                    
                    // stopt output van raycast in gameobject en zet de player als parent
                    currentgrab = hit.transform.gameObject;
                    PickUp();
                    hit.transform.parent = player.transform;

                }
            }                                                   
        }
        //roept drop functie aan
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
            
    }
    public void PickUp()
    {
        // zet grab op true, de rigidbody van het item op kinematic en zet het object op een set positie
        grab = true;
        currentgrab.GetComponent<Rigidbody>().isKinematic = true;
        currentgrab.transform.position = equipPos.transform.position;
    }
    public void Drop()
    {
        // currentgrab gameobject word geleegd, en de player word niet meer als parent gezien en zet rigidbody op niet kinematic
        currentgrab.GetComponent<Rigidbody>().isKinematic = false;
        currentgrab.transform.parent = null;
        currentgrab = null;
        grab = false;
        
    }
}

