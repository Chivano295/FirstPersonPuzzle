using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimationManager)), CanEditMultipleObjects]
public class AnimationManEditor : Editor
{
    protected virtual void OnSceneGUI()
    {
        AnimationManager example = (AnimationManager)target;

        EditorGUI.BeginChangeCheck();
        Vector3 newTargetPosition = Handles.PositionHandle(example.SaveButtonsLocation, Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(example, "Change SaveButtons ending location");
            example.SaveButtonsLocation = newTargetPosition;
        }
    }
}
[CustomEditor(typeof(ABTrack)), CanEditMultipleObjects]
public class ABEditor : Editor
{
    bool pressed = false;

    public override void OnInspectorGUI()
    {
        
        pressed = GUILayout.Toggle(pressed, "Show AB Handles", "Button");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Set begin to current position"))
        {
            ABTrack track = (ABTrack)target;
            track.Begin = track.gameObject.transform.position;
        }
        if (GUILayout.Button("Set end to current position"))
        {
            ABTrack track = (ABTrack)target;
            track.End = track.gameObject.transform.position;
        }
        if (GUILayout.Button("Snap to begin"))
        {
            ABTrack track = (ABTrack)target;
            track.gameObject.transform.position = track.Begin;
        }

        GUILayout.EndHorizontal();
        base.OnInspectorGUI();
        //OnSceneGUI();
    }

    protected virtual void OnSceneGUI()
    {
        ABTrack example = (ABTrack)target;
        if (pressed)
        {
            EditorGUI.BeginChangeCheck();
            Vector3 newBeginPosition = Handles.PositionHandle(example.Begin, Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(example, "Change AB begin location");
                example.Begin = newBeginPosition;
            }
            EditorGUI.BeginChangeCheck();
            Vector3 newEndPosition = Handles.PositionHandle(example.End, Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(example, "Change AB end location");
                example.End = newEndPosition;
            }
        }
    }
}
