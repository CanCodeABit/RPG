using System.Reflection;
using RPG.Attributes;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Camera _mainCamera;
        private Mover _mover;
        private Health  _health;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _mover = GetComponent<Mover>();
            _mainCamera = Camera.main;
            _health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_health.IsDead()) return;
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
                if(target == null) continue;
                if (!GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue;
                }

                if (Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
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
                    _mover.StartMoveAction(hit.point, 1f);
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
