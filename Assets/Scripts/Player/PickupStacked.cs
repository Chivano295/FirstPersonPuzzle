using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupStacked : MonoBehaviour
{
    public Transform EquipPos;
    public Camera Cam;
    public GameObject Currentgrab;
    public int MaxHeldObjects = 1;

    [SerializeField] private float distance = 5;
    [SerializeField] private GameObject player;
    [SerializeField] private int strength = 1;

    private Stack<GameObject> grabbedItems = new Stack<GameObject>();
    private HashSet<IsGravityGun> activeGuns = new HashSet<IsGravityGun>();

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && grabbedItems.Count < MaxHeldObjects)
        {
            if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out RaycastHit hit, distance, ~LayerMask.GetMask("Weegschaal")))
            {
                if (hit.transform.tag == "Grab") //checkt of hij grab tag heeft
                {
                    //Check with a script as tag if it's a gravity gun
                    IsGravityGun gravityGun = hit.collider.GetComponent<IsGravityGun>();
                    if (gravityGun != null && activeGuns.Add(gravityGun))
                    {
                        strength++;
                        MaxHeldObjects++;
                    }

                    HeldInstruct instruct = hit.collider.GetComponent<HeldInstruct>();
                    if (instruct != null)
                    {
                        if (instruct.Weight > strength)
                            return;
                    }

                    PickUp(hit.transform.gameObject, instruct);
                    hit.transform.parent = player.transform;

                }
            }
        }
        if (grabbedItems.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
                Drop();
        }       
    }
    public void PickUp(GameObject target, HeldInstruct instruct = null)
    {
        // zet grab op true, de rigidbody van het item op kinematic en zet het object op een set positie
        if (target.TryGetComponent<Rigidbody>(out Rigidbody targetRb))
        {
            targetRb.isKinematic = true;
        }
        if (instruct != null)
        {
            instruct.RestoreScale = target.transform.localScale;
            instruct.RestoreRotation = target.transform.rotation;

            target.transform.position = EquipPos.transform.position + instruct.Offset;
            target.transform.rotation = instruct.Rotation;
            if (instruct.Scale != -Vector3.one) target.transform.localScale = instruct.Scale;
        }
        else
        {
            target.transform.position = EquipPos.transform.position;
        }
        
        grabbedItems.Push(target);
    }
    public void Drop()
    {
        // currentgrab gameobject word geleegd, en de player word niet meer als parent gezien en zet rigidbody op niet kinematic
        GameObject droppedObj = grabbedItems.Pop();
        IsGravityGun gun = droppedObj.GetComponent<IsGravityGun>();

        if (droppedObj.TryGetComponent<Rigidbody>(out Rigidbody targetRb))
        {
            targetRb.isKinematic = false;
        }
        if (gun != null && activeGuns.Remove(gun))
        {
            strength--;
            MaxHeldObjects--;
        }
        droppedObj.transform.parent = null;
        
    }
}

