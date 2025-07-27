// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;
using System;

namespace Boxophobic.StyledGUI
{
    public class StyledCategoryDrawer : MaterialPropertyDrawer
    {
        public bool IsEnabled = true;
        public bool ShowDot = false;

        public string Category;
        public string Colapsable;
        public string Conditions = "";
        public string DotColor = "";
        public float Top;
        public float Down;

        public StyledCategoryDrawer(string category)
        {
            this.Category = category;
            this.Colapsable = "false";
            this.Conditions = "";
            this.DotColor = "";
            this.Top = 10;
            this.Down = 10;
        }

        public StyledCategoryDrawer(string category, string colapsable)
        {
            this.Category = category;
            this.Colapsable = colapsable;
            this.Conditions = "";
            this.DotColor = "";
            this.Top = 10;
            this.Down = 10;
        }

        public StyledCategoryDrawer(string category, float top, float down)
        {
            this.Category = category;
            this.Colapsable = "false";
            this.Conditions = "";
            this.DotColor = "";
            this.Top = top;
            this.Down = down;
        }

        public StyledCategoryDrawer(string category, string colapsable, float top, float down)
        {
            this.Category = category;
            this.Colapsable = colapsable;
            this.Conditions = "";
            this.DotColor = "";
            this.Top = top;
            this.Down = down;
        }

        public StyledCategoryDrawer(string category, string colapsable, string conditions, string dotColor, float top, float down)
        {
            this.Category = category;
            this.Colapsable = colapsable;
            this.Conditions = conditions;
            this.DotColor = dotColor;
            this.Top = top;
            this.Down = down;
        }

        public override void OnGUI(Rect position, MaterialProperty prop, String label, MaterialEditor materialEditor)
        {
            GUI.enabled = true;
            //GUI.color = Color.white;
            //GUI.contentColor = Color.white;
            EditorGUI.indentLevel = 0;

            if (Conditions != "")
            {
                ShowDot = false;

                Material material = materialEditor.target as Material;

                string[] split = Conditions.Split(char.Parse(" "));

                for (int i = 0; i < split.Length; i++)
                {
                    var property = split[i];

                    if (material.HasProperty(property))
                    {
                        if (material.GetFloat(property) > 0)
                        {
                            ShowDot = true;
                            break;
                        }
                    }
                }

                DrawInspector(prop);
            }
            else
            {
                DrawInspector(prop);
            }
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            return -2;
        }

        void DrawInspector(MaterialProperty prop)
        {
            bool isColapsable = false;

            if (Colapsable == "true")
            {
                isColapsable = true;
            }

            //bool isEnabled = true;

            if (prop.floatValue < 0.5f)
            {
                IsEnabled = false;
            }
            else
            {
                IsEnabled = true;
            }

            if (ShowDot)
            {
                IsEnabled = StyledGUI.DrawInspectorCategory(Category, IsEnabled, isColapsable, DotColor, Top, Down);
            }
            else
            {
                IsEnabled = StyledGUI.DrawInspectorCategory(Category, IsEnabled, isColapsable, Top, Down);
            }

            if (IsEnabled)
            {
                prop.floatValue = 1;
            }
            else
            {
                prop.floatValue = 0;
            }
        }
    }
}
