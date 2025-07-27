// Cristian Pop - https://boxophobic.com/

using UnityEngine;

namespace Boxophobic.StyledGUI
{
    public class StyledIndent : PropertyAttribute
    {
        public int Indent;

        public StyledIndent(int indent)
        {
            this.Indent = indent;
        }
    }
}

