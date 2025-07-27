// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;

namespace Boxophobic.StyledGUI
{
    [CustomPropertyDrawer(typeof(StyledCategory))]
    public class StyledCategoryAttributeDrawer : PropertyDrawer
    {
        StyledCategory _a;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _a = (StyledCategory)attribute;

            property.boolValue = StyledGUI.DrawInspectorCategory(_a.Category, property.boolValue, _a.Colapsable, _a.Top, _a.Down);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return -2;
        }
    }
}
