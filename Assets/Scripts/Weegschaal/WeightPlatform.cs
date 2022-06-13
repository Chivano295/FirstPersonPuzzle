using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeightPlatform : MonoBehaviour
{
    public int WeegschaalWeight;
    public bool check = false;
    public TextMeshPro text;
    public GameObject platform;
    public float platformMP;
    [SerializeField]private float moveTime;


    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Grab")
        {
            check = true;
            WeegschaalWeight += other.gameObject.GetComponent<ObjectWeight>().weight;
            text.text = "" + WeegschaalWeight;
            PosUpdate();
            
        }
    }
    public void OnTriggerExit(Collider other)
    {
        WeegschaalWeight -= other.gameObject.GetComponent<ObjectWeight>().weight;
        text.text = "" + WeegschaalWeight;
        PosUpdate();


    }

    private void PosUpdate()
    {
        //platform.transform.localPosition = new Vector3(0, WeegschaalWeight * platformMP, 0);
        LeanTween.moveLocalY(platform, WeegschaalWeight * platformMP, moveTime).setEaseOutCubic();
       





    }
}
