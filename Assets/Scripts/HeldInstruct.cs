using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldInstruct : MonoBehaviour
{
    public Vector3 Offset;
    public Quaternion Rotation;
    public Vector3 Scale;
    public int Weight = 1;

    [HideInInspector]
    public Vector3 RestoreOffset;
    [HideInInspector]
    public Quaternion RestoreRotation;
    [HideInInspector]
    public Vector3 RestoreScale;

}
