using System.Collections;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CompressedSceneLoader
{
    /// <summary>
    /// Compresses a unity scene to save space, editor only
    /// </summary>
    /// 
    /// <param name="_path"></param>
    public static void CompressScene(string _path)
    {
        //Since this uses System.IO to load and compress scenes it cannot be used in a build
#if !UNITY_EDITOR
        Logger.LogWarning("Scene compressing in a built player is not supported");
#else
        string[] nameSplit = _path.Split('.');
        List<string> nameInsert = nameSplit.ToList();
        nameInsert.Insert(1, "-compressed");
        string fullName = $"{nameInsert[0]}{nameInsert[1]}.unis";
        
        Logger.LogVerbose(fullName);

        using (FileStream fsIn = File.OpenRead(_path))
        {
            using FileStream fsOut = File.Create(fullName);

            using (GZipStream gz = new GZipStream(fsOut, CompressionMode.Compress, false))
            {
                fsIn.CopyTo(gz);
            }
        }
        Logger.Log("Compression completed!");
#endif
    }

    /// <summary>
    /// Decompress a scene, editor only
    /// </summary>
    /// <param name="_path"></param>
    public static void DecompressScene(string _path)
    {
        Logger.LogVerbose(_path);

        string decompressedPath = string.Concat(_path.Remove(_path.LastIndexOf('.')), "-decompressed.unity");

        using (FileStream fsIn = File.OpenRead(_path))
        {
            using FileStream fsOut = File.Create(decompressedPath);
            using GZipStream gz = new GZipStream(fsIn, CompressionMode.Decompress);

            gz.CopyTo(fsOut);
        }
        Logger.Log("Decompression completed");
    }

    public static void DecompressAndLoad(string _path)
    {
        Logger.LogVerbose(_path);

        string decompressedPath = string.Concat(
            _path.Remove(_path.LastIndexOf('.')).Split(Path.AltDirectorySeparatorChar).Last(),
            "-decompressed.unity");
        
        using (FileStream fsIn = File.OpenRead(_path))
        {
            using FileStream fsOut = File.Create(decompressedPath);
            using GZipStream gz = new GZipStream(fsIn, CompressionMode.Decompress);

            gz.CopyTo(fsOut);
        }
        Logger.Log("Decompression completed");
    }
}
