using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scales : MonoBehaviour
{
    public WeegSchaalNT scaleSide1;
    public WeegSchaalNT scaleSide2;
    public Animator anim;
    public int difference;
    public float percentageDif;

    public TextMeshPro objCount;

    public Transform Draaibalk;

    public float rotation;
    public int degreeMP;

    public int totalCount;

    float lastRot = 0;
    bool updated => lastRot == rotation;


    public void Update()
    {
        //regelt hoe zwaar alles is
        if(scaleSide1.WeegschaalWeight > scaleSide2.WeegschaalWeight)
        {
            difference = scaleSide1.WeegschaalWeight - scaleSide2.WeegschaalWeight;

            percentageDif = (float)difference / (float)scaleSide2.WeegschaalWeight;
           
        }
        
        if (scaleSide2.WeegschaalWeight > scaleSide1.WeegschaalWeight)
        {
            difference = scaleSide1.WeegschaalWeight - scaleSide2.WeegschaalWeight;

            percentageDif = (float)difference / (float)scaleSide1.WeegschaalWeight;
            
        }
        if(scaleSide2.WeegschaalWeight == scaleSide1.WeegschaalWeight)
        {
            percentageDif = 0;
        }
        //checkt of het infinity is, zoja is infity == 0
        if (float.IsInfinity(percentageDif))
        {
            percentageDif = 0;
        }

        rotation = percentageDif * degreeMP;
        //doet de daadwerkelijke rotatie
       

        if (rotation != 0 && !updated)
        {
            lastRot = rotation;
            LeanTween.rotateZ(Draaibalk.gameObject, rotation, 10).setEaseOutCubic();
        }

        //getal van objecten op beide weegschalen
        totalCount = scaleSide1.OBJcount + scaleSide2.OBJcount;

       
       if(totalCount == 9 && percentageDif == 0)
        {
            anim.SetBool("Door", true);
            objCount.color = Color.green;
        }

       if(totalCount != 9)
        {
            objCount.color = Color.red;
        }

        objCount.text = "" + totalCount;

        
    }
}
