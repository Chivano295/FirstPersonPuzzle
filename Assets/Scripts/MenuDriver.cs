using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityUtils;
using System;

public class MenuDriver : MonoBehaviour
{
    [DictionaryView]
    public Dictionary<string, int> SceneLocations = new Dictionary<string, int>(); 
    [SerializeField]
    private int menuScenes;
    [SerializeField]
    private int creditsLoc;

    public SceneAsset Oof;

    private void Awake()
    {
        if (PlayerPrefsExt.GetInt("game.initialized", 0) == 0)
        {

        }
        SceneLocations.Add("main", 0);
        SceneLocations.Add("score", 1);
        SceneLocations.Add("credits", 4);
        SceneLocations.Add("settings", 3);
        SceneLocations.Add("game", 2);
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

    //public void ToStats()
    //{
    //    SceneManager.LoadScene(2);
    //}

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

    public void PopImage(GameObject popup)
    {
        popup.SetActive(true);
    }

    public void PauseGame()
    {
        //if (GameManager.Instance.IsPaused) UIManager.Instance.isHovering = false;
        //GameManager.Instance.HandlePause();
        //UIManager.Instance.ShowPause();
    }
}
