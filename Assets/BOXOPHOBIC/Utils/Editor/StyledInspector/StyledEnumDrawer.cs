// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

namespace Boxophobic.StyledGUI
{
    [CustomPropertyDrawer(typeof(StyledEnum))]
    public class StyledEnumAttributeDrawer : PropertyDrawer
    {
        StyledEnum _a;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _a = (StyledEnum)attribute;

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

            GUILayout.Space(_a.Top);

            int index = property.intValue;
            int realIndex = enumIndices[0];

            for (int i = 0; i < enumIndices.Count; i++)
            {
                if (enumIndices[i] == index)
                {
                    realIndex = i;
                }
            }

            if (_a.Display == "")
            {
                _a.Display = property.displayName;
            }

            realIndex = EditorGUILayout.Popup(_a.Display, realIndex, enumOptions.ToArray());

            //Debug Value
            //EditorGUILayout.LabelField(enumIndices[realIndex].ToString());

            property.intValue = enumIndices[realIndex];

            GUILayout.Space(_a.Down);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return -2;
        }
    }
}
