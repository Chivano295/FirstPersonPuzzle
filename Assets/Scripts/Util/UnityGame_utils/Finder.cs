using System.Collections.Generic;
//using Unity.Collections
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityUtils
{
    public static class Finder
    {
        /// <summary>
        /// Find interfaces of type <c>T</c>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> FindInterfaces<T>()
        {
            //Unity.Collections.LowLevel.Unsafe.UnsafeUtility.
            List<T> interfaces = new List<T>();
            GameObject[] rootObjs = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (var rootObj in rootObjs)
            {
                T[] childInterfaces = rootObj.GetComponentsInChildren<T>();
                foreach (var item in childInterfaces)
                {
                    interfaces.Add(item);
                }
            }
            return interfaces;
        }
    }
}