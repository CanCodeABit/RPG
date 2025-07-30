using System;
using System.Reflection;
using RPG.Attributes;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Camera _mainCamera;
        private Mover _mover;
        private Health  _health;
        [System.Serializable]
        struct CursorMapping
        {
            public CursorType cursorType;
           // public string resourceName;
            public Texture2D texture;
            public Vector2 hotspot;
        }
        [SerializeField] private CursorMapping[] cursorMappings;
        [SerializeField] private float maxNavMeshProjectionDistance = 1f;
        [SerializeField] private float maxPathLenght = 40f;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            _mover = GetComponent<Mover>();
            _mainCamera = Camera.main;
            _health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            if (InteractWithComponents())return;
            if (InteractWithUI())return;
            if (_health.IsDead())
            {
                SetCursor(CursorType.None);
                return;
            }
            if (InteractWithMovement()) return;
            SetCursor(CursorType.None);
        }

        private bool InteractWithComponents()
        {
            RaycastHit[] hits = RaycastAllSorted();
            foreach (RaycastHit hit in hits)
            {
                IRaycastable[] raycastable = hit.transform.GetComponents<IRaycastable>();
                foreach (IRaycastable raycast in raycastable)
                {
                    if (raycast.HandleRaycast(this))
                    {
                        SetCursor(raycast.GetCursorType());
                        return true;
                    }
                }

            }
            return false;
        }
        RaycastHit[] RaycastAllSorted()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            float[] distance = new float[hits.Length];
            for (int i = 0; i < hits.Length; i++)
            {
                distance[i] = hits[i].distance;
            }
            Array.Sort(distance, hits);
            return hits;
        }
        private bool InteractWithUI()
        {
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                SetCursor(CursorType.UI);
                return true;
            }
            return false;
        }
        private void SetCursor(CursorType cursorType)
        {
            CursorMapping cursorMappings = GetCursorMapping(cursorType);
            Cursor.SetCursor(
                cursorMappings.texture,
                cursorMappings.hotspot,
                CursorMode.Auto
            );
        }
        private CursorMapping GetCursorMapping(CursorType cursorType)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if (mapping.cursorType == cursorType)
                {
                    return mapping;
                }
            }
            return cursorMappings[0]; // Default to the first mapping if none found
        }
        private bool InteractWithMovement()
        {
            var hasHit = RaycastNavmesh(out Vector3 target);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    _mover.StartMoveAction(target, 1f);
                }
                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }
        private bool RaycastNavmesh(out Vector3 target)
        {
            target = Vector3.zero;
            var hasHit = Physics.Raycast(GetMouseRay(), out var hit);
            if(!hasHit)return false; // No hit detected
            NavMeshHit navMeshHit;
            var hasCastToNavmesh = NavMesh.SamplePosition(hit.point, out navMeshHit, maxNavMeshProjectionDistance, NavMesh.AllAreas);
            if (!hasCastToNavmesh) return false;
            target = navMeshHit.position;
            NavMeshPath path = new NavMeshPath();
            if (!NavMesh.CalculatePath(transform.position, navMeshHit.position, NavMesh.AllAreas, path))return false; // Path calculation failed
            if (path.status != NavMeshPathStatus.PathComplete)return false; // Path is not complete
            if(GetPathLenght(path) > maxPathLenght)return false; // Path exceeds maximum length 
            return true;
            
        }

        private float GetPathLenght(NavMeshPath path)
        {
            float totalDistance = 0f;
            if(path.corners.Length < 2) return totalDistance; // No path or only one point in the path
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                totalDistance += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }
            return totalDistance;
        }

        private Ray GetMouseRay()
        {
            return _mainCamera.ScreenPointToRay(Input.mousePosition);
        }
    }
}
