using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeegSchaal : MonoBehaviour
{
    public int WeegschaalWeight;
    public bool check = false;
    public TextMeshPro text;
   
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Grab")
        {
            check = true;
            WeegschaalWeight += other.gameObject.GetComponent<ObjectWeight>().weight;
            text.text = "" + WeegschaalWeight;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        WeegschaalWeight -= other.gameObject.GetComponent<ObjectWeight>().weight;
        text.text = "" + WeegschaalWeight;
    }
}
