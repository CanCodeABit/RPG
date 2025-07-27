// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;

namespace Boxophobic.StyledGUI
{
    [CustomPropertyDrawer(typeof(StyledSpace))]
    public class StyledSpaceAttributeDrawer : PropertyDrawer
    {
        StyledSpace _a;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _a = (StyledSpace)attribute;

            GUILayout.Space(_a.Space);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return -2;
        }
    }
}
