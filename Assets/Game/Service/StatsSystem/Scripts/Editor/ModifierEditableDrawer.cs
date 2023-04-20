using UnityEditor;
using UnityEngine;

namespace StatSystem
{
    [CustomPropertyDrawer(typeof(ModifierEditable))]
    public class ModifierEditableDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            var meAttribute = attribute as ModifierEditable;
            if (property.type == typeof(StatModifier).Name)
                StatModifierDrawer.Draw(position, property, label, meAttribute.type, meAttribute.species);
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }
    }
}