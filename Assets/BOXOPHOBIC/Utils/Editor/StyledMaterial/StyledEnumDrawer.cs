// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;

namespace Boxophobic.StyledGUI
{
    public class StyledEnumDrawer : MaterialPropertyDrawer
    {
        public string File = "";
        public string Options = "";

        public float Top = 0;
        public float Down = 0;

        public StyledEnumDrawer(string file, string options, float top, float down)
        {
            this.File = file;
            this.Options = options;

            this.Top = top;
            this.Down = down;
        }

        public override void OnGUI(Rect position, MaterialProperty prop, String label, MaterialEditor materialEditor)
        {
            GUIStyle styleLabel = new GUIStyle(EditorStyles.label)
            {
                richText = true,
                alignment = TextAnchor.MiddleCenter,
                wordWrap = true
            };

            if (Resources.Load<TextAsset>(File) != null)
            {
                var layersPath = AssetDatabase.GetAssetPath(Resources.Load<TextAsset>(File));

                StreamReader reader = new StreamReader(layersPath);

                Options = reader.ReadLine();

                reader.Close();
            }

            string[] enumSplit = Options.Split(char.Parse(" "));
            List<string> enumOptions = new List<string>(enumSplit.Length / 2);
            List<int> enumIndices = new List<int>(enumSplit.Length / 2);

            for (int i = 0; i < enumSplit.Length; i++)
            {
                if (i % 2 == 0)
                {
                    enumOptions.Add(enumSplit[i].Replace("_", " "));
                }
                else
                {
                    enumIndices.Add(int.Parse(enumSplit[i]));
                }
            }

            GUILayout.Space(Top);

            int index = (int)prop.floatValue;
            int realIndex = enumIndices[0];

            for (int i = 0; i < enumIndices.Count; i++)
            {
                if (enumIndices[i] == index)
                {
                    realIndex = i;
                }
            }

            realIndex = EditorGUILayout.Popup(prop.displayName, realIndex, enumOptions.ToArray());

            //Debug Value
            //EditorGUILayout.LabelField(enumIndices[realIndex].ToString());

            prop.floatValue = enumIndices[realIndex];

            GUILayout.Space(Down);
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            return -2;
        }
    }
}
