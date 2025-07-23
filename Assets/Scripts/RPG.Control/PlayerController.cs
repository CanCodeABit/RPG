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
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        private void MoveToCursor()
        {
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            var hasHit = Physics.Raycast(ray, out var hit);
            if (hasHit)
            {
                _mover.MoveTo(hit.point);
            }
        }

    }
}
