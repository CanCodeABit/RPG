// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;

namespace Boxophobic.StyledGUI
{
    [CustomPropertyDrawer(typeof(StyledText))]
    public class StyledTextAttributeDrawer : PropertyDrawer
    {
        StyledText _a;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _a = (StyledText)attribute;

            GUIStyle styleLabel = new GUIStyle(EditorStyles.label)
            {
                richText = true,
                wordWrap = true
            };

            styleLabel.alignment = _a.Alignment;

            GUILayout.Space(_a.Top);

            GUILayout.Label(property.stringValue, styleLabel);

            GUILayout.Space(_a.Down);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return -2;
        }
    }
}

