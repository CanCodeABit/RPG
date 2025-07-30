using RPG.Attributes;
using RPG.Control;
using RPG.Core;
using UnityEngine;
namespace RPG.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
        public CursorType GetCursorType()
        {
            return CursorType.Combat; // Return the appropriate cursor type for combat targets
        }

        public bool HandleRaycast(PlayerController playerController)
        {
            if (!playerController.GetComponent<Fighter>().CanAttack(gameObject))
            {
                return false; // Not a valid target
            }

            if (Input.GetMouseButton(0))
            {
                playerController.GetComponent<Fighter>().Attack(gameObject);
            }
            return true;
        }
    }
}
