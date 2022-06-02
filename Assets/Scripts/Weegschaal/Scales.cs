using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scales : MonoBehaviour
{
    public WeegSchaalNT scaleSide1;
    public WeegSchaalNT scaleSide2;
    public int difference;
    public float percentageDif;

    public Transform Draaibalk;

    public float rotation;
    public int degreeMP;


   
    public void Update()
    {
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
        rotation = percentageDif * degreeMP;

        Quaternion aa = Draaibalk.transform.localRotation;
        aa.eulerAngles = new Vector3(aa.eulerAngles.x, aa.eulerAngles.y, rotation);
        Draaibalk.transform.localRotation = aa;

        
    }
}
