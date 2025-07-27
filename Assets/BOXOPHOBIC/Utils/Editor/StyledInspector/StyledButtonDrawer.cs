// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;

namespace Boxophobic.StyledGUI
{
    [CustomPropertyDrawer(typeof(StyledButton))]
    public class StyledButtonAttributeDrawer : PropertyDrawer
    {
        StyledButton _a;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _a = (StyledButton)attribute;

            GUILayout.Space(_a.Top);

            if (GUILayout.Button(_a.Text))
            {
                property.boolValue = true;
            }

            GUILayout.Space(_a.Down);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return -2;
        }
    }
}

