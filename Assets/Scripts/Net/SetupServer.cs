using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SetupServer : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_EDITOR_LINUX
        Debug.Log("Starting server");
        NetworkManager.singleton.StartServer();
#endif
    }
}
