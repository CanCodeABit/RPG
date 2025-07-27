using UnityEngine;

namespace Boxophobic.StyledGUI
{
    public class StyledEnum : PropertyAttribute
    {
        public string Display = "";
        public string File = "";
        public string Options = "";

        public int Top = 0;
        public int Down = 0;

        public StyledEnum(string file, string options, int top, int down)
        {
            this.File = file;
            this.Options = options;

            this.Top = top;
            this.Down = down;
        }

        public StyledEnum(string display, string file, string options, int top, int down)
        {
            this.Display = display;
            this.File = file;
            this.Options = options;

            this.Top = top;
            this.Down = down;
        }
    }
}

