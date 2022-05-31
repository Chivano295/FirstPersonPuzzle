using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformPrefetch : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_STANDALONE_LINUX
        SceneManager.LoadScene(2);
#else
        SceneManager.LoadScene(1);
#endif

    }
}
