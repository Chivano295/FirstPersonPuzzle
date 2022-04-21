using System.Collections.Generic;
using UnityEngine;

namespace UnityUtils
{
    public class ObjectPool : MonoBehaviour
    {
        /// <summary>
        /// Object used to fill the pool initially with
        /// </summary>
        public GameObject PooledObject;

        /// <summary>
        /// Inintial start size of the pool
        /// </summary>
        public int InitialSize = 11;
        /// <summary>
        /// Maximum size the pool can grow to
        /// </summary>
        public int MaxGrow = 20;

        /// <summary>
        /// Dynamically create objects when needed
        /// </summary>
        public bool AllowResize = true;

        private int m_Size;
        private Stack<PoolItem> poolItems = new Stack<PoolItem>();

        private void Awake()
        {
            m_Size = InitialSize;
            Expand(InitialSize);
        }

        private void Expand(int GrowTo)
        {
            for (int i = 0; i < GrowTo; i++)
            {
                GameObject go = Instantiate(PooledObject);
                PoolItem pi = go.GetComponent<PoolItem>();
                pi.ObjectPool = this;
                pi.transform.parent = transform;
                go.SetActive(false);
                poolItems.Push(pi);
            }
        }

        /// <summary>
        /// Pops an object of the stack and return it
        /// </summary>
        /// <param name="pos">Position</param>
        /// <param name="rot">Rotation</param>
        /// <param name="par">Parent</param>
        /// <returns>An object from the pool</returns>
        public GameObject PopObject(Vector3 pos, Quaternion rot, Transform par = null)
        {
            if (poolItems.Count == 0 && !AllowResize)
            {
                Debug.LogWarning("No objects to pop, consider enabling AllowResize or increasing Init size");
                return null;
            }
            else if (poolItems.Count == 0 && AllowResize)
            {
                //Resize logic
                if (m_Size >= MaxGrow)
                {
                    Debug.LogWarning("Reached max pool size, increase 'MaxGrow' to increase the pool size");
                    return null;
                }
                GameObject go = Instantiate(PooledObject);
                PoolItem piRe = go.GetComponent<PoolItem>();
                piRe.ObjectPool = this;
                poolItems.Push(piRe);
                m_Size++;
            }
            PoolItem pi = null;
            try
            {
                //System.Collections.Stack safe
                pi = (PoolItem)poolItems.Pop();
            }
            catch (System.Exception e)
            {
                Debug.LogError("What the fuck did you do " + e.Message);
                throw new System.Exception();
            }

            pi.Create(pos, rot, par != null ? par : transform);
            pi.gameObject.SetActive(true);
            return pi.gameObject;
        }

        /// <summary>
        /// Returns <paramref name="pi"/> back to the pool
        /// </summary>
        /// <param name="pi"></param>
        public void ReturnPooledObject(PoolItem pi)
        {
            if (!pi.gameObject.activeSelf) return;

            pi.transform.parent = transform;
            pi.gameObject.SetActive(false);
            poolItems.Push(pi);
        }
    }
}