// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;

namespace Boxophobic.StyledGUI
{
    public class StyledMaskDrawer : MaterialPropertyDrawer
    {
        public string File = "";
        public string Options = "";

        public float Top = 0;
        public float Down = 0;

        public StyledMaskDrawer(string file, string options, float top, float down)
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

            for (int i = 0; i < enumSplit.Length; i++)
            {
                if (i % 2 == 0)
                {
                    enumOptions.Add(enumSplit[i].Replace("_", " "));
                }
            }

            GUILayout.Space(Top);

            int index = (int)prop.floatValue;

            index = EditorGUILayout.MaskField(prop.displayName, index, enumOptions.ToArray());

            if (index < 0)
            {
                index = -1;
            }

            //Debug Value
            //EditorGUILayout.LabelField(index.ToString());

            prop.floatValue = index;

            GUILayout.Space(Down);
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            return -2;
        }
    }
}
