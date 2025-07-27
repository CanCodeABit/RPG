// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;
using System;

namespace Boxophobic.StyledGUI
{
    public class StyledRemapSliderDrawer : MaterialPropertyDrawer
    {
        public string NameMin = "";
        public string NameMax = "";
        public float Min = 0;
        public float Max = 0;
        public float Top = 0;
        public float Down = 0;

        float _internalValueMin;
        float _internalValueMax;

        bool _showAdvancedSettings = false;

        public StyledRemapSliderDrawer(string nameMin, string nameMax, float min, float max)
        {
            this.NameMin = nameMin;
            this.NameMax = nameMax;
            this.Min = min;
            this.Max = max;
            this.Top = 0;
            this.Down = 0;
        }

        public StyledRemapSliderDrawer(string nameMin, string nameMax, float min, float max, float top, float down)
        {
            this.NameMin = nameMin;
            this.NameMax = nameMax;
            this.Min = min;
            this.Max = max;
            this.Top = top;
            this.Down = down;
        }

        public StyledRemapSliderDrawer()
        {
            this.NameMin = null;
            this.NameMax = null;
            this.Min = 0;
            this.Max = 1;
            this.Top = 0;
            this.Down = 0;
        }

        public StyledRemapSliderDrawer(float min, float max)
        {
            this.NameMin = null;
            this.NameMax = null;
            this.Min = min;
            this.Max = max;
            this.Top = 0;
            this.Down = 0;
        }

        public StyledRemapSliderDrawer(float min, float max, float top, float down)
        {
            this.NameMin = null;
            this.NameMax = null;
            this.Min = min;
            this.Max = max;
            this.Top = top;
            this.Down = down;
        }

        public override void OnGUI(Rect position, MaterialProperty prop, String label, MaterialEditor editor)
        {
            var internalPropMin = MaterialEditor.GetMaterialProperty(editor.targets, NameMin);
            var internalPropMax = MaterialEditor.GetMaterialProperty(editor.targets, NameMax);

            var stylePopupMini = new GUIStyle(EditorStyles.popup)
            {
                fontSize = 9,
            };

            var styleButton = new GUIStyle(EditorStyles.label)
            {

            };

            Vector4 propVector = prop.vectorValue;

            EditorGUI.BeginChangeCheck();

            if (propVector.w == 0)
            {
                _internalValueMin = propVector.x;
                _internalValueMax = propVector.y;
            }
            else
            {
                _internalValueMin = propVector.y;
                _internalValueMax = propVector.x;
            }

            GUILayout.Space(Top);

            EditorGUI.showMixedValue = prop.hasMixedValue;

            GUILayout.BeginHorizontal();

            if (GUILayout.Button(label, styleButton, GUILayout.Width(EditorGUIUtility.labelWidth), GUILayout.Height(18)))
            {
                _showAdvancedSettings = !_showAdvancedSettings;
            }

            EditorGUILayout.MinMaxSlider(ref _internalValueMin, ref _internalValueMax, Min, Max);

            GUILayout.Space(2);

            propVector.w = (float)EditorGUILayout.Popup((int)propVector.w, new string[] { "Remap", "Invert" }, stylePopupMini, GUILayout.Width(50));

            GUILayout.EndHorizontal();

            if (_showAdvancedSettings)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(-1);
                GUILayout.Label("      Remap Min", GUILayout.Width(EditorGUIUtility.labelWidth));
                _internalValueMin = Mathf.Clamp(EditorGUILayout.Slider(_internalValueMin, Min, Max), Min, _internalValueMax);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(-1);
                GUILayout.Label("      Remap Max", GUILayout.Width(EditorGUIUtility.labelWidth));
                _internalValueMax = Mathf.Clamp(EditorGUILayout.Slider(_internalValueMax, Min, Max), _internalValueMin, Max);
                GUILayout.EndHorizontal();
            }

            EditorGUI.showMixedValue = false;

            if (EditorGUI.EndChangeCheck())
            {
                if (propVector.w == 0)
                {
                    propVector.x = _internalValueMin;
                    propVector.y = _internalValueMax;
                }
                else
                {
                    propVector.y = _internalValueMin;
                    propVector.x = _internalValueMax;
                }

                propVector.z = 1 / (propVector.y - propVector.x);

                prop.vectorValue = propVector;

                if (internalPropMin.displayName != null && internalPropMax.displayName != null)
                {
                    internalPropMin.floatValue = _internalValueMin;
                    internalPropMax.floatValue = _internalValueMax;
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