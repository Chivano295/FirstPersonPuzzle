using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class ABTrack : MonoBehaviour
{
    public Vector3 Begin;
    public Vector3 End;

    public bool SetBeginToCurrentPosition = false;

    private void Update()
    {
        if (SetBeginToCurrentPosition) Begin = transform.position;
    }
}