using UnityEngine;

namespace UnityUtils
{
    public class PoolItem : MonoBehaviour
    {
        // ObjectPool which this object is apart of
        [SerializeField]
        private ObjectPool m_ObjectPool;

        /// <summary>
        /// Set ObjectPool
        /// </summary>
        public ObjectPool ObjectPool { get { return m_ObjectPool; } set { m_ObjectPool = value; } }

        #region Overridables
        /// <summary>
        /// Called when object is taken form the pool
        /// </summary>
        protected virtual void Activate() { }
        /// <summary>
        /// Called when object is returned to the region
        /// </summary>
        protected virtual void Deactivate() { }
        #endregion
        #region Create
        /// <summary>
        /// Creates an instance of a PoolItem
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="parent"></param>
        public void Create(Vector3 position, Quaternion rotation, Transform parent)
        {
            transform.position = position;
            transform.rotation = rotation;
            transform.parent = parent;
            Activate();
        }
        #endregion

        /// <summary>
        /// Return the object back to it's ObjectPool
        /// </summary>
        public void ReturnToSender()
        {
            Deactivate();
            m_ObjectPool.ReturnPooledObject(this);
        }
    }
}