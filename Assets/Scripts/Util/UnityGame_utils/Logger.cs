using UnityEngine;
using UnityEditor;

/// <summary>
/// A logger with toggle
/// </summary>
public class Logger
{
    public enum DebugLevel
    {
        Verbose = 0,
        Info,
        Warning,
        Error,
        Exception,
        Fatal
    }
    public static bool Debugging = true;
    public static DebugLevel Level = DebugLevel.Info;

    /// <summary>
    /// Verbose log
    /// </summary>
    /// <param name="message"></param>
    public static void LogVerbose(object message)
    {
        if (Debugging && Level == DebugLevel.Verbose)
            Debug.Log(message);
    }
    public static void Log(object message)
    {
        if (Debugging && Level <= DebugLevel.Info)
            Debug.Log(message);
    }
    public static void Log(string message)
    {
        if (Debugging && Level <= DebugLevel.Info)
            Debug.Log(message);
    }
    public static void LogWarning(object message)
    {
        if (Debugging && Level <= DebugLevel.Warning)
            Debug.LogWarning(message);
    }
    public static void LogSevere(object message)
    {
        if (Debugging && Level <= DebugLevel.Error)
            Debug.LogError(((string) message).ToUpper());
    }
    public static void LogError(object message)
    {
        if (Debugging && Level <= DebugLevel.Error)
            Debug.LogError(message.ToString());
    }

    //[MenuItem("Tools/Logger enabled (Non-working)")]
    public static void ToggleDebugging()
    {
        Debugging = !Debugging;
        Debug.Log(Debugging ? "Debugging enabled" : "Debugging disabled");
    }
}
