// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;
using System;

namespace Boxophobic.StyledGUI
{
    public class StyledVectorDrawer : MaterialPropertyDrawer
    {
        public float Space = 0;
        public float Top = 0;
        public float Down = 0;

        public StyledVectorDrawer(float space)
        {
            this.Space = space;
        }

        public StyledVectorDrawer(float space, float top, float down)
        {
            this.Space = space;
            this.Top = top;
            this.Down = down;
        }

        public override void OnGUI(Rect position, MaterialProperty prop, String label, MaterialEditor materialEditor)
        {
            GUILayout.Space(Top);

            if (EditorGUIUtility.currentViewWidth > 344)
            {
                materialEditor.VectorProperty(prop, label);
                GUILayout.Space(-Space);
            }
            else
            {
                materialEditor.VectorProperty(prop, label);
                GUILayout.Space(2);
            }

            GUILayout.Space(Down);
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            return -2;
        }
    }
}
