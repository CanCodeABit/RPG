// Cristian Pop - https://boxophobic.com/

using UnityEngine;

namespace Boxophobic.StyledGUI
{
    public class StyledButton : PropertyAttribute
    {
        public string Text = "";
        public float Top = 0;
        public float Down = 0;

        public StyledButton(string text)
        {
            this.Text = text;
            this.Top = 0;
            this.Down = 0;
        }

        public StyledButton(string text, float top, float down)
        {
            this.Text = text;
            this.Top = top;
            this.Down = down;
        }
    }
}

