﻿// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;
using System;

namespace Boxophobic.StyledGUI
{
    public class StyledEmissiveIntensityDrawer : MaterialPropertyDrawer
    {
        public string Reference = "";
        public float Top = 0;
        public float Down = 0;

        public StyledEmissiveIntensityDrawer()
        {
            this.Top = 0;
            this.Down = 0;
        }

        public StyledEmissiveIntensityDrawer(string reference)
        {
            this.Reference = reference;
            this.Top = 0;
            this.Down = 0;
        }

        public StyledEmissiveIntensityDrawer(float top, float down)
        {
            this.Top = top;
            this.Down = down;
        }

        public StyledEmissiveIntensityDrawer(string reference, float top, float down)
        {
            this.Reference = reference;
            this.Top = top;
            this.Down = down;
        }

        public override void OnGUI(Rect position, MaterialProperty prop, String label, MaterialEditor editor)
        {
            var stylePopup = new GUIStyle(EditorStyles.popup)
            {
                fontSize = 9,
                alignment = TextAnchor.MiddleCenter,
            };

            var internalReference = MaterialEditor.GetMaterialProperty(editor.targets, Reference);

            Vector4 propVector = prop.vectorValue;

            GUILayout.Space(Top);

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = prop.hasMixedValue;

            GUILayout.BeginHorizontal();
            GUILayout.Space(-1);
            GUILayout.Label(label, GUILayout.Width(EditorGUIUtility.labelWidth - 1));

            GUILayout.BeginVertical();
            GUILayout.Space(3);

            if (propVector.w == 0)
            {
                propVector.y = EditorGUILayout.FloatField(propVector.y, GUILayout.Height(17));
            }
            else if (propVector.w == 1)
            {
                propVector.z = EditorGUILayout.FloatField(propVector.z, GUILayout.Height(17));
            }

            GUILayout.EndVertical();

            GUILayout.Space(2);

            propVector.w = (float)EditorGUILayout.Popup((int)propVector.w, new string[] { "Nits", "EV100" }, stylePopup, GUILayout.Width(50));

            GUILayout.EndHorizontal();

            EditorGUI.showMixedValue = false;

            if (EditorGUI.EndChangeCheck())
            {
                if (propVector.w == 0)
                {
                    propVector.x = propVector.y;
                }
                else if (propVector.w == 1)
                {
                    propVector.x = ConvertEvToLuminance(propVector.z);
                }

                if (internalReference.displayName != null)
                {
                    internalReference.floatValue = propVector.x;
                }

                prop.vectorValue = propVector;
            }

            GUILayout.Space(Down);
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            return -2;
        }

        //public float ConvertLuminanceToEv(float luminance)
        //{
        //    return (float)Math.Log((luminance * 100f) / 12.5f, 2);
        //}

        public float ConvertEvToLuminance(float ev)
        {
            return (12.5f / 100.0f) * Mathf.Pow(2f, ev);
        }
    }
}