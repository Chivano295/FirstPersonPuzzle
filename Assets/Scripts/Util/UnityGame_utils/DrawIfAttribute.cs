using System;
using UnityEngine;

namespace UnityUtils
{
    /// <summary>
    /// Types of comparisons.
    /// </summary>
    public enum ComparisonType
    {
        Equals = 1,
        NotEqual = 2,
        GreaterThan = 3,
        SmallerThan = 4,
        SmallerOrEqual = 5,
        GreaterOrEqual = 6
    }

    /// <summary>
    /// Types of comparisons.
    /// </summary>
    public enum DisablingType
    {
        ReadOnly = 2,
        DontDraw = 3
    }

    [AttributeUsage(AttributeTargets.Field/*, AllowMultiple = true*/)]
    public partial class DrawIfAttribute : PropertyAttribute
    {
        public string comparedPropertyName { get; private set; }
        public object comparedValue { get; private set; }
        public ComparisonType comparisonType { get; private set; }
        public DisablingType disablingType { get; private set; }
        public bool isProperty { get; private set; }
    }

    public partial class DrawIfAttribute : PropertyAttribute
    {
        /// <summary>
        /// Make an field not show in editor or be readonly if the compared field/property meets the set condition
        /// Supports numeric types and bools
        /// </summary>
        /// <param name="comparedPropertyName">Name of property to compare it's value</param>
        /// <param name="comparedValue">The value to compare the <see cref="comparedPropertyName"/> against</param>
        /// <param name="comparisonType">How to compare the value</param>
        /// <param name="disablingType">show the field as read only or don't draw it</param>
        public DrawIfAttribute(string comparedPropertyName, object comparedValue, ComparisonType comparisonType, DisablingType disablingType = DisablingType.DontDraw)            
        {

            this.comparedPropertyName = comparedPropertyName;
            this.comparedValue = comparedValue;
            this.comparisonType = comparisonType;
            this.disablingType = disablingType;
            this.isProperty = false;
        }
        /// <inheritdoc cref="DrawIfAttribute.DrawIfAttribute(string, object, ComparisonType, DisablingType)"/>
        /// <param name="isProperty">specifies if <see cref="comparedPropertyName"/> is a property</param>
        public DrawIfAttribute(string comparedPropertyName, object comparedValue, ComparisonType comparisonType, DisablingType disablingType = DisablingType.DontDraw, bool isProperty = false)
        {

            this.comparedPropertyName = comparedPropertyName;
            this.comparedValue = comparedValue;
            this.comparisonType = comparisonType;
            this.disablingType = disablingType;
            this.isProperty = isProperty;
        }
    }
}
