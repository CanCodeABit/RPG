// Cristian Pop - https://boxophobic.com/

using UnityEngine;

namespace Boxophobic.StyledGUI
{
    public class StyledRangeOptions : PropertyAttribute
    {
        public string Display;
        public float Min;
        public float Max;
        public string[] Options;

        public StyledRangeOptions(string display, float min, float max,  string[] options)
        {
            this.Display = display;
            this.Min = min;
            this.Max = max;

            this.Options = options;
        }
    }
}

