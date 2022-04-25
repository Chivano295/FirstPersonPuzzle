using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

using Text = TMPro.TextMeshProUGUI;

public class TwitchApiManaged : MonoBehaviour
{
    public string AppId;
    public string AppSecret;
    public TwitchApi Twitch;

    public TextAsset ConfigFile;

    public Text SigninText;
    public Text AccountNameText;

    private bool authInProgress;
    private AppConfig apCon;

    private void Awake()
    {
        apCon = JsonConvert.DeserializeObject<AppConfig>(ConfigFile.text);
        Twitch = new TwitchApi(apCon.TwitchClientId, apCon.TwitchClientSecret);
    }

    public async void ButtonTwitchAuth()
    {
        SigninText.text = "Signing in...";
        if (await Twitch.AuthenticateWindow(NetworkBackendMode.HttpListener))
        {
            SigninText.text = "Sign in success";
            TwitchUser current = await Twitch.ReadCurrentUser();
            AccountNameText.text = current.DisplayName;
        }
        else
        { 
            SigninText.text = "Sign in failed";
        }
    }
}
