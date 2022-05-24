using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class SceneReferenceEditor : EditorWindow
{
    public static string RegexExpression { get; }  = "([0-9a-f]){32}";
    public static RegexOptions regexOptions { get; } = RegexOptions.Multiline;

    public int MaxRows = 3000;
    public int editButtonSizeMod = 8;
    public string ScorePath;

    Vector2 scrollPos = Vector2.zero;
    MatchCollection guids;
    bool editMode = false;
    bool reload = true;
    GUIStyle overMaxStyle = new GUIStyle();

    [MenuItem("Tools/Experimental/View scene references")]
    public static void OpenViewer()
    {
        SceneReferenceEditor window = GetWindow<SceneReferenceEditor>("Reference editor [View]");
        window.ScorePath = EditorSceneManager.GetActiveScene().path;
        window.guids = Regex.Matches(File.ReadAllText(window.ScorePath), RegexExpression);
        window.reload = false;
    }
    [MenuItem("Tools/Experimental/Edit scene references")]
    public static void OpenEditor()
    {
        SceneReferenceEditor window = GetWindow<SceneReferenceEditor>("Reference editor [Edit]");
        window.ScorePath = EditorSceneManager.GetActiveScene().path;
        window.guids = Regex.Matches(File.ReadAllText(window.ScorePath), RegexExpression, regexOptions);
        window.editMode = true;
        window.reload = false;
    }
    private void OnEnable()
    {
        reload = true;
        Debug.Log("[SceneReferenceEditor] Reloading...");
    }

    private void OnGUI()
    {
        if (reload)
        {
            ScorePath = EditorSceneManager.GetActiveScene().path;
            guids = Regex.Matches(File.ReadAllText(ScorePath), RegexExpression, regexOptions);
            reload = false;
            Debug.Log("[SceneReferenceEditor] Reloaded!");
        }
        bool overMaxRows = false;
        if (!editMode)
        {
            scrollPos = GUILayout.BeginScrollView(scrollPos);
            //foreach (Match xmatch in guids)
            for (int i = 0; i < guids.Count; i++)
            {
                if (i >= MaxRows)
                {
                    overMaxRows = true;
                    break;
                }
                    
                Match xmatch = guids[i];
                GUILayout.BeginHorizontal();
                GUILayout.Label(new GUIContent(xmatch.Value), GUILayout.Width(184));
            
                GUILayout.Label(new GUIContent("->"), GUILayout.Width(32));
                string assetPath = AssetDatabase.GUIDToAssetPath(xmatch.Value);

                GUILayout.Label(new GUIContent(assetPath));
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();
            if (overMaxRows)
            {
                overMaxStyle.fontStyle = FontStyle.Bold;
                overMaxStyle.active.textColor = Color.red;
                //overMaxStyle.richText = true;
                GUILayout.Label(new GUIContent($"{guids.Count}/{MaxRows}"), overMaxStyle);
            }
            GUILayout.BeginHorizontal();
            Texture2D tex = UnityEditorInternal.InternalEditorUtility.GetIconForFile("Assets/Editor/MyCustomSettings.asset");
            GUILayout.Label(new GUIContent(tex), GUILayout.Width(256), GUILayout.Height(256));
            //Texture2D tex2 = UnityEditorInternal.InternalEditorUtility.GetIconForFile("Assets/Prefabs/BigSphere.prefab");
            //GUILayout.Label(new GUIContent(tex2), GUILayout.Width(256), GUILayout.Height(256));
            GUILayout.EndHorizontal();
        }
        else
        {

            List<Match> matches = guids.Cast<Match>().ToList();
            List<string> matchPaths = matches.Select(match => AssetDatabase.GUIDToAssetPath(match.Value)).ToList();

            int largestBox = matchPaths.OrderByDescending(pth => pth.Length).First().Length;

            scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.Width(192 + largestBox * editButtonSizeMod + 4));
            
            foreach (Match xmatch in guids)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(new GUIContent(xmatch.Value), GUILayout.Width(160));

                GUILayout.Label(new GUIContent("->"), GUILayout.Width(32));
                string assetPath = AssetDatabase.GUIDToAssetPath(xmatch.Value);

                GUIStyle tmpStyle = new GUIStyle();
                tmpStyle.border = new RectOffset(10,10,10,10);
                tmpStyle.alignment = TextAnchor.MiddleLeft;

                if (GUILayout.Button(new GUIContent(assetPath)))
                {
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();
        }
    }
}
