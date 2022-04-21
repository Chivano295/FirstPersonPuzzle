using UnityEngine;
using System;
using System.IO;
using System.Reflection;

namespace UnityUtils.IO
{
    /// <summary>
    /// All functions require a Binary Reader/Writer
    /// </summary>
    public class FileUtils
    {
        public static void WriteVector3(BinaryWriter bw, Vector3 vector)
        {
            bw.Write(vector.x);
            bw.Write(vector.y);
            bw.Write(vector.z);
        }
        public static Vector3 ReadVector3(BinaryReader br)
        {
            Vector3 vect = new Vector3();
            vect.x = br.ReadSingle();
            vect.y = br.ReadSingle();
            vect.z = br.ReadSingle();
            return vect;
        }
        public static void Write<T>(BinaryWriter bw, T objWrite)
        {
            Type type = objWrite.GetType();
            FieldInfo[] fieldInfos = type.GetFields();
            foreach (var field in fieldInfos)
            {
                bw.Write(field.GetValue(objWrite).ToString());
            }
            return;
        }
    }
}
