using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public GameObject Gun;
    public GameObject Held;
    public GameObject Player;
    public Transform EquipPos;
    public float PickupRange = 5f;

    [SerializeField] private Pickup pickupPlayer;
    [SerializeField] private Camera mainCam;
    
    private void Awake()
    {
        if (mainCam == null) mainCam = Camera.main;
    }

    private void Update()
    {
        if (pickupPlayer == null) return;
        if (Held == null && Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit; // schiet raycast vanuit camera naar waar je kijkt
            if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, PickupRange))
            {
                if (hit.transform.GetComponent<PickupByGravityGun>() != null) //checkt of hij grab tag heeft
                {
                    // stopt output van raycast in gameobject en zet de player als parent
                    Held = hit.transform.gameObject;
                    PickUp();
                    hit.transform.parent = Player.transform;

                }
            }
        }
    }

    public void PickUp()
    {
        // zet grab op true, de rigidbody van het item op kinematic en zet het object op een set positie
        Held.GetComponent<Rigidbody>().isKinematic = true;
        Held.transform.position = EquipPos.transform.position;
    }
    public void Drop()
    {
        // currentgrab gameobject word geleegd, en de player word niet meer als parent gezien en zet rigidbody op niet kinematic
        Held.GetComponent<Rigidbody>().isKinematic = false;
        Held.transform.parent = null;
        Held = null;
    }
}
