// Cristian Pop - https://boxophobic.com/

using UnityEngine;

namespace Boxophobic.StyledGUI
{
    public class StyledMessage : PropertyAttribute
    {
        public string Type;
        public string Message;
        public float Top;
        public float Down;

        public StyledMessage(string type, string message)
        {
            this.Type = type;
            this.Message = message;
            this.Top = 0;
            this.Down = 0;
        }

        public StyledMessage(string type, string message, float top, float down)
        {
            this.Type = type;
            this.Message = message;
            this.Top = top;
            this.Down = down;
        }
    }
}

