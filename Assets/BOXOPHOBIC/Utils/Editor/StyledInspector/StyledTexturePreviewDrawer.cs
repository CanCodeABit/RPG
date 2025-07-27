// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

namespace Boxophobic.StyledGUI
{
    [CustomPropertyDrawer(typeof(StyledTexturePreview))]
    public class StyledTexturePreviewAttributeDrawer : PropertyDrawer
    {
        int _channel = 0;
        ColorWriteMask _channelMask = ColorWriteMask.All;

        StyledTexturePreview _a;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _a = (StyledTexturePreview)attribute;

            var tex = (Texture)property.objectReferenceValue;

            if (_a.DisplayName != "")
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(-1);
                GUILayout.Label(_a.DisplayName, GUILayout.Width(EditorGUIUtility.labelWidth - 1));
                tex = (Texture)EditorGUILayout.ObjectField(tex, typeof(Texture), false);
                GUILayout.EndHorizontal();

                GUILayout.Space(10);

                property.objectReferenceValue = tex;
            }

            if (tex == null)
            {
                return;
            }

            var styledText = new GUIStyle(EditorStyles.toolbarButton)
            {
                alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Normal,
                fontSize = 10,
            };

            var styledPopup = new GUIStyle(EditorStyles.toolbarPopup)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 10,
            };

            var rect = GUILayoutUtility.GetRect(0, 0, Screen.width, 0);

            EditorGUI.DrawPreviewTexture(rect, tex, null, ScaleMode.ScaleAndCrop, 1, 0, _channelMask);

            GUILayout.Space(2);

            GUILayout.BeginHorizontal();

            GUILayout.Label((UnityEngine.Profiling.Profiler.GetRuntimeMemorySizeLong(tex) / 1024f / 1024f).ToString("F2") + " mb", styledText);
            GUILayout.Space(-1);
            GUILayout.Label(tex.width.ToString(), styledText);
            GUILayout.Space(-1);
            GUILayout.Label(tex.graphicsFormat.ToString(), styledText);
            GUILayout.Space(-1);

            _channel = EditorGUILayout.Popup(_channel, new string[] { "RGB", "R", "G", "B", "A" }, styledPopup, GUILayout.MaxWidth(60)); 

            GUILayout.EndHorizontal();

            if (_channel == 0)
            {
                _channelMask = ColorWriteMask.All;
            }
            else if (_channel == 1)
            {
                _channelMask = ColorWriteMask.Red;
            }
            else if (_channel == 2)
            {
                _channelMask = ColorWriteMask.Green;
            }
            else if (_channel == 3)
            {
                _channelMask = ColorWriteMask.Blue;
            }
            else if (_channel == 4)
            {
                _channelMask = ColorWriteMask.Alpha;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return -2;
        }
    }
}
