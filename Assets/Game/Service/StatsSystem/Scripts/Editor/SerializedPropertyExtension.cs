using UnityEditor;
using UnityEngine;

namespace StatSystem
{
    public static class SerializedPropertyExtension
    {
        public static void DrawRelativeEnum (this SerializedProperty property, string relativePropertyPath, Rect position, bool editable)
        {
            if (editable)
            {
                EditorGUI.PropertyField(position, property.FindPropertyRelative(relativePropertyPath), GUIContent.none);
            }
            else
            {
                var field = property.FindPropertyRelative(relativePropertyPath);
                string enumName = field.enumNames[field.enumValueIndex];
                string name = ObjectNames.NicifyVariableName(enumName);
                GUI.Label(position, name);
            }
        }
    }
}
