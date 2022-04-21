using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

namespace UnityUtils
{
    [CustomPropertyDrawer(typeof(DrawIfAttribute))]
    public class DrawIfDrawer : PropertyDrawer
    {
        // Reference to the attribute on the property.
        private DrawIfAttribute drawIf;

        // Field that is being compared.
        private SerializedProperty comparedField;

        // Height of the property.  
        private float propertyHeight;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return propertyHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //Magic
            drawIf = attribute as DrawIfAttribute;
            if (drawIf == null) return;

            if (!drawIf.isProperty)
            {
                comparedField = property.serializedObject.FindProperty(drawIf.comparedPropertyName);
                if (comparedField == null) return;
            }

            Object target = property.serializedObject.targetObject;
            Type targetObjectClassType = target.GetType();
            var field = targetObjectClassType.GetField(drawIf.comparedPropertyName);
            object value;
            if (field != null)
            {
                value = field.GetValue(target);
            }
            else if (targetObjectClassType.GetProperty(drawIf.comparedPropertyName) != null)
            {
                var prop = targetObjectClassType.GetProperty(drawIf.comparedPropertyName);
                value = prop.GetValue(target);
            }
            else
            {
                Debug.LogWarning("You may specified a incorrect field, try using nameof(yourFieldHere)");
                EditorGUI.PropertyField(position, property, label);
                return;
            }
            bool conditionMet = false;

            //Convert from bool when needed
            bool wasBoolean = false;
            if (value.GetType() == typeof(bool))
            {
                wasBoolean = true;
                value = (bool)value ? 1 : 0;
            }

            if (value.IsNumbericType())
            {
                NumericType nt = new NumericType(value);
                NumericType cmp = new NumericType(!wasBoolean ? drawIf.comparedValue : (bool)(drawIf.comparedValue) ? 1 : 0);
                switch (drawIf.comparisonType)
                {
                    case ComparisonType.Equals:
                        if (nt == cmp)
                            conditionMet = true;
                        break;
                    case ComparisonType.NotEqual:
                        if (nt != cmp)
                            conditionMet = true;
                        break;
                    case ComparisonType.GreaterThan:
                        if (wasBoolean)
                        {
                            EditorGUI.PropertyField(position, property, label);
                            Debug.LogWarning("GreaterThan, SmallerThan, SmallerOrEqual and GreaterOrEqual are incompatible with booleans");
                            return;
                        }
                        if (nt > cmp)
                            conditionMet = true;
                        break;
                    case ComparisonType.SmallerThan:
                        if (wasBoolean)
                        {
                            EditorGUI.PropertyField(position, property, label);
                            Debug.LogWarning("GreaterThan, SmallerThan, SmallerOrEqual and GreaterOrEqual are incompatible with booleans");
                            return;
                        }
                        if (nt < cmp)
                            conditionMet = true;
                        break;
                    case ComparisonType.SmallerOrEqual:
                        if (wasBoolean)
                        {
                            EditorGUI.PropertyField(position, property, label);
                            Debug.LogWarning("GreaterThan, SmallerThan, SmallerOrEqual and GreaterOrEqual are incompatible with booleans");
                            return;
                        }
                        if (nt <= cmp)
                            conditionMet = true;
                        break;
                    case ComparisonType.GreaterOrEqual:
                        if (wasBoolean)
                        {
                            EditorGUI.PropertyField(position, property, label);
                            Debug.LogWarning("GreaterThan, SmallerThan, SmallerOrEqual and GreaterOrEqual are incompatible with booleans");
                            return;
                        }
                        if (nt >= cmp)
                            conditionMet = true;
                        break;
                }
            }
            propertyHeight = base.GetPropertyHeight(property, label);

            if (conditionMet)
                EditorGUI.PropertyField(position, property, label);
            else
            {
                switch (drawIf.disablingType)
                {
                    case DisablingType.ReadOnly:
                        GUI.enabled = false;
                        EditorGUI.PropertyField(position, property, label);
                        GUI.enabled = true;
                        break;
                    case DisablingType.DontDraw:
                        propertyHeight = 0;
                        
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
