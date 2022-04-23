using System.Collections;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CompressedSceneLoader
{
    public static void CompressScene(string _path)
    {
        string[] nameSplit = _path.Split('.');
        List<string> nameInsert = nameSplit.ToList();
        nameInsert.Insert(1, "-compressed");
        string fullName = $"{nameInsert[0]}{nameInsert[1]}.unis";
        
        Logger.Level = Logger.DebugLevel.Verbose;
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
    }
    public static void DecompressScene(string _path)
    {
        Logger.Level = Logger.DebugLevel.Verbose;
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
}
