// Cristian Pop - https://boxophobic.com/

using UnityEngine;

namespace Boxophobic.StyledGUI
{
    public class StyledText : PropertyAttribute
    {
        public string Text = "";
        public TextAnchor Alignment = TextAnchor.MiddleCenter;
        public float Top = 0;
        public float Down = 0;

        public StyledText()
        {

        }

        public StyledText(TextAnchor alignment)
        {
            this.Alignment = alignment;
        }

        public StyledText(TextAnchor alignment, float top, float down)
        {
            this.Alignment = alignment;
            this.Top = top;
            this.Down = down;
        }
    }
}

