using UnityEngine;

namespace Boxophobic.StyledGUI
{
    public class StyledSpace : PropertyAttribute
    {
        public int Space;

        public StyledSpace(int space)
        {
            this.Space = space;
        }
    }
}

