using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scales : MonoBehaviour
{
    public WeegSchaalNT scaleSide1;
    public WeegSchaalNT scaleSide2;
    public Animator anim;
    public int difference;
    public float percentageDif;

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
        //Quaternion aa = Draaibalk.transform.localRotation;
        //aa.eulerAngles = new Vector3(aa.eulerAngles.x, aa.eulerAngles.y, rotation);
        //Draaibalk.transform.localRotation = aa;

        if (rotation != 0 && !updated)
        {
            lastRot = rotation;
            LeanTween.rotateZ(Draaibalk.gameObject, rotation, 10).setEaseInBounce();
        }

        //getal van objecten op beide weegschalen
        totalCount = scaleSide1.OBJcount + scaleSide2.OBJcount;

        if( percentageDif == 0)
        {
            Debug.Log("niets");
        }

       if(totalCount == 9 && percentageDif == 0)
        {
            anim.SetBool("Doorup", true);
        }

    }
}
