using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveRotate : MonoBehaviour
{
    public Vector3 RotateDir = Vector3.up;
    public float SpeedMultiplier = 1f;

    private void Update()
    {
        transform.Rotate(RotateDir * SpeedMultiplier * Time.deltaTime);
    }
}
