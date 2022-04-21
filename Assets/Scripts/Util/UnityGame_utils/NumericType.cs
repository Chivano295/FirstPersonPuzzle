using System;

namespace UnityUtils
{
    public class NumericType : IEquatable<NumericType>
    {
        private object value;
        private Type type;
        public NumericType(object obj)
        {
            if (!obj.IsNumbericType())
            {
                throw new NumericTypeExpectedException("The type of object in the NumericType constructor must be numeric.");
            }
            value = obj;
            type = obj.GetType();
        }
        public object GetValue()
        {
            return value;
        }
        public void SetValue(object newValue)
        {
            value = newValue;
        }
        public bool Equals(NumericType other)
        {
            return this == other;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is NumericType))
                return GetValue() == obj;
            return Equals(obj);
        }
        public override int GetHashCode()
        {
            return GetValue().GetHashCode();
        }
        public override string ToString()
        {
            return GetValue().ToString();
        }
        /// 
        /// Checks if the value of left is smaller than the value of right.
        /// 
        public static bool operator <(NumericType left, NumericType right)
        {
            object leftValue = left.GetValue();
            object rightValue = right.GetValue();
            switch (Type.GetTypeCode(left.type))
            {
                case TypeCode.Byte:
                    return (byte)leftValue < (byte)rightValue;
                case TypeCode.SByte:
                    return (sbyte)leftValue < (sbyte)rightValue;
                case TypeCode.UInt16:
                    return (ushort)leftValue < (ushort)rightValue;
                case TypeCode.UInt32:
                    return (uint)leftValue < (uint)rightValue;
                case TypeCode.UInt64:
                    return (ulong)leftValue < (ulong)rightValue;
                case TypeCode.Int16:
                    return (short)leftValue < (short)rightValue;
                case TypeCode.Int32:
                    return (int)leftValue < (int)rightValue;
                case TypeCode.Int64:
                    return (long)leftValue < (long)rightValue;
                case TypeCode.Decimal:
                    return (decimal)leftValue < (decimal)rightValue;
                case TypeCode.Double:
                    return (double)leftValue < (double)rightValue;
                case TypeCode.Single:
                    return (float)leftValue < (float)rightValue;
            }
            throw new NumericTypeExpectedException("Please compare valid numeric types.");
        }
        /// 
        /// Checks if the value of left is greater than the value of right.
        /// 
        public static bool operator >(NumericType left, NumericType right)
        {
            object leftValue = left.GetValue();
            object rightValue = right.GetValue();
            switch (Type.GetTypeCode(left.type))
            {
                case TypeCode.Byte:
                    return (byte)leftValue > (byte)rightValue;
                case TypeCode.SByte:
                    return (sbyte)leftValue > (sbyte)rightValue;
                case TypeCode.UInt16:
                    return (ushort)leftValue > (ushort)rightValue;
                case TypeCode.UInt32:
                    return (uint)leftValue > (uint)rightValue;
                case TypeCode.UInt64:
                    return (ulong)leftValue > (ulong)rightValue;
                case TypeCode.Int16:
                    return (short)leftValue > (short)rightValue;
                case TypeCode.Int32:
                    return (int)leftValue > (int)rightValue;
                case TypeCode.Int64:
                    return (long)leftValue > (long)rightValue;
                case TypeCode.Decimal:
                    return (decimal)leftValue > (decimal)rightValue;
                case TypeCode.Double:
                    return (double)leftValue > (double)rightValue;
                case TypeCode.Single:
                    return (float)leftValue > (float)rightValue;
            }
            throw new NumericTypeExpectedException("Please compare valid numeric types.");
        }
        /// 
        /// Checks if the value of left is the same as the value of right.
        /// 
        public static bool operator ==(NumericType left, NumericType right)
        {
            return !(left > right) && !(left < right);
        }
        /// 
        /// Checks if the value of left is not the same as the value of right.
        /// 
        public static bool operator !=(NumericType left, NumericType right)
        {
            return (left > right) || (left < right);
        }
        /// 
        /// Checks if left is either equal or smaller than right.
        /// 
        public static bool operator <=(NumericType left, NumericType right)
        {
            return left == right || left < right;
        }
        /// 
        /// Checks if left is either equal or greater than right.
        /// 
        public static bool operator >=(NumericType left, NumericType right)
        {
            return left == right || left > right;
        }
    }
}
