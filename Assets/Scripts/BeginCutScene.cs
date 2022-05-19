using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginCutScene : MonoBehaviour
{
    public string scene;
    public bool canSwitch = false;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canSwitch == true)
        {
            ChangeScene(); 
        }
    }
    public void ChangeScene()
    {
        
        SceneManager.LoadScene(scene);

    }
    public void UpdateButton()
    {
        canSwitch = true;
    }
}
