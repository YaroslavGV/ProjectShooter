using UnityEngine;
using UnityEditor;

namespace StatSystem
{
    [CustomPropertyDrawer(typeof(StatTypeNotEdititable))]
    public class StatTypeNotEdititableDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.type == typeof(StatValue).Name)
                StatValueDrawer.Draw(position, property, label, false);
            else
                EditorGUI.PropertyField(position, property, label);
        }
    }
}