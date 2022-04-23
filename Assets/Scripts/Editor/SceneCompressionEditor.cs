using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class SceneCompressionEditor : EditorWindow
{
    [MenuItem("Tools / Scene Compressor")]
    public static void OpenCompressor()
    {
        GetWindow<SceneCompressionEditor>();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Compress current scene"))
        {
            Scene activeScene = EditorSceneManager.GetActiveScene();
            int progressId = Progress.Start($"Compressing scene {activeScene.name}");
            try
            {
                CompressedSceneLoader.CompressScene(activeScene.path);
            }
            catch (Exception)
            {
                Progress.Finish(progressId, Progress.Status.Failed);
                throw;
            }
            Logger.LogWarning("Compressed scenes will give unity an error message that it's corrupted, you can safely ignore this");
            Progress.Finish(progressId, Progress.Status.Succeeded);
        }
        if (GUILayout.Button("Decompress current scene"))
        {
            int progressId = Progress.Start("Running one task");
            string scenePath = EditorSceneManager.GetActiveScene().path;
            Logger.Log(scenePath);
            Progress.Report(progressId, 50f);
            string pathNoExtension = scenePath.Remove(scenePath.LastIndexOf('.'));
            try
            {
                CompressedSceneLoader.DecompressScene(string.Concat(pathNoExtension, "-compressed.unis"));
                Progress.Finish(progressId, Progress.Status.Succeeded);
            }
            catch
            {
                Progress.Finish(progressId, Progress.Status.Failed);
            }
        }
    }
}
