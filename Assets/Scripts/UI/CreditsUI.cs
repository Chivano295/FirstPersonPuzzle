using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;

public class CreditsUI : MonoBehaviour
{
    public GameObject ToRotate;
    public void Spin()
    {
        LeanTween.rotateAround(ToRotate, Vector3.forward, 60, 1f).setEase(LeanTweenType.easeInOutElastic);
    }
}
