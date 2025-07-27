using UnityEngine;

namespace Boxophobic.StyledGUI
{
    public class StyledTexturePreview : PropertyAttribute
    {
        public string DisplayName = "";

        public StyledTexturePreview()
        {
            this.DisplayName = "";
        }

        public StyledTexturePreview(string displayName)
        {
            this.DisplayName = displayName;
        }
    }
}

