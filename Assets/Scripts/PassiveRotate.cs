using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveRotate : MonoBehaviour
{
    public Vector3 RotateDir = Vector3.up;
    public float SpeedMultiplier = 1f;

    private void Update()
    {

        //if (!LeanTween.isTweening(gameObject)) 
        //    LeanTween.rotateY(gameObject,  transform.rotation.eulerAngles.y + RotateDir.y * SpeedMultiplier, 0.1f);
        transform.Rotate(Vector3.up * SpeedMultiplier * Time.deltaTime, Space.World);

    }
}
