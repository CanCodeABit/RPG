// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;

namespace Boxophobic.StyledGUI
{
    public class StyledTextureDrawer : MaterialPropertyDrawer
    {
        public float Size;
        public float Top;
        public float Down;

        public StyledTextureDrawer()
        {
            this.Size = 50;
            this.Top = 0;
            this.Down = 0;
        }

        public StyledTextureDrawer(float size)
        {
            this.Size = size;
            this.Top = 0;
            this.Down = 0;
        }

        public StyledTextureDrawer(float size, float top, float down)
        {
            this.Size = size;
            this.Top = top;
            this.Down = down;
        }

        public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor materialEditor)
        {
            GUILayout.Space(Top);

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = prop.hasMixedValue;

            Texture tex = null;

            if (prop.textureDimension == UnityEngine.Rendering.TextureDimension.Tex2D)
            {
                tex = (Texture2D)EditorGUILayout.ObjectField(prop.displayName, prop.textureValue, typeof(Texture2D), false, GUILayout.Height(50));
            }

            if (prop.textureDimension == UnityEngine.Rendering.TextureDimension.Cube)
            {
                tex = (Cubemap)EditorGUILayout.ObjectField(prop.displayName, prop.textureValue, typeof(Cubemap), false, GUILayout.Height(50));
            }

            EditorGUI.showMixedValue = false;

            if (EditorGUI.EndChangeCheck())
            {
                prop.textureValue = tex;
            }

            GUILayout.Space(Down);
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            return -2;
        }
    }
}
