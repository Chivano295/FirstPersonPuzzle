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
    public static RegexOptions RegexOptions { get; } = RegexOptions.Multiline;

    public int MaxRows = 1000;
    public int editButtonSizeMod = 8;
    public string ScorePath;

    Vector2 scrollPos = Vector2.zero;
    MatchCollection guids;
    HashSet<Match> guidsNoDupe;
    bool editMode = false;
    bool reload = true;
    bool performance = false;
    GUIStyle overMaxStyle = new GUIStyle();

    [MenuItem("Tools/Experimental/View scene references")]
    public static void OpenViewer()
    {
        SceneReferenceEditor window = GetWindow<SceneReferenceEditor>("Reference editor [View]");
        window.ScorePath = EditorSceneManager.GetActiveScene().path;
        window.guidsNoDupe = new HashSet<Match>();
        window.guids = Regex.Matches(File.ReadAllText(window.ScorePath), RegexExpression);
        for (int i = 0; i < window.guids.Count; i++)
        {
            window.guidsNoDupe.Add(window.guids[i]);
        }
        window.reload = false;
    }
    [MenuItem("Tools/Experimental/Edit scene references")]
    public static void OpenEditor()
    {
        SceneReferenceEditor window = GetWindow<SceneReferenceEditor>("Reference editor [Edit]");
        window.ScorePath = EditorSceneManager.GetActiveScene().path;
        window.guids = Regex.Matches(File.ReadAllText(window.ScorePath), RegexExpression, regexOptions);
        for (int i = 0; i < window.guids.Count; i++)
        {
            window.guidsNoDupe.Add(window.guids[i]);
        }
        window.editMode = true;
        window.reload = false;
    }
    private void OnEnable()
    {
        if (!reload)
            Debug.Log("[SceneReferenceEditor] Reloading...");
        reload = true;

    }

    private void OnGUI()
    {
        if (reload)
        {
            ScorePath = EditorSceneManager.GetActiveScene().path;
            guids = Regex.Matches(File.ReadAllText(ScorePath), RegexExpression, regexOptions);
            guidsNoDupe = new HashSet<Match>();
            for (int i = 0; i < guids.Count; i++)
            {
                guidsNoDupe.Add(guids[i]);
            }
            reload = false;
            Debug.Log("[SceneReferenceEditor] Reloaded!");
        }
        bool overMaxRows = false;
        if (!editMode)
        {
            scrollPos = GUILayout.BeginScrollView(scrollPos);
            int i = 0;
            foreach (Match xmatch in guidsNoDupe)
            //for (int i = 0; i < guids.Count; i++)
            {
                if (i >= MaxRows)
                {
                    overMaxRows = true;
                    if (performance) break;
                }

                //Match xmatch = guidsNoDupe[i];
                GUILayout.BeginHorizontal();
                GUILayout.Label(new GUIContent(xmatch.Value), GUILayout.Width(184));
            
                GUILayout.Label(new GUIContent("->"), GUILayout.Width(32));
                string assetPath = AssetDatabase.GUIDToAssetPath(xmatch.Value);

                GUILayout.Label(new GUIContent(assetPath));
                GUILayout.EndHorizontal();
                i++;
            }
            GUILayout.EndScrollView();
            Texture2D tex = UnityEditorInternal.InternalEditorUtility.GetIconForFile(ScorePath);
            GUILayout.BeginHorizontal();
            GUILayout.Label(new GUIContent(tex), GUILayout.Width(32), GUILayout.Height(32));
            if (overMaxRows)
            {
                overMaxStyle.fontStyle = FontStyle.Bold;
                overMaxStyle.normal.textColor = Color.red;
                overMaxStyle.alignment = TextAnchor.MiddleLeft;
                //overMaxStyle.richText = true;
                GUILayout.Label(new GUIContent($"{guidsNoDupe.Count}/{MaxRows}"), overMaxStyle);
            }
            else
            {
                GUILayout.Label(new GUIContent($"{guidsNoDupe.Count}/{MaxRows}"));
            }
            performance = GUILayout.Toggle(performance, new GUIContent("Cap shown references"));
            if (performance)
            {
                MaxRows = EditorGUILayout.IntField("Max references shown", MaxRows);
            }
            else 
            { 
                GUILayout.Box("                    ");
            }
            GUILayout.Label(new GUIContent("\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t"));
            //Texture2D tex2 = UnityEditorInternal.InternalEditorUtility.GetIconForFile("Assets/Prefabs/BigSphere.prefab");
            //GUILayout.Label(new GUIContent(tex2), GUILayout.Width(256), GUILayout.Height(256));
            GUILayout.EndHorizontal();
        }
        //Edit mode
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
            Texture2D tex = UnityEditorInternal.InternalEditorUtility.GetIconForFile(ScorePath);
            GUILayout.BeginHorizontal();
            GUILayout.Label(new GUIContent(tex), GUILayout.Width(32), GUILayout.Height(32));
            if (overMaxRows)
            {
                overMaxStyle.fontStyle = FontStyle.Bold;
                overMaxStyle.normal.textColor = Color.red;
                overMaxStyle.alignment = TextAnchor.MiddleLeft;
                //overMaxStyle.richText = true;
                GUILayout.Label(new GUIContent($"{guidsNoDupe.Count}/{MaxRows}"), overMaxStyle);
            }
            else
            {
                GUILayout.Label(new GUIContent($"{guidsNoDupe.Count}/{MaxRows}"));
            }
            performance = GUILayout.Toggle(performance, new GUIContent("Cap shown references"));
            if (performance)
            {
                MaxRows = EditorGUILayout.IntField("Max references shown", MaxRows);
            }
            else
            {
                GUILayout.Box("                    ");
            }
            GUILayout.Label(new GUIContent("\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t"));
            //Texture2D tex2 = UnityEditorInternal.InternalEditorUtility.GetIconForFile("Assets/Prefabs/BigSphere.prefab");
            //GUILayout.Label(new GUIContent(tex2), GUILayout.Width(256), GUILayout.Height(256));
            GUILayout.EndHorizontal();
        }
    }
}
