// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;

namespace Boxophobic.StyledGUI
{
    [CustomPropertyDrawer(typeof(StyledLayers))]
    public class StyledLayersAttributeDrawer : PropertyDrawer
    {
        StyledLayers _a;
        private int _index;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _a = (StyledLayers)attribute;

            _index = property.intValue;

            string[] allLayers = new string[32];

            for (int i = 0; i < 32; i++)
            {
                if (LayerMask.LayerToName(i).Length < 1)
                {
                    allLayers[i] = "Missing";
                }
                else 
                {
                    allLayers[i] = LayerMask.LayerToName(i);
                }
            }

            if (_a.Display == "")
            {
                _a.Display = property.displayName;
            }

            _index = EditorGUILayout.Popup(_a.Display, _index, allLayers);

            property.intValue = _index;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return -2;
        }
    }
}
