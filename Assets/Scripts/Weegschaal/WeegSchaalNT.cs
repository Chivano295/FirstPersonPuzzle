using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeegSchaalNT : MonoBehaviour
{
    public int WeegschaalWeight;
    public bool check = false;
    public TextMeshPro text;
    public int NeededWeight;
    public int OBJcount;
    public Scales scales;
    
   
   
    public void OnTriggerEnter(Collider other)
    {
        
        
            if (other.transform.tag == "Grab")
            {
                check = true;
                WeegschaalWeight += other.gameObject.GetComponent<ObjectWeight>().weight;
                text.text = "" + WeegschaalWeight;
                OBJcount += 1;
            
            }
       
        
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Grab")
        {
            WeegschaalWeight -= other.gameObject.GetComponent<ObjectWeight>().weight;
            text.text = "" + WeegschaalWeight;
            OBJcount -= 1;
            
        }


    }
}
