// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;
using Boxophobic.Constants;

namespace Boxophobic.StyledGUI
{
    public class StyledBannerDrawer : MaterialPropertyDrawer
    {
        public string Title;

        public StyledBannerDrawer(string title)
        {
            this.Title = title;
        }

        public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor materialEditor)
        {
            StyledGUI.DrawInspectorBanner(Title);
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            return -4;
        }
    }
}
