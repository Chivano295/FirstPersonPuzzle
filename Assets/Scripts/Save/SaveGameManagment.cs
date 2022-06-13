using System;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public class SaveGameManagment
{
    public const int CurrentSaveVersion = 1;
    public string SavePathBinary => Path.Combine(Application.dataPath, "save.bin");
    public string SavePathJson => Path.Combine(Application.dataPath, "save.json");
    public string SavePathEncrypted => Path.Combine(Application.dataPath, "save.dat");
    public FileSaveMode Fsm = FileSaveMode.FileSystemBinary;

    /// <summary>
    /// Loads save data from disk in the specifield FileSaveMode <see cref="Fsm"/>
    /// </summary>
    /// <returns></returns>
    public SaveData Load(bool loadLegacy = false)
    {
        SaveData sv = null;
        switch (Fsm)
        {
            case FileSaveMode.FileSystemBinary:
                sv = LoadFsBinary(loadLegacy);
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
    /// <summary>
    /// Attempts to delete the save file
    /// </summary>
    /// <returns>true if it succeeded, false if does not exists</returns>
    public bool TryDelete()
    {
        switch (Fsm)
        {
            case FileSaveMode.FileSystemBinary:
                if (File.Exists(SavePathBinary))
                {
                    Delete();
                    return true;
                }
                break;
            case FileSaveMode.FileSystemJsonUtf8:
                break;
            case FileSaveMode.FileSystemEncrypted:
                break;
            default:
                break;
        }
        return false;
    }

    /// <summary>
    /// Deletes the save regardless if it exists or not
    /// </summary>
    public void Delete()
    {
        switch (Fsm)
        {
            case FileSaveMode.FileSystemBinary:
                DeleteFsBinary();
                break;
            case FileSaveMode.FileSystemJsonUtf8:
                break;
            case FileSaveMode.FileSystemEncrypted:
                break;
            default:
                break;
        }
    }

    public DateTime GetLastSaveDate()
    {
        DateTime saveDate = default;
        switch (Fsm)
        {
            case FileSaveMode.FileSystemBinary:
                saveDate = File.GetLastWriteTime(SavePathBinary);
                break;
            case FileSaveMode.FileSystemJsonUtf8:
                break;
            case FileSaveMode.FileSystemEncrypted:
                break;
            default:
                break;
        }
        return saveDate;
    }

    public bool TryLoadLegacyBinary(out SaveData saveData)
    {
        if (!File.Exists(SavePathBinary))
        {
            saveData = SaveData.OutdatedSave;
            return false;
        }
        using FileStream fs = File.OpenRead(SavePathBinary);
        using BinaryReader br = new BinaryReader(fs);
        saveData = new SaveData();

        saveData.SaveVersion = br.ReadInt32();
        if (saveData.SaveVersion != CurrentSaveVersion)
        {
            saveData = SaveData.OutdatedSave;
            return false;
        }

        saveData.PlayerPosition = br.ReadVec3();
        int rigids = br.ReadInt32();
        saveData.RigidBodyDatas = new RigidBodyData[rigids];
        for (int i = 0; i < rigids; i++)
        {
            saveData.RigidBodyDatas[i] = RigidBodyData.ReadFromBinary(br);
        }
        return true;
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
    private SaveData LoadFsBinary(bool loadLegacy = false)
    {
        if (!File.Exists(SavePathBinary))
            return null;
        using FileStream fs = File.OpenRead(SavePathBinary);
        using BinaryReader br = new BinaryReader(fs);

        SaveData saveData = new SaveData();

        saveData.SaveVersion = br.ReadInt32();
        if (saveData.SaveVersion != CurrentSaveVersion)
        {
            return SaveData.OutdatedSave;
        }

        saveData.PlayerPosition = br.ReadVec3();
        int rigids = br.ReadInt32();
        saveData.RigidBodyDatas = new RigidBodyData[rigids];
        for (int i = 0; i < rigids; i++)
        {
            saveData.RigidBodyDatas[i] = RigidBodyData.ReadFromBinary(br);
        }
        return saveData;
    }

    private void DeleteFsBinary()
    {
        File.Delete(SavePathBinary);
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