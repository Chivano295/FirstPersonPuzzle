using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;

using Text = TMPro.TextMeshProUGUI;

public class TwitchApiManaged : MonoBehaviour
{
    public string appId;
    public TwitchApi Twitch;

    public Text SigninText;

    private void Awake()
    {
        Twitch = new TwitchApi(appId);
        //Logger.Level = Logger.DebugLevel.Verbose;
        //Logger.Log(await Twitch.UserReadEmail());
        
    }

    public void ButtonTwitchAuth()
    {
        Twitch.AuthenticateWindow(NetworkBackendMode.HttpListener);
        SigninText.text = "Singing in...";
    }
}
