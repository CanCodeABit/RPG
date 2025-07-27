// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;
using System;

namespace Boxophobic.StyledGUI
{
    public class StyledTextDrawer : MaterialPropertyDrawer
    {
        public string Text = "";
        public string Alignment = "Center";
        public string Font = "Normal";
        public float Size = 11;
        public float Top = 0;
        public float Down = 0;

        public StyledTextDrawer(string text)
        {
            this.Text = text;
        }

        public StyledTextDrawer(string text, string alignment, string font, float size)
        {
            this.Text = text;
            this.Alignment = alignment;
            this.Font = font;
            this.Size = size;
        }

        public StyledTextDrawer(string text, string alignment, string font, float size, float top, float down)
        {
            this.Text = text;
            this.Alignment = alignment;
            this.Font = font;
            this.Size = size;
            this.Top = top;
            this.Down = down;
        }

        public override void OnGUI(Rect position, MaterialProperty prop, String label, MaterialEditor materialEditor)
        {
            //Material material = materialEditor.target as Material;

            GUIStyle styleLabel = new GUIStyle(EditorStyles.label)
            {
                richText = true,
                alignment = TextAnchor.MiddleCenter,
                wordWrap = true
            };

            GUILayout.Space(Top);

            if (Alignment == "Center")
            {
                styleLabel.alignment = TextAnchor.MiddleCenter;

            }
            else if (Alignment == "Left")
            {
                styleLabel.alignment = TextAnchor.MiddleLeft;
            }
            else if (Alignment == "Right")
            {
                styleLabel.alignment = TextAnchor.MiddleRight;
            }

            if (Font == "Normal")
            {
                styleLabel.fontStyle = FontStyle.Normal;
            }
            else if (Font == "Bold")
            {
                styleLabel.fontStyle = FontStyle.Bold;
            }
            else if (Font == "Italic")
            {
                styleLabel.fontStyle = FontStyle.Italic;
            }
            else if (Font == "BoldAndItalic")
            {
                styleLabel.fontStyle = FontStyle.BoldAndItalic;
            }

            styleLabel.fontSize = (int)Size;

            GUILayout.Label(Text, styleLabel);

            GUILayout.Space(Down);
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            return -2;
        }
    }
}
