using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Vec3 = UnityEngine.Vector3;

public class SaveData
{
    public int SaveVersion;
    public Vec3 PlayerPosition;
        
    //Add the scales
    

    //Every rigidbody
    public RigidBodyData[] RigidBodyDatas;

    public SaveData() { }
    public SaveData(int ver)
    {
        SaveVersion = ver;
    }

    public static SaveData OutdatedSave
    {
        get
        {
            SaveData save = new SaveData();
            save.SaveVersion = -1;
            return save;
        }
    }
}
public struct RigidBodyData
{
    public Vec3 Position;
    public Vec3 Velocity;
    public Vec3 AngularVelocity;

    public RigidBodyData(Vec3 pos, Vec3 vel, Vec3 avl)
    {
        this.Position = pos;
        this.Velocity = vel;
        this.AngularVelocity = avl;
    }

    //Placed the values of the data into a rigidbody
    public void SetRigidbody(UnityEngine.Rigidbody rb)
    {
        rb.position = this.Position;
        rb.velocity = this.Velocity;
        rb.angularVelocity = this.AngularVelocity;
    }

    public void WriteToBinary(System.IO.BinaryWriter binaryWriter)
    {
        binaryWriter.WriteVec3(Position);
        binaryWriter.WriteVec3(Velocity);
        binaryWriter.WriteVec3(AngularVelocity);
    }
    public static RigidBodyData ReadFromBinary(System.IO.BinaryReader binaryReader)
    {
        RigidBodyData bodyData = new RigidBodyData();
        bodyData.Position = binaryReader.ReadVec3();
        bodyData.Velocity = binaryReader.ReadVec3();
        bodyData.AngularVelocity = binaryReader.ReadVec3();
        return bodyData;
    }

    public static RigidBodyData Empty => new RigidBodyData(Vec3.zero, Vec3.zero, Vec3.zero);

    public static bool operator ==(RigidBodyData left, RigidBodyData right)
    {
        return (left.Position == right.Position) && (left.Velocity == right.Velocity) && (left.AngularVelocity == right.AngularVelocity);
    }
    public static bool operator !=(RigidBodyData left, RigidBodyData right)
    {
        return !(left == right);
    }
}