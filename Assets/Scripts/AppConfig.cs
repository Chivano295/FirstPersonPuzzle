using System;
using System.Collections.Generic;
using Newtonsoft.Json;
public class AppConfig
{
    [JsonProperty("twitch.clientId")]
    public string TwitchClientId;
    [JsonProperty("twitch.clientSecret")]
    public string TwitchClientSecret;
}
