// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;
using System;

namespace Boxophobic.StyledGUI
{
    public class StyledButtonDrawer : MaterialPropertyDrawer
    {
        public string Text;
        public string Target = "";
        public float Value = 1;
        public float Top;
        public float Down;

        public StyledButtonDrawer(string text)
        {
            this.Text = text;
            this.Value = 1;
            this.Top = 0;
            this.Down = 0;
        }

        public StyledButtonDrawer(string text, float value, float top, float down)
        {
            this.Text = text;
            this.Value = value;
            this.Top = top;
            this.Down = down;
        }

        public StyledButtonDrawer(string text, string target, float value, float top, float down)
        {
            this.Text = text;
            this.Target = target;
            this.Value = value;
            this.Top = top;
            this.Down = down;
        }

        public override void OnGUI(Rect position, MaterialProperty prop, String label, MaterialEditor materialEditor)
        {
            Material material = materialEditor.target as Material;

            GUILayout.Space(Top);

            if (GUILayout.Button(Text))
            {
                if (Target == "")
                {
                    prop.floatValue = Value;
                }
                else
                {
                    if (material.HasProperty(Target))
                    {
                        material.SetFloat(Target, Value);
                    }
                }
            }

            GUILayout.Space(Down);
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            return -2;
        }
    }
}
