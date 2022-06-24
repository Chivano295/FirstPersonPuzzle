using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeegSchaal : MonoBehaviour
{
    public int WeegschaalWeight;
    public bool check = false;
    public TextMeshPro text;
    public int NeededWeight;
    public DoorTrigger trigger;
    private string DoorOpenSoundName = "door_slide_open";
    

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Grab")
        {
            check = true;
            WeegschaalWeight += other.gameObject.GetComponent<ObjectWeight>().weight;
            text.text = "" + WeegschaalWeight;
            if(WeegschaalWeight == NeededWeight)
            {
                trigger.SetAnim();
            }
          
        }
    }
    public void OnTriggerExit(Collider other)
    {
        var weight = other.gameObject.GetComponent<ObjectWeight>();
        if (weight == null) return;
        WeegschaalWeight -= weight.weight;
        text.text = "" + WeegschaalWeight;
        if (WeegschaalWeight != NeededWeight)
        {
            AudioManager.Instance.Play(DoorOpenSoundName);
            trigger.SetAnimFalse();
        }
    }
}
