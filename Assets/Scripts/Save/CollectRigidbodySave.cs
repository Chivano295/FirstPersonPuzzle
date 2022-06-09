using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectRigidbodySave : MonoBehaviour
{
    public Transform Player;

    public SaveGameManagment SaveManagment = new SaveGameManagment();

    private Rigidbody[] evil = null;

    public void DiscoverableSave()
    {
        SaveData sd = new SaveData(SaveGameManagment.CurrentSaveVersion);
        evil = FindObjectsOfType<Rigidbody>();
        sd.RigidBodyDatas = new RigidBodyData[evil.Length];
        for (int i = 0; i < evil.Length; i++)
        {
            sd.RigidBodyDatas[i] = evil[i].GetRigidBodyData();
        }
        sd.PlayerPosition = Player.position;
        SaveManagment.Save(sd);
    }

    public void DiscoverableLoad()
    {
        SaveData data = SaveManagment.Load();
        if (data == SaveData.OutdatedSave) throw new System.Exception("Please resave to update the save file");
        for (int i = 0; i < evil.Length; i++)
        {
            if (data.RigidBodyDatas[i] == RigidBodyData.Empty)
            {
                Debug.LogError("Err: cannot empty");
            }
            if (evil[i] == null)
            {
                Debug.LogError("Err: cannot null");
            }
            data.RigidBodyDatas[i].SetRigidbody(evil[i]);
        }
        Player.GetComponent<CharacterController>().Move(data.PlayerPosition);
    }
}
