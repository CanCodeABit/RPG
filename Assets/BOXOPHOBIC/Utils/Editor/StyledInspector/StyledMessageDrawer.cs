// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;

namespace Boxophobic.StyledGUI
{
    [CustomPropertyDrawer(typeof(StyledMessage))]
    public class StyledMessageAttributeDrawer : PropertyDrawer
    {
        StyledMessage _a;

        bool _show;
        MessageType _messageType;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _show = property.boolValue;

            if (_show)
            {
                _a = (StyledMessage)attribute;

                if (_a.Type == "None")
                {
                    _messageType = MessageType.None;
                }
                else if (_a.Type == "Info")
                {
                    _messageType = MessageType.Info;
                }
                else if (_a.Type == "Warning")
                {
                    _messageType = MessageType.Warning;
                }
                else if (_a.Type == "Error")
                {
                    _messageType = MessageType.Error;
                }

                GUILayout.Space(_a.Top);
                EditorGUILayout.HelpBox(_a.Message, _messageType);
                GUILayout.Space(_a.Down);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return -2;
        }
    }
}
