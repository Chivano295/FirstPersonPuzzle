using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun : MonoBehaviour
{
    //The position to place objects at
    public Transform HoverPos;
    //Parent of HoverPos to rotate relative to player 
    public Transform HoverAncor;
    public Camera Cam;
    public GameObject Currentgrab;
    public int MaxHeldObjects = 1;

    //Limits vertical movement 
    public float MinLook = 30f;
    public float MaxLook = 60f;

    [SerializeField] private float distance = 5;
    [SerializeField] private GameObject player; 
    [SerializeField] private int strength = 1;
    [SerializeField] private float   smoothTime = 0.2f;
    [SerializeField] private float angleOffset = 90f;

    private Stack<GameObject> grabbedItems = new Stack<GameObject>();

    private void Update()
    {
        if (Currentgrab != null && !LeanTween.isTweening(Currentgrab))
        {
            LeanTween.move(Currentgrab, HoverPos.position, smoothTime).setEaseOutCubic();
        }
        if (!LeanTween.isTweening(HoverAncor.gameObject))
        {
            Vector3 rot = Cam.transform.localRotation.eulerAngles;
            float angle = rot.x;
            if (angle >= 180f && angle < 360f + MinLook)
            {
                angle = MinLook;
            }
            else if (angle < 180f && angle > MaxLook)
            {
                angle = MaxLook;
            }
            rot.x = angle;
            //rot.x = Mathf.Clamp(rot.x - angleOffset, WrapAngle(MinLook), MaxLook);
            LeanTween.rotateLocal(HoverAncor.gameObject, rot, smoothTime);
        }
        if (Input.GetKeyDown(KeyCode.E) && Currentgrab == null)
        {
            RaycastHit hit; // schiet raycast vanuit camera naar waar je kijkt
            if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, distance))
            {
                if (hit.transform.tag == "Grab") //checkt of hij grab tag heeft
                {
                    PickUp(hit.transform.gameObject);
                    //hit.transform.parent = player.transform;

                }
            }
        }
        if (Currentgrab != null && Input.GetKeyDown(KeyCode.Q))
            Drop();
   
    }

    public void PickUp(GameObject target, HeldInstruct instruct = null)
    {
        // zet grab op true, de rigidbody van het item op kinematic en zet het object op een set positie
        if (target.TryGetComponent<Rigidbody>(out Rigidbody targetRb))
        {
            targetRb.isKinematic = true;
        }
        LeanTween.move(target, HoverPos.position, smoothTime).setEaseOutCubic();
        Vector3 rot = Cam.transform.localRotation.eulerAngles;
        rot.x = Mathf.Clamp(rot.x, MinLook, MaxLook);
        LeanTween.rotateLocal(HoverAncor.gameObject, rot, 0.2f);


        Currentgrab = target;
    }
    public void Drop()
    {
        Collider col = Currentgrab.GetComponent<Collider>();
        col.isTrigger = true;

        //RaycastHit hit; // schiet raycast vanuit camera naar waar je kijkt
        //if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, distance, 0xFFFF, QueryTriggerInteraction.Ignore))
        //{
            
        //}

        if (Currentgrab.TryGetComponent<Rigidbody>(out Rigidbody targetRb))
        {
            targetRb.isKinematic = false;
        }

        col.isTrigger = false;
        Currentgrab = null;
    }

    private float WrapAngle(float angle)
    {
        float o = angle;
        if (angle < 0)
        {
            o = 360 - angle;
        }
        else if (angle > 360)
        {
            o = angle - 360;
        }
        return o;
    }
}
