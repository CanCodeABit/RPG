using RPG.Core;
using UnityEngine;
using RPG.Movement;
namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float weaponRange;
        private Transform _target;

        private Mover _mover;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _mover = GetComponent<Mover>();
        }

        // Update is called once per frame
        void Update()
        {
            if(_target == null) return;
            if (!GetInRange())
            {
                _mover.MoveTo(_target.position);
            }
            else
            {
                _mover.Cancel();
               // _mover.StartMoveAction(_target.position);
            }
        }

        private bool GetInRange()
        {
            return Vector3.Distance(transform.position, _target.position) < weaponRange;
        }

        public void Cancel()
        {
            _target = null;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            _target = combatTarget.transform;
        }
    }
}
