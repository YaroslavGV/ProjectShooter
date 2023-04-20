using UnityEngine;
using UnityEditor;

namespace StatSystem
{
    [CustomPropertyDrawer(typeof(StatValue))]
    public class StatValueDrawer : PropertyDrawer
    {
        private const string Type = "type";
        private const string Value = "value";
        private const float SpaceWidth = 2;
        private const float TypeWidth = 80;

        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            bool typeEditable = attribute is StatTypeNotEdititable == false;
            Draw(position, property, label, typeEditable);
        }

        public static void Draw (Rect position, SerializedProperty property, GUIContent label, bool typeEditable)
        {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            float valueWidth = position.width-TypeWidth-SpaceWidth;
            float typePosition = position.x;
            float valuePosition = typePosition+SpaceWidth+TypeWidth;
            var typeRect = new Rect(typePosition, position.y, TypeWidth, position.height);
            var valueRect = new Rect(valuePosition, position.y, valueWidth, position.height);

            property.DrawRelativeEnum(Type, typeRect, typeEditable);
            EditorGUI.PropertyField(valueRect, property.FindPropertyRelative(Value), GUIContent.none);

            EditorGUI.EndProperty();
        }
    }
}