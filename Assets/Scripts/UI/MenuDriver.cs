﻿using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityUtils;
using System;
using System.Linq;
using System.Text;

public class MenuDriver : MonoBehaviour
{
    //public SceneAsset SceneAss;
    public string SceneAssStr;
    public string FlagsPath => Path.Combine(Application.dataPath, "flags");

    [DictionaryView]
    public Dictionary<string, int> SceneLocations = new Dictionary<string, int>();
    [SerializeField]
    private int menuScenes;
    [SerializeField]
    private int creditsLoc;

    [SerializeField] private float zR;
    private GameObject aaaaa;

    private void Awake()
    {
        SceneLocations.Add("main", 1);
        SceneLocations.Add("credits", 4);
        SceneLocations.Add("settings", 5);
        SceneLocations.Add("game", 2);
        SceneLocations.Add("gameNoCut", 3);
    }

    public void PlayButton()
    {
        PlayerPrefs.SetInt("hasPlayed", 1);
        //Global.playAmount += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + menuScenes);
    }

    public void QuitButton()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit(0);
#endif
    }

    public void BackButton()
    {
        SceneManager.LoadScene(0);
    }
    [Obsolete("use GoTo(String) instead")]
    public void ToCredits()
    {
        SceneManager.LoadScene(1);
    }
    [Obsolete("use GoTo(String) instead")]
    public void ToMain()
    {
        //if (GameManager.Instance != null) GameManager.Instance.HandlePause();
        SceneManager.LoadScene(0);
    }

    [Obsolete("use GoTo(String) instead")]
    public void ToSettings()
    {
        SceneManager.LoadScene(3);
    }

    public void GoTo(string destination)
    {
        if (SceneLocations.TryGetValue(destination, out int targetIndex))
        {
            SceneManager.LoadScene(targetIndex);
        }
        else
        {
            Debug.LogWarning(string.Format("Scene with name {0}", destination));
        }
    }

    public void PauseGame()
    {

    }

    public void SetFlag(string crossSceneFlag)
    {
        FileStream fs = new FileStream(FlagsPath, FileMode.OpenOrCreate);
        StreamReader sr = new StreamReader(fs, Encoding.UTF8, false, 1024, true);

        string flagsStr = sr.ReadToEnd();
        flagsStr = flagsStr.TrimEnd(',');
        string[] flags = flagsStr.Split(',');

        HashSet<string> uniqFlags = new HashSet<string>();
        foreach (var flag in flags)
        {
            if (flag == string.Empty) continue;
            uniqFlags.Add(flag);
        }
        uniqFlags.Add(crossSceneFlag);

        string[] exportFlags = new string[uniqFlags.Count];
        uniqFlags.CopyTo(exportFlags);

        sr.Close();
        StreamWriter sw = new StreamWriter(fs, Encoding.UTF8, 1024, true);
        sw.Write(string.Join(",", exportFlags));

        sw.Close();
        fs.Close();
    }

    public bool GetFlag(string flag)
    {
        try
        {
            FileStream fs = new FileStream(FlagsPath, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8, false, 1024, true);

            string flagsStr = sr.ReadToEnd();
            flagsStr = flagsStr.TrimEnd(',');
            string[] flags = flagsStr.Split(',');

            HashSet<string> uniqFlags = new HashSet<string>();
            foreach (var flagChj in flags)
            {
                if (flagChj == string.Empty) continue;
                if (flagChj == flag) continue;
                uniqFlags.Add(flag);
            }

            string[] exportFlags = new string[uniqFlags.Count];
            uniqFlags.CopyTo(exportFlags);

            sr.Close();
            fs.Close();
            fs = new FileStream(FlagsPath, FileMode.Truncate);

            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8, 1024, true);
            sw.Write(string.Join(",", exportFlags));

            sw.Close();
            fs.Close();

            return flags.Contains(flag);

        }
        catch(FileNotFoundException)
        {
            return false;
        }
    }
}
