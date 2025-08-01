using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(transform.GetChild(i).position, 0.2f);
                Gizmos.DrawLine(GetWaypoints(i), GetWaypoints(j));
            }
        }

        public Vector3 GetWaypoints(int i)
        {
            return transform.GetChild(i).position;
        }
        public int GetNextIndex(int i)
        {
            if (i + 1 == transform.childCount) return 0;
            return i + 1;

        }
    }
}
