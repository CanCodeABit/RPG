// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;
using System;

namespace Boxophobic.StyledGUI
{
    public class StyledMessageDrawer : MaterialPropertyDrawer
    {
        public string Type;
        public string Message;
        public string Keyword;
        public float Value;
        public float Top;
        public float Down;

        MessageType _mType;

        public StyledMessageDrawer(string type, string message)
        {
            this.Type = type;
            this.Message = message;
            Keyword = null;

            this.Top = 0;
            this.Down = 0;
        }

        public StyledMessageDrawer(string type, string message, float top, float down)
        {
            this.Type = type;
            this.Message = message;
            Keyword = null;

            this.Top = top;
            this.Down = down;
        }

        public StyledMessageDrawer(string type, string message, string keyword, float value, float top, float down)
        {
            this.Type = type;
            this.Message = message;
            this.Keyword = keyword;
            this.Value = value;

            this.Top = top;
            this.Down = down;
        }

        public override void OnGUI(Rect position, MaterialProperty prop, String label, MaterialEditor materialEditor)
        {
            Material material = materialEditor.target as Material;

            if (Type == "None")
            {
                _mType = MessageType.None;
            }
            else if (Type == "Info")
            {
                _mType = MessageType.Info;
            }
            else if (Type == "Warning")
            {
                _mType = MessageType.Warning;
            }
            else if (Type == "Error")
            {
                _mType = MessageType.Error;
            }

            Message = Message.Replace("__", ",");

            if (Keyword != null)
            {
                if (material.HasProperty(Keyword))
                {
                    if (material.GetFloat(Keyword) == Value)
                    {
                        GUILayout.Space(Top);

                        EditorGUILayout.HelpBox(Message, _mType);

                        GUILayout.Space(Down);

                    }
                }
            }
            else
            {
                GUILayout.Space(Top);
                EditorGUILayout.HelpBox(Message, _mType);
                GUILayout.Space(Down);
            }
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            return -2;
        }
    }
}
