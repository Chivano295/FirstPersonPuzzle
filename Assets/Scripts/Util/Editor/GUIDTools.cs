using System.Windows;
using NUnit;
using UnityEngine;
using UnityEditor;

namespace UnityUtils.Editor
{
    public class GUIDTools : EditorWindow
    {
        static int guiMode;

        //Guid2Path
        string guidToSearch = "";
        bool noRes = false;
        bool autoFind = false;
        bool priv = false;
        
        //Path2Guid
        string pathToSearch = "";
        bool pathNoguid = false;
        bool gPriv = false;

        [MenuItem("Assets/Copy GUID")]
        public static void CopyGUID()
        {
            string guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(Selection.activeObject));
            Debug.Log(guid + " copied");
            GUIUtility.systemCopyBuffer = guid;
        }

        [MenuItem("Tools/GUID/Find by GUID _%#f")]
        static void FindByGUID()
        {
            guiMode = 0;
            EditorWindow.GetWindow<GUIDTools>("Finder");
        }
        [MenuItem("Tools/GUID/Path to GUID")]
        static void ChangeObjSceneGui()
        {
            guiMode = 1;
            EditorWindow.GetWindow<GUIDTools>("Path2Guid");
        }

        private void Reset()
        {
            priv = false;
            noRes = true;
        }

        private void OnGUI()
        {
            if (guiMode == 0)
            {
                
                string lastSearch = guidToSearch;
                guidToSearch = GUILayout.TextField(guidToSearch);
                if (guidToSearch != lastSearch) Reset();
                GUILayout.Space(2f);
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Find") || autoFind)
                {
                    priv = true;
                    if (ValidateGUIDAsset(guidToSearch))
                        noRes = false;
                    else
                        noRes = true;
                }
                //GUILayout.FlexibleSpace();
                autoFind = EditorGUILayout.Toggle(autoFind);
                GUILayout.EndHorizontal();
                GUILayout.Space(2f);
                if (!noRes && priv)
                {
                    GUILayout.Label("Location of asset: " + GetAssetPath(guidToSearch));
                }
                else if (noRes && priv)
                {
                    GUILayout.Label("No asset found with that GUID");
                }
            }
            else if (guiMode == 1)
            {
                string lastSearch = pathToSearch;
                pathToSearch = GUILayout.TextField(pathToSearch);
                if (pathToSearch != lastSearch) Reset();
                GUILayout.Space(2f);
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Get guid") || autoFind)
                {
                    gPriv = true;
                    if (ValidatePathAsset(pathToSearch))
                        pathNoguid = false;
                    else
                        pathNoguid = true;
                }
                //GUILayout.FlexibleSpace();
                autoFind = EditorGUILayout.Toggle(autoFind);
                GUILayout.EndHorizontal();
                GUILayout.Space(2f);
                if (!pathNoguid && gPriv)
                {
                    GUILayout.Label("Guid: " + GetGuidAtPath(pathToSearch));
                }
                else if (pathNoguid && gPriv)
                {
                    GUILayout.Label("?");
                }
            }
        }
        public bool ValidateGUIDAsset(string guid)
        {
            bool assetFound;
            assetFound = AssetDatabase.GUIDToAssetPath(guid) != "";
            return assetFound;
        }
        public bool ValidatePathAsset(string path)
        {
            bool assetFound;
            assetFound = AssetDatabase.AssetPathToGUID(path) != "";
            return assetFound;
        }

        public string GetAssetPath(string guid) => AssetDatabase.GUIDToAssetPath(guid);
        public string GetGuidAtPath(string path) => AssetDatabase.AssetPathToGUID(path); 
    }
}
