using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WorkbenchSetup : EditorWindow
{
    private Pickup playerPickup;
    private List<Recipe> recipes;

    private GameObject defaultWorkbench;
    private GameObject defaultWbSlot;
    private GameObject craftingBaseGo = null;


    [MenuItem("Tools / Create workbench")]
    public static void CreateWindow()
    {
        EditorWindow window = GetWindow<WorkbenchSetup>();
        window.titleContent = new GUIContent("Create workbench");
    }

    private void OnGUI()
    {
        GUILayout.Label("Fill in the fields to make a workbench");
        GUILayout.Space(1);

        craftingBaseGo = (GameObject)EditorGUILayout.ObjectField(craftingBaseGo, typeof(GameObject), false);
    }
}
