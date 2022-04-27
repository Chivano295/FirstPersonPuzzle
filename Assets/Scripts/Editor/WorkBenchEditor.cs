using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WorkbenchSlot))]
[CanEditMultipleObjects]
public class WorkBenchEditor : Editor
{
    bool showDebugFeeder = false;
    CraftingRecipeMaterial matIn;
    public override void OnInspectorGUI()
    {
        if (Application.isPlaying && GUILayout.Button(!showDebugFeeder ? "Show" : "Hide"))
        {
            showDebugFeeder = !showDebugFeeder;
        }
        if (showDebugFeeder)
        {
            matIn = (CraftingRecipeMaterial)EditorGUILayout.ObjectField(matIn, typeof(CraftingRecipeMaterial), true);
            if (matIn != null && GUILayout.Button("Trigger"))
            {

            }
        }
        base.OnInspectorGUI();
    }
}

