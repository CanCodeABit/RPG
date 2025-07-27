using UnityEngine;

namespace Boxophobic.StyledGUI
{
    public class StyledCategory : PropertyAttribute
    {
        public string Category;
        public float Top;
        public float Down;
        public bool Colapsable;

        public StyledCategory(string category)
        {
            this.Category = category;
            this.Top = 10;
            this.Down = 10;
            this.Colapsable = false;
        }

        public StyledCategory(string category, bool colapsable)
        {
            this.Category = category;
            this.Top = 10;
            this.Down = 10;
            this.Colapsable = colapsable;
        }

        public StyledCategory(string category, float top, float down)
        {
            this.Category = category;
            this.Top = top;
            this.Down = down;
            this.Colapsable = false;
        }

        public StyledCategory(string category, int top, int down, bool colapsable)
        {
            this.Category = category;
            this.Top = top;
            this.Down = down;
            this.Colapsable = colapsable;
        }
    }
}

