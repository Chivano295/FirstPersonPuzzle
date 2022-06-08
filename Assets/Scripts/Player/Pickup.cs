using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Transform equipPos;
    public Camera cam;
    public GameObject Currentgrab;
    public bool GravityGunHeld = false;
    [SerializeField] private float distance;
    [SerializeField] private bool grab;
    [SerializeField] private GameObject player;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && Currentgrab == null)
        {
            Debug.Log("ik doe iets");
            RaycastHit hit; // schiet raycast vanuit camera naar waar je kijkt
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance))
            {
                if (hit.transform.tag == "Grab") //checkt of hij grab tag heeft
                {                    
                    // stopt output van raycast in gameobject en zet de player als parent
                    Currentgrab = hit.transform.gameObject;
                    PickUp();
                    if (hit.collider.GetComponent<IsGravityGun>())
                        GravityGunHeld = true;
                    hit.transform.parent = player.transform;

                }
            }                                                   
        }
        //roept drop functie aan
        if(Currentgrab != null)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                GravityGunHeld = false;
                Drop();
            }
        }
      
            
    }
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

