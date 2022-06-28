using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Vec3 = UnityEngine.Vector3;
using Vec4 = UnityEngine.Vector4;
using static UnityEngine.Debug;

/// <summary>
/// Save extension methods
/// </summary>
public static class SaveExtensions
{
    /// <summary>
    /// Extracts the position, velocity and angular velocity from a rigidbody
    /// </summary>
    /// <param name="rb"></param>
    /// <returns></returns>
    public static RigidBodyData GetRigidBodyData(this UnityEngine.Rigidbody rb)
    {
        RigidBodyData rbd = new RigidBodyData(rb.position, rb.velocity, rb.angularVelocity);
        return rbd;
    }

    /// <summary>
    /// Writes a Vector3 to a stream via a binary writer
    /// </summary>
    /// <param name="binaryWriter"></param>
    /// <param name="vector"></param>
    public static void WriteVec3(this BinaryWriter binaryWriter, Vec3 vector)
    {
        binaryWriter.Write(vector.x);
        binaryWriter.Write(vector.y);
        binaryWriter.Write(vector.z);
    }

    public static void WriteVec4(this BinaryWriter binaryWriter, Vec4 vector)
    {
        binaryWriter.Write(vector.x);
        binaryWriter.Write(vector.y);
        binaryWriter.Write(vector.z);
        binaryWriter.Write(vector.w);
    }

    /// <summary>
    /// Reads a Vector3 from a stream via a binary reader
    /// </summary>
    /// <param name="binaryWriter"></param>
    /// <param name="vector"></param>
    public static Vec3 ReadVec3(this BinaryReader binaryReader)
    {
        float x = binaryReader.ReadSingle();
        float y = binaryReader.ReadSingle();
        float z = binaryReader.ReadSingle();
        return new Vec3(x, y, z);
    }

    public static Vec4 ReadVec4(this BinaryReader binaryReader)
    {
        float x = binaryReader.ReadSingle();
        float y = binaryReader.ReadSingle();
        float z = binaryReader.ReadSingle();
        float w = binaryReader.ReadSingle();
        return new Vec3(x, y, z);
    }

    /// <summary>
    /// We don't talk about this
    /// </summary>
    /// <typeparam name="TStruct"></typeparam>
    /// <param name="binaryWriter"></param>
    /// <param name="instance"></param>
    public static void WriteStruct<TStruct>(this BinaryWriter binaryWriter, TStruct instance) where TStruct : unmanaged
    {
        Type structType = instance.GetType();
        FieldInfo[] fields = structType.GetFields();

        binaryWriter.Write(structType.Name);
        float a = 0;
        if (float.IsInfinity(a))
        {
            
        }

        foreach (FieldInfo field in fields)
        {
            if (!field.IsStatic && field.IsPublic)
            {
                binaryWriter.Write(field.FieldType.FullName);
                binaryWriter.Write(field.Name);
                using StreamWriter writer = new StreamWriter(binaryWriter.BaseStream, Encoding.UTF8, 1024, true);

                object val = field.GetValue(instance);
                writer.Write(val);
                //UnityEngine.Debug.Log(Marshal.SizeOf(val));
            }
        }
    }
    
    public static TStruct ReadStruct<TStruct>(this BinaryReader binaryReader) where TStruct : unmanaged
    {
        TStruct read = new TStruct();

        Type structType = read.GetType();
        FieldInfo[] fields = structType.GetFields();

        if (binaryReader.ReadString() != structType.Name)
        {
            throw new Exception("Provided file does not match specified struct");
        }

        foreach (FieldInfo field in fields)
        {
            Type readType = Type.GetType(binaryReader.ReadString());
            if (!field.IsStatic && field.IsPublic)
            {
                string fieldName = binaryReader.ReadString();

                if (field.Name != fieldName)
                {
                    LogWarning("Field mismatch!");
                    continue;
                }
                using StreamReader reader = new StreamReader(binaryReader.BaseStream, Encoding.UTF8, false, 1024, true);

                object tmpIn = Activator.CreateInstance(readType);
                switch (readType.FullName)
                {
                    case "System.Single":
                        Single block = (Single)tmpIn;
                        block = binaryReader.ReadSingle();
                        break;
                    default:
                        break;
                }

                //binaryWriter.Write(field.DeclaringType.Name);
                //field.SetValue(read, binaryReader.ReadBytes(Type.GetTypeFromProgID(typeName).;
                //binaryWriter.Write((int)field.GetValue(instance));
            }
        }

        return read;
    }
}