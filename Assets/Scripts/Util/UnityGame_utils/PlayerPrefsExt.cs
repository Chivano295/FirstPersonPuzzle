using System;
using UnityEngine;

namespace UnityUtils
{
    /// <summary>
    /// Extended Playerprefs class with long support. Every function from <c>PlayerPrefs</c> is accessible in <c>PlayerPrefsExt</c>.
    /// </summary>
    public class PlayerPrefsExt : PlayerPrefs
    {
        public static void SetLong(string key, Int32 fragA, Int32 fragB)
        {
            PlayerPrefs.SetInt(key + "a", fragA);
            PlayerPrefs.SetInt(key + "b", fragB);
        }
        public static void SetLong(string key, Int64 defrag)
        {
            Int32[] fragged = IntegerUtil.Long2doubleInt(defrag);
            Int32 fragmentA = fragged[0];
            Int32 fragmentB = fragged[1];
            PlayerPrefs.SetInt(key + "a", fragmentA);
            PlayerPrefs.SetInt(key + "b", fragmentB);
        }
        public static Int64 GetLong(string key, Int64 defaultValue)
        {
            Int32[] defFrag = IntegerUtil.Long2doubleInt(defaultValue);
            Int32 fragA = PlayerPrefs.GetInt(key, defFrag[0]);
            Int32 fragB = PlayerPrefs.GetInt(key, defFrag[1]);
            return IntegerUtil.DoubleInt2Long(fragA, fragB);
        }
        public static void DeleteLong(string key)
        {
            PlayerPrefs.DeleteKey(key + "a");
            PlayerPrefs.DeleteKey(key + "b");
        }
    }
}
