using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]private Animator anim;
    
    public void SetAnim()
    {
        anim.SetBool("Door", true);
    }
    public void SetAnimFalse()
    {
        anim.SetBool("Door", false);
    }
}
