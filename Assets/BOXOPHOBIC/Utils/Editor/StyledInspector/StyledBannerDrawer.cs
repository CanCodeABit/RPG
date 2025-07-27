// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;

namespace Boxophobic.StyledGUI
{
    [CustomPropertyDrawer(typeof(StyledBanner))]
    public class StyledBannerAttributeDrawer : PropertyDrawer
    {
        StyledBanner _a;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _a = (StyledBanner)attribute;

            var bannerColor = new Color(_a.ColorR, _a.ColorG, _a.ColorB);

            if (_a.ColorR < 0)
            {
                StyledGUI.DrawInspectorBanner(_a.Title);
            }
            else
            {
                StyledGUI.DrawInspectorBanner(bannerColor, _a.Title);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return -2;
        }
    }
}
