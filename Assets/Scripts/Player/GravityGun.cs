using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun : MonoBehaviour
{
    public Transform HoverPos;
    public Camera Cam;
    public GameObject Currentgrab;
    public int MaxHeldObjects = 1;

    [SerializeField] private float distance = 5;
    [SerializeField] private GameObject player;
    [SerializeField] private int strength = 1;
    [SerializeField] private float   smoothTime = 0.2f;

    private Stack<GameObject> grabbedItems = new Stack<GameObject>();
    private HashSet<IsGravityGun> activeGuns = new HashSet<IsGravityGun>();

    private void Update()
    {
        if (Currentgrab != null && !LeanTween.isTweening(Currentgrab))
        {
            LeanTween.move(Currentgrab, HoverPos.position, smoothTime).setEaseOutCubic();
        }
        if (Input.GetKeyDown(KeyCode.E) && grabbedItems.Count < MaxHeldObjects)
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
        LeanTween.rotate(target, Cam.transform.rotation.eulerAngles, 0.2f);


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


        Currentgrab = null;
    }
}
