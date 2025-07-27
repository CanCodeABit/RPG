// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;
using System;

namespace Boxophobic.StyledGUI
{
    public class StyledOptionsSliderDrawer : MaterialPropertyDrawer
    {
        public string NameMin = "";
        public string NameMax = "";
        public string NameVal = "";
        public float Min = 0;
        public float Max = 0;
        public float Val = 0;
        public float Top = 0;
        public float Down = 0;

        bool _showAdvancedOptions = false;

        public StyledOptionsSliderDrawer(string nameMin, string nameMax, string nameVal, float min, float max, float val)
        {
            this.NameMin = nameMin;
            this.NameMax = nameMax;
            this.NameVal = nameVal;
            this.Min = min;
            this.Max = max;
            this.Val = val;
            this.Top = 0;
            this.Down = 0;
        }

        public StyledOptionsSliderDrawer(string nameMin, string nameMax, string nameVal, float min, float max, float val, float top, float down)
        {
            this.NameMin = nameMin;
            this.NameMax = nameMax;
            this.NameVal = nameVal;
            this.Min = min;
            this.Max = max;
            this.Val = val;
            this.Top = top;
            this.Down = down;
        }

        public override void OnGUI(Rect position, MaterialProperty prop, String label, MaterialEditor editor)
        {
            var internalPropMin = MaterialEditor.GetMaterialProperty(editor.targets, NameMin);
            var internalPropMax = MaterialEditor.GetMaterialProperty(editor.targets, NameMax);
            var internalPropVal = MaterialEditor.GetMaterialProperty(editor.targets, NameVal);

            if (internalPropMin.displayName != null && internalPropMax.displayName != null && internalPropVal.displayName != null)
            {
                var stylePopup = new GUIStyle(EditorStyles.popup)
                {
                    fontSize = 9,
                };

                var styleButton = new GUIStyle(EditorStyles.label)
                {

                };

                var internalValueMin = internalPropMin.floatValue;
                var internalValueMax = internalPropMax.floatValue;
                var internalValueVal = internalPropVal.floatValue;
                Vector4 propVector = prop.vectorValue;

                EditorGUI.BeginChangeCheck();

                if (propVector.w == 2)
                {
                    propVector.x = Min;
                    propVector.y = Max;
                    propVector.z = internalValueVal;
                }
                else
                {
                    if (internalValueMin <= internalValueMax)
                    {
                        propVector.w = 0;
                    }
                    else if (internalValueMin > internalValueMax)
                    {
                        propVector.w = 1;
                    }

                    if (propVector.w == 0)
                    {
                        propVector.x = internalValueMin;
                        propVector.y = internalValueMax;
                    }
                    else
                    {
                        propVector.x = internalValueMax;
                        propVector.y = internalValueMin;
                    }

                    propVector.z = Val;
                }

                GUILayout.Space(Top);

                EditorGUI.showMixedValue = prop.hasMixedValue;

                GUILayout.BeginHorizontal();

                if (GUILayout.Button(label, styleButton, GUILayout.Width(EditorGUIUtility.labelWidth), GUILayout.Height(18)))
                {
                    _showAdvancedOptions = !_showAdvancedOptions;
                }

                if (propVector.w == 2)
                {
                    propVector.z = GUILayout.HorizontalSlider(propVector.z, Min, Max);
                }
                else
                {
                    EditorGUILayout.MinMaxSlider(ref propVector.x, ref propVector.y, Min, Max);
                }

                GUILayout.Space(2);

                propVector.w = (float)EditorGUILayout.Popup((int)propVector.w, new string[] { "Remap", "Invert", "Simple" }, stylePopup, GUILayout.Width(50));

                GUILayout.EndHorizontal();

                if (_showAdvancedOptions)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(-1);
                    GUILayout.Label("      Remap Min", GUILayout.Width(EditorGUIUtility.labelWidth));
                    propVector.x = EditorGUILayout.Slider(propVector.x, Min, Max);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(-1);
                    GUILayout.Label("      Remap Max", GUILayout.Width(EditorGUIUtility.labelWidth));
                    propVector.y = EditorGUILayout.Slider(propVector.y, Min, Max);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(-1);
                    GUILayout.Label("      Simple Value", GUILayout.Width(EditorGUIUtility.labelWidth));
                    propVector.z = EditorGUILayout.Slider(propVector.z, Min, Max);
                    GUILayout.EndHorizontal();
                }

                if (propVector.w == 0f)
                {
                    internalValueMin = propVector.x;
                    internalValueMax = propVector.y;
                    internalValueVal = Val;
                }
                else if (propVector.w == 1f)
                {
                    internalValueMin = propVector.y;
                    internalValueMax = propVector.x;
                    internalValueVal = Val;
                }
                else if (propVector.w == 2f)
                {
                    internalValueMin = Min;
                    internalValueMax = Max;
                    internalValueVal = propVector.z;
                }

                EditorGUI.showMixedValue = false;

                if (EditorGUI.EndChangeCheck())
                {
                    prop.vectorValue = propVector;
                    internalPropMin.floatValue = internalValueMin;
                    internalPropMax.floatValue = internalValueMax;
                    internalPropVal.floatValue = internalValueVal;
                }

                GUILayout.Space(Down);
            }
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            return -2;
        }
    }
}