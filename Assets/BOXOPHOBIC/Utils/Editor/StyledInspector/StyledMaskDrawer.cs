// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

namespace Boxophobic.StyledGUI
{
    [CustomPropertyDrawer(typeof(StyledMask))]
    public class StyledMaskAttributeDrawer : PropertyDrawer
    {
        StyledMask _a;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _a = (StyledMask)attribute;

            GUIStyle styleLabel = new GUIStyle(EditorStyles.label)
            {
                richText = true,
                alignment = TextAnchor.MiddleCenter,
                wordWrap = true
            };

            if (Resources.Load<TextAsset>(_a.File) != null)
            {
                var layersPath = AssetDatabase.GetAssetPath(Resources.Load<TextAsset>(_a.File));

                StreamReader reader = new StreamReader(layersPath);

                _a.Options = reader.ReadLine();

                reader.Close();
            }

            string[] enumSplit = _a.Options.Split(char.Parse(" "));
            List<string> enumOptions = new List<string>(enumSplit.Length / 2);

            for (int i = 0; i < enumSplit.Length; i++)
            {
                if (i % 2 == 0)
                {
                    enumOptions.Add(enumSplit[i].Replace("_", " "));
                }
            }

            GUILayout.Space(_a.Top);

            int index = property.intValue;

            if (_a.Display == "")
            {
                _a.Display = property.displayName;
            }

            index = EditorGUILayout.MaskField(_a.Display, index, enumOptions.ToArray());

            if (Mathf.Abs(index) > 32000)
            {
                index = -1;
            }

            //Debug Value
            EditorGUILayout.LabelField(index.ToString());

            property.intValue = index;

            GUILayout.Space(_a.Down);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return -2;
        }
    }
}
