using System.Threading;
using UnityEngine;

namespace UnityUtils.math
{
    public class MathUtil
    {
        public static Vector3 GetRandomDir()
        {
            return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }
        public static Vector3 GetRandomDirXZ()
        {
            return new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));//.normalized;
        }
        public static Vector2 GetRandomDir2D()
        {
            return new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized;
        }
       
        public static int Wrap(int wrapNum, int MinInclusive, int MaxExclusive)
        {
            int wrapped = wrapNum;
            if (wrapNum > MaxExclusive)
            {
                
            }
            return wrapNum > MaxExclusive ? wrapNum - MaxExclusive : wrapNum < MinInclusive ?wrapNum + MinInclusive : wrapNum;
        }
        public static float Wrap(float wrapNum, float Min, float Max)
        {
            return wrapNum > Max ? Max : wrapNum < Min ? wrapNum + Min : wrapNum;
        }

        public static Vector3 Wrap3(Vector3 toWrap, Vector3 Min, Vector3 Max)
        {
            return new Vector3(Wrap(toWrap.x, Max.x, Max.x), Wrap(toWrap.y, Max.y, Max.y), Wrap(toWrap.z, Max.z, Max.z));
        }

        /// <summary>
        /// Clamps the given Vector3 between min and max
        /// </summary>
        /// <param name="value">The Vector3 value to restrict inside the range defined by the min and max values.</param>
        /// <param name="min">The minimum Vector3 value to compare against.</param>
        /// <param name="max">The maximum Vector3 value to compare against.</param>
        /// <returns></returns>
        public static Vector3 ClampV3(Vector3 value, Vector3 min, Vector3 max)
        {
            Vector3 clamped = new Vector3();
            clamped.x = Mathf.Clamp(value.x, min.x, max.x);
            clamped.x = Mathf.Clamp(value.y, min.y, max.y);
            clamped.x = Mathf.Clamp(value.z, min.z, max.z);
            return clamped;
        }

        /// <summary>
        /// Clamps the given Vector3 between min and max
        /// </summary>
        /// <param name="value">The Vector3 value to restrict inside the range defined by the min and max values.</param>
        /// <param name="min">The minimum Vector3 value to compare against.</param>
        /// <returns></returns>
        public static Vector3 ClampV3(Vector3 value, float max)
        {
            Vector3 clamped = new Vector3();
            clamped.x = Mathf.Clamp(value.x, 0, max);
            clamped.x = Mathf.Clamp(value.y, 0, max);
            clamped.x = Mathf.Clamp(value.z, 0, max);
            return clamped;
        }

        public static Vector3 ClampV301(Vector3 value)
        {
            Vector3 clamped = new Vector3();
            clamped.x = Mathf.Clamp01(value.x);
            clamped.x = Mathf.Clamp01(value.y);
            clamped.x = Mathf.Clamp01(value.z);
            return clamped;
        }

        public static Vector3 DivideFloatOutVec3(float left, Vector3 right)
        {
            float x = left / right.x;
            float y = left / right.y;
            float z = left / right.z;
            x = float.IsNaN(x) ? 0 : x; 
            y = float.IsNaN(y) ? 0 : y; 
            z = float.IsNaN(z) ? 0 : z;
            x = x == float.PositiveInfinity ? 0 : x; 
            y = y == float.PositiveInfinity ? 0 : y; 
            z = z == float.PositiveInfinity ? 0 : z;
            Vector3 vecOut = new Vector3(x, y, z);
            return vecOut;
        }
        
    }
}
