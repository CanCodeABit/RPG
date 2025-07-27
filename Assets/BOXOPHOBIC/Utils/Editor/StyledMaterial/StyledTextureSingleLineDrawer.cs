// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;

namespace Boxophobic.StyledGUI
{
    public class StyledTextureSingleLineDrawer : MaterialPropertyDrawer
    {
        public float Top;
        public float Down;
        public string Tooltip;

        int _previewChannel = 0;
        Material _previewMaterial;

        bool _showAdvancedSettings = false;

        public StyledTextureSingleLineDrawer()
        {
            this.Top = 0;
            this.Down = 0;
            this.Tooltip = "";
        }

        public StyledTextureSingleLineDrawer(string tooltip)
        {
            this.Top = 0;
            this.Down = 0;
            this.Tooltip = tooltip;
        }

        public StyledTextureSingleLineDrawer(float top, float down)
        {
            this.Top = top;
            this.Down = down;
            this.Tooltip = "";
        }

        public StyledTextureSingleLineDrawer(string tooltip, float top, float down)
        {
            this.Top = top;
            this.Down = down;
            this.Tooltip = tooltip;
        }

        public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor materialEditor)
        {
            GUILayout.Space(Top);

            materialEditor.TexturePropertySingleLine(new GUIContent(prop.displayName, Tooltip), prop);

            if (prop.textureValue != null && prop.textureValue.dimension == UnityEngine.Rendering.TextureDimension.Tex2D)
            {
                var lastRect = GUILayoutUtility.GetLastRect();

                if (GUI.Button(lastRect, "", GUIStyle.none))
                {
                    _showAdvancedSettings = !_showAdvancedSettings;
                }

                if (_showAdvancedSettings)
                {
                    if (_previewMaterial == null)
                    {
                        _previewMaterial = new Material(Shader.Find("Hidden/BOXOPHOBIC/Helpers/Channel Preview"));
                    }

                    _previewChannel = StyledGUI.DrawTexturePreview(prop.textureValue, _previewMaterial, _previewChannel);
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
