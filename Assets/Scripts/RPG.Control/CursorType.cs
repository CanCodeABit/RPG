using UnityEngine;

namespace RPG.Control
{
    public enum CursorType
    {
        None,
        Movement,
        Combat,
        UI,
        PickUp // Added for weapon pickups
    }

    public static class CursorTypeExtensions
    {
        public static Texture2D GetCursorTexture(this CursorType cursorType)
        {
            // This method would return the appropriate cursor texture based on the cursor type.
            // Implementation details would depend on how textures are managed in your project.
            return null; // Placeholder for actual texture retrieval logic.
        }
    }
}

