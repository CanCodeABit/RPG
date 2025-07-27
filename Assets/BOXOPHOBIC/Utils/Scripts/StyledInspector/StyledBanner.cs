using UnityEngine;

namespace Boxophobic.StyledGUI
{
    public class StyledBanner : PropertyAttribute
    {
        public float ColorR;
        public float ColorG;
        public float ColorB;
        public string Title;
        public string HelpURL;

        public StyledBanner(string title)
        {
            this.ColorR = -1;
            this.Title = title;
            this.HelpURL = "";
        }

        public StyledBanner(float colorR, float colorG, float colorB, string title)
        {
            this.ColorR = colorR;
            this.ColorG = colorG;
            this.ColorB = colorB;
            this.Title = title;
            this.HelpURL = "";
        }

        // Legacy
        public StyledBanner(string title, string helpURL)
        {
            this.ColorR = -1;
            this.Title = title;
            this.HelpURL = helpURL;
        }

        public StyledBanner(float colorR, float colorG, float colorB, string title, string helpURL)
        {
            this.ColorR = colorR;
            this.ColorG = colorG;
            this.ColorB = colorB;
            this.Title = title;
            this.HelpURL = helpURL;
        }

        public StyledBanner(string title, string subtitle, string helpURL)
        {
            this.ColorR = -1;
            this.Title = title;
            this.HelpURL = helpURL;
        }

        public StyledBanner(float colorR, float colorG, float colorB, string title, string subtitle, string helpURL)
        {
            this.ColorR = colorR;
            this.ColorG = colorG;
            this.ColorB = colorB;
            this.Title = title;
            this.HelpURL = helpURL;
        }
    }
}

