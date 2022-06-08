using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectRigidbodySave : MonoBehaviour
{
    public Transform Player;

    public SaveGameManagment SaveManagment = new SaveGameManagment();

    public void DiscoverableSave()
    {
        Rigidbody[] evil = FindObjectsOfType<Rigidbody>();
        RigidBodyData[] rigidBodyDatas = new RigidBodyData[evil.Length];
        for (int i = 0; i < evil.Length; i++)
        {
            rigidBodyDatas[i] = new RigidBodyData(evil[i].position, evil[i].velocity, evil[i].angularVelocity);
        }
        SaveData sd = new SaveData();
        sd.RigidBodyDatas = rigidBodyDatas;
        sd.PlayerPosition = Player.position;
        SaveManagment.Save(sd);
    }
}
