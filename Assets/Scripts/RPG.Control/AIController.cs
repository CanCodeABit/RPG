using RPG.Attributes;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance;
        [SerializeField] private float suspecionTime = 3f;
        [SerializeField] private PatrolPath patrolPath;
        [SerializeField] private float waypointTolerance = 1f;
        [SerializeField] private float waypointDwellTime = 2f;

        private Fighter _fighter;
        private GameObject _player;
        private Health  _health;
        private Vector3 _guardPosition;
        private int _getCurrentWaypointIndex = 0;

        private Mover _mover;

        private float _timeSinceLastSawPlayer = Mathf.Infinity;
        private float _timeSinceArrivedAtWaypoint = Mathf.Infinity;
        [Range(0,1)]
        private float _patrolSpeedFraction = 0.2f;

        private ActionScheduler _actionScheduler;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _actionScheduler = GetComponent<ActionScheduler>();
            _mover = GetComponent<Mover>();
            _health = GetComponent<Health>();
            _fighter = GetComponent<Fighter>();
            _player = GameObject.FindWithTag("Player");
            _guardPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (_health.IsDead()) return;
            if (InAttackRangeOfPlayer() && _fighter.CanAttack(_player))
            {
                ActionBehaviour();
            }
            else if (_timeSinceLastSawPlayer < suspecionTime)
            {
                SuspecionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            _timeSinceLastSawPlayer += Time.deltaTime;
            _timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = _guardPosition;
            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    _timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }

                nextPosition = GetCurrentWaypoint();
            }

            if (_timeSinceArrivedAtWaypoint > waypointDwellTime)
            {
                _mover.StartMoveAction(nextPosition, _patrolSpeedFraction);
            }
        }
        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(_getCurrentWaypointIndex);
        }

        private void CycleWaypoint()
        {
            _getCurrentWaypointIndex = patrolPath.GetNextIndex(_getCurrentWaypointIndex);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint()); 
            return distanceToWaypoint < waypointTolerance;
        }

        private void SuspecionBehaviour()
        {
            _actionScheduler.CancelAction();
        }

        private void ActionBehaviour()
        {
            _timeSinceLastSawPlayer = 0;
            _fighter.Attack(_player);
        }

        private bool InAttackRangeOfPlayer()
        {
            return Vector3.Distance(_player.transform.position, transform.position) < chaseDistance;
        }
    }
}
