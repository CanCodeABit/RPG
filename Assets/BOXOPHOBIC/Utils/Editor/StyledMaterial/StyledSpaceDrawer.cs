// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;

namespace Boxophobic.StyledGUI
{
    public class StyledSpaceDrawer : MaterialPropertyDrawer
    {
        public float Space;
        public string Conditions = "";

        public StyledSpaceDrawer(float space)
        {
            this.Space = space;
        }

        public StyledSpaceDrawer(float space, string conditions)
        {
            this.Space = space;
            this.Conditions = conditions;
        }

        public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor materialEditor)
        {
            if (Conditions == "")
            {
                GUILayout.Space(Space);
            }
            else
            {
                Material material = materialEditor.target as Material;

                bool showInspector = false;

                string[] split = Conditions.Split(char.Parse(" "));

                for (int i = 0; i < split.Length; i++)
                {
                    if (material.HasProperty(split[i]))
                    {
                        showInspector = true;
                        break;
                    }
                }

                if (showInspector)
                {
                    GUILayout.Space(Space);
                }
            }

        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            return -2;
        }
    }
}
