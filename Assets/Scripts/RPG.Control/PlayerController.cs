using RPG.Combat;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Camera _mainCamera;
        private Mover _mover;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _mover = GetComponent<Mover>();
            _mainCamera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            if (InteractWithCombat()) return;
            if(InteractWithMovement()) return;
            Debug.Log("Nothing");
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target);
                }

                return true;
            }

            return false;
        }

        private bool InteractWithMovement()
        {
            var hasHit = Physics.Raycast(GetMouseRay(), out var hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    _mover.StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }
        private Ray GetMouseRay()
        {
            return _mainCamera.ScreenPointToRay(Input.mousePosition);
        }
    }
}
