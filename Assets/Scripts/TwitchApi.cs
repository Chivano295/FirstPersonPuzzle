using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using Flurl;
using Flurl.Http;

/// <summary>
/// Class for interacting with Twitch Api
/// </summary>
public class TwitchApi
{
    //Port to listen locally for redirect
    public const int TCPPORT = 8888;//5576;
    //Endpoint mapping
    public Dictionary<string, string> Endpoints { get; private set; }  = new Dictionary<string, string>()
    {
        { "auth", "oauth2/authorize" },
        { "get_user", "/helix/users" }
    };

    //Twitter base endpoint for calls
    public Uri TwitchBase = new Uri("https://api.twitch.tv/");
    public string TwitchAuthorizationBase = "https://id.twitch.tv/";

    //Client for interacting with api
    private HttpClient client;
    //Listener for HttpListener mode
    private HttpListener listener;
    //Listener for TcpListener mode
    private TcpListener tcp;
    //The clientId passed in the constructor
    private string clientId;
    //Determines whether the current instance is authorized
    private bool authorized = false;

    //Creds
    private string clientSecret;
    private string accessCode;
    private string token;
    
    // Constructs a new instance to interact with the API with a specific application
    public TwitchApi(string _clientId, string _clientSecret)
    {
        this.clientId = _clientId;
        this.clientSecret = _clientSecret; 
        //Initalize instances 
        client = new HttpClient();
        client.BaseAddress = TwitchBase;
        listener = new HttpListener();
        //Add prefix to listen on
        listener.Prefixes.Add($"http://localhost:{TCPPORT}/");

        //listener only for Tcp mode (not activly maintained)
        tcp = new TcpListener(IPAddress.Loopback, TCPPORT);
    }

    public async Task<bool> AuthenticateWindow(NetworkBackendMode _backendMode = NetworkBackendMode.HttpListener)
    {
        //Open the browser for authorization
        Process.Start(
            $"{TwitchAuthorizationBase}{Endpoints["auth"]}?client_id={clientId}&response_type=code&force_verify=true&scope=user:read:email&redirect_uri=http://localhost:{TCPPORT}"
            );

        //HTML for response
        string responseString =
            "<!DOCTYPE html>\n<html>\n<head>\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>\n</head>\n<body>\nPlease return to the app.\n</body>\n</html>";
        //Bytes of the HTML
        byte[] buffer = Encoding.UTF8.GetBytes(responseString);
        //TcpSocket mode deprecated and may be removed
        if (_backendMode == NetworkBackendMode.TcpSocket)
        {
            tcp.Start();

            var tcpClient = await tcp.AcceptSocketAsync();
            int bytesSent = 0;
            if ((bytesSent = tcpClient.Send(buffer)) != buffer.Length)
            {
                Logger.LogError($"Transfering {buffer.Length} failed!");
            }

            Logger.LogVerbose($"Net stat: {bytesSent} sent");
            tcpClient.Disconnect(false);

            tcp.Stop();
        }
        else
        {
            //Start to listen
            listener.Start();
            //Await an connection
            var context = await listener.GetContextAsync();
            var response = context.Response;

            response.ContentLength64 = buffer.Length;
            var responseOutput = response.OutputStream;
            await responseOutput.WriteAsync(buffer, 0, buffer.Length);
            responseOutput.Close();

            string errString = context.Request.QueryString.Get("error");
            if (errString is object)
            {
                if (errString == "access_denied")
                {
                    Logger.LogWarning("User declined");
                    listener.Stop();
                    return false;
                }
                Logger.LogError($"Failed error: {errString}");
                listener.Stop();
                return false;
            }

            accessCode = context.Request.QueryString.Get("code");

            HttpRequestMessage tokenMessage = new HttpRequestMessage();
            tokenMessage.Method = HttpMethod.Post;
            tokenMessage.RequestUri = new Uri($"{TwitchAuthorizationBase}oauth2/token" +
                $"?client_id={clientId}&code={accessCode}&client_secret={clientSecret}&grant_type=authorization_code&redirect_uri=http://localhost:8888");

            HttpResponseMessage tokenResponse = await client.SendAsync(tokenMessage);
            token = (await tokenResponse.Content.ReadAsStringAsync()).Split('\"')[3];
            
            if (tokenResponse.StatusCode != HttpStatusCode.OK)
            {
                Logger.LogError("Failed getting token");
                return false;
            }

            listener.Stop();
            
            Logger.Log(response.StatusCode);

        }
        authorized = true;
        return true;
    }

    public void ValidateToken()
    { }

    public async Task<TwitchUser> ReadCurrentUser()
    {
        //Check if user is authenticated
        if (!authorized)
        {
            Logger.LogWarning("User not authenticated, use TwitchApi.AuthenticateWindow()");
            return null;
        }
        HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, $"{Endpoints["get_user"]}");
        message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        message.Headers.Add("Client-Id", clientId);
        //"https://api.twitch.tv/".AppendPathSegment(Endpoints["get_user"]).WithOAuthBearerToken("")

        HttpResponseMessage response = await client.SendAsync(message);
        string identity = await response.Content.ReadAsStringAsync();

        TwitchRquestUser user = JsonConvert.DeserializeObject<TwitchRquestUser>(identity);

        return user.Data[0];
    }
}

public enum NetworkBackendMode
{
    HttpListener,
    TcpSocket
}

public class TwitchRquestUser
{
    [JsonProperty("data")]
    public TwitchUser[] Data { get; set; }
}
public class TwitchUser
{
    [JsonProperty("login")]
    public string Login { get; set; }
    [JsonProperty("display_name")]
    public string DisplayName { get; set; }
    [JsonProperty("type")]
    public string Type { get; set; }
    [JsonProperty("description")]
    public string Discription { get; set; }
    [JsonProperty("profile_image_url")]
    public string Profile_image_url { get; set; }
    [JsonProperty("offline_image_url")]
    public string Offline_image_url { get; set; }
    [JsonProperty("email")]
    public string email { get; set; }
    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }
}

class StringTypeConverter : JsonConverter
{
    public override bool CanRead => true;
    public override bool CanWrite => true;

    public override bool CanConvert(Type objectType)
    {
        return true;
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        string json = (string)reader.Value;
        var result = JsonConvert.DeserializeObject(json, objectType);
        return result;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var json = JsonConvert.SerializeObject(value);
        serializer.Serialize(writer, json);
    }
}