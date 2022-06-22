using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private string DoorOpenSoundName = "door_slide_open";
    private string DoorCloseSoundName = "door_slide_close";
    [SerializeField]private Animator anim;
    
    public void SetAnim()
    {
        anim.SetBool("Door", true);
        AudioManager.Instance.Play(DoorOpenSoundName);
    }
    public void SetAnimFalse()
    {
        anim.SetBool("Door", false);
        AudioManager.Instance.Play(DoorCloseSoundName);
    }
}
