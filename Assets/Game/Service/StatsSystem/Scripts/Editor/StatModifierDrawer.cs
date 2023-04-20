using UnityEditor;
using UnityEngine;

namespace StatSystem
{
    [CustomPropertyDrawer(typeof(StatModifier))]
    public class StatModifierDrawer : PropertyDrawer
    {
        private const string Type = "type";
        private const string Value = "value";
        private const string Species = "species";
        private const float TypeWidth = 80;
        private const float SpeciesWidth = 60;
        private const float Space = 2;

        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            Draw(position, property, label, true, true);
        }

        public static void Draw (Rect position, SerializedProperty property, GUIContent label, bool typeEditable, bool speciaEditable)
        {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var typeRect = new Rect(position.x, position.y, TypeWidth, position.height);
            var valuesRect = new Rect(position.x+TypeWidth+Space, position.y, position.width-TypeWidth-SpeciesWidth-Space*2f, position.height);
            var speciesRect = new Rect(position.x+position.width-SpeciesWidth, position.y, SpeciesWidth, position.height);

            property.DrawRelativeEnum(Type, typeRect, typeEditable);
            EditorGUI.PropertyField(valuesRect, property.FindPropertyRelative(Value), GUIContent.none);
            property.DrawRelativeEnum(Species, speciesRect, speciaEditable);
            
            EditorGUI.EndProperty();
        }
    }
}
