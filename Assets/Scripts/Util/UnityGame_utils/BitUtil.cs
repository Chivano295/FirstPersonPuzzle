using System;

namespace UnityUtils
{

    /// <summary>
    /// Integer conversion class
    /// </summary>
    public static class IntegerUtil
    {
        /// <summary>
        /// Convert 2 Int32 to 1 Int64 (long)
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <returns>Int64</returns>
        public static Int64 DoubleInt2Long(Int32 a1, Int32 a2)
        {
            Int64 b = a2;
            b = b << 32;
            b = b | (UInt32)a1;
            return b;
        }

        /// <summary>
        /// Converts an Int64 into an array of Int32 with 2 elements
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Int32[] Long2doubleInt(Int64 a)
        {
            Int32 a1 = (Int32)(a & UInt32.MaxValue);
            Int32 a2 = (Int32)(a >> 32);
            return new Int32[] { a1, a2 };
        }

        public static Int32 DoubleShort2Int(Int16 a1, Int16 a2)
        {
            Int32 b = a2;
            b = b << 16;
            b = b | (UInt16)a1;
            return b;
        }

        public static Int16[] Int2doublrShort(Int32 a)
        {
            Int16 a1 = (Int16)(a & UInt16.MaxValue);
            Int16 a2 = (Int16)(a >> 16);
            return new Int16[] { a1, a2 };
        }
    }
}
