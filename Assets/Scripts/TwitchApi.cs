using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Flurl;
using Flurl.Http;

/// <summary>
/// Class for interacting with Twitch Api
/// </summary>
public class TwitchApi
{
    //Port to listen locally for redirect
    public const int TcpPort = 8888;//5576;
    //Twitter base endpoint for calls
    public Uri TwitchBase = new Uri("https://api.twitch.tv/");
    public string TwitchAuthorizationBase = "https://id.twitch.tv/";


    //Endpoint mapping
    public Dictionary<string, string> Endpoints { get; private set; }  = new Dictionary<string, string>()
    {
        { "auth", "oauth2/authorize" },
        { "get_user", "/helix/users" }
    };

    //Client for interacting with api
    private HttpClient client;
    //Listener for HttpListener mode
    private HttpListener listener;
    //Listener for TcpListener mode
    private TcpListener tcp;
    //The clientId passed in the constructor
    private string clientId;
    //Deternines wether 
    private bool authorized = false;
    private string accessCode;
    private string token;
    
    // Constructs a new instance to interact with the API with a specific application
    public TwitchApi(string _clientId)
    {
        //Initalize instances 
        client = new HttpClient();
        client.BaseAddress = TwitchBase;
        this.clientId = _clientId;
        listener = new HttpListener();
        //listener.Prefixes.Add($"http://127.0.0.1:{TcpPort}/");
        listener.Prefixes.Add($"http://localhost:{TcpPort}/");
        //listener.Prefixes.Add("https://127.0.0.1:5566/");

        tcp = new TcpListener(IPAddress.Loopback, TcpPort);
    }

    public async Task<bool> AuthenticateWindow(NetworkBackendMode _backendMode = NetworkBackendMode.HttpListener)
    {
        Process.Start(
            $"{TwitchAuthorizationBase}{Endpoints["auth"]}?client_id=wu1igcaqgvex2nqrrnqdr2d40c7rjh&response_type=code&force_verify=true&scope=user:read:email&redirect_uri=http://localhost:{TcpPort}"
            );

        string responseString =
            "<!DOCTYPE html>\n<html>\n<head>\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>\n</head>\n<body>\nPlease return to the app.\n</body>\n</html>";
        byte[] buffer = Encoding.UTF8.GetBytes(responseString);
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
            listener.Start();
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
                Logger.LogError("Failed");
                listener.Stop();
                return false;
            }

            accessCode = context.Request.QueryString.Get("code");

            HttpRequestMessage tokenMessage = new HttpRequestMessage();
            tokenMessage.Method = HttpMethod.Post;
            tokenMessage.RequestUri = new Uri($"{TwitchAuthorizationBase}oauth2/token" +
                $"?client_id=wu1igcaqgvex2nqrrnqdr2d40c7rjh&code={accessCode}&client_secret=v9w982xzajxbavisjhhfx70tljwc46&grant_type=authorization_code&redirect_uri=http://localhost:8888");

            HttpResponseMessage tokenResponse = await client.SendAsync(tokenMessage);

            if (tokenResponse.StatusCode != HttpStatusCode.OK)
            {
                
            }

            listener.Stop();
            
            Logger.Log(response.StatusCode);

        }
        authorized = true;
        return true;
    }

    public void ValidateToken()
    { }

    public async Task<string> UserReadEmail()
    {
        //Check if user is authenticated
        if (!authorized)
        {
            Logger.LogWarning("User not authenticated, use TwitchApi.AuthenticateWindow()");
        }
        HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, Endpoints["get_user"]);

        //"https://api.twitch.tv/".AppendPathSegment(Endpoints["get_user"]).WithOAuthBearerToken("")

        HttpResponseMessage response = await client.SendAsync(message);
        return await response.Content.ReadAsStringAsync();
    }
}

public enum NetworkBackendMode
{
    HttpListener,
    TcpSocket
}