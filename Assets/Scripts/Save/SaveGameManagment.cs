using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public class SaveGameManagment
{
    public const int CurrentSaveVersion = 1;
    public readonly string SavePathBinary = Path.Combine(Application.dataPath, "/save.bin");
    public readonly string SavePathJson = Path.Combine(Application.dataPath, "/save.json");
    public readonly string SavePathEncrypted = Path.Combine(Application.dataPath, "/save.dat");
    public FileSaveMode Fsm = FileSaveMode.FileSystemBinary;

    public SaveData Load()
    {
        SaveData sv = null;
        switch (Fsm)
        {
            case FileSaveMode.FileSystemBinary:
                sv = LoadFsBinary();
                break;
            case FileSaveMode.FileSystemJsonUtf8:
                break;
            case FileSaveMode.FileSystemEncrypted:
                break;
            default:
                break;
        }
        return sv;
    }

    public void Save(SaveData sd)
    {
        switch (Fsm)
        {
            case FileSaveMode.FileSystemBinary:
                SaveFsBinary(sd);
                break;
            case FileSaveMode.FileSystemJsonUtf8:
                break;
            case FileSaveMode.FileSystemEncrypted:
                break;
            default:
                break;
        }
    }

    private void SaveFsBinary(SaveData saveData)
    {
        using FileStream fs = File.OpenWrite(SavePathBinary);
        using BinaryWriter bw = new BinaryWriter(fs);

        bw.Write(saveData.SaveVersion);
        bw.WriteVec3(saveData.PlayerPosition);
        bw.Write(saveData.RigidBodyDatas.Length);
        for (int i = 0; i < saveData.RigidBodyDatas.Length; i++)
        {
            saveData.RigidBodyDatas[i].WriteToBinary(bw);
        }
    }
    private SaveData LoadFsBinary()
    {
        if (!File.Exists(SavePathBinary))
            return null;

        using FileStream fs = File.OpenWrite(SavePathBinary);
        using BinaryReader br = new BinaryReader(fs);

        SaveData saveData = new SaveData();

        saveData.SaveVersion = br.ReadInt32();
        saveData.PlayerPosition = br.ReadVec3();
        int rigids = br.ReadInt32();
        saveData.RigidBodyDatas = new RigidBodyData[rigids];
        for (int i = 0; i < rigids; i++)
        {
            saveData.RigidBodyDatas[i] = RigidBodyData.ReadFromBinary(br);
        }
        return saveData;
    }
}

public enum FileSaveMode
{
    FileSystemBinary,
    /// <summary>
    /// Too easy to read
    /// </summary>
    FileSystemJsonUtf8,
    /// <summary>
    /// Jk Cryptography is hard
    /// </summary>
    FileSystemEncrypted
}