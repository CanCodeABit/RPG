using RPG.Core;
using RPG.Saving;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        private static readonly int ForwardSpeed = Animator.StringToHash("forwardSpeed");
        private NavMeshAgent _agent;
        private Animator _animator;
        private Health _health;
        private float _maxSpeed = 6f;
        private ActionScheduler _actionScheduler;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _actionScheduler = GetComponent<ActionScheduler>();
            _health = GetComponent<Health>();
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            _agent.enabled = !_health.IsDead();
            UpdateAnimations();
        }
        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            _actionScheduler.StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            _agent.destination = destination;
            _agent.speed = _maxSpeed * Mathf.Clamp01(speedFraction);
            _agent.isStopped = false;
        }

        public void Cancel()
        {
            _agent.isStopped = true;
        }

        private void UpdateAnimations()
        {
            var velocity = _agent.velocity;
            var localVelocity = transform.InverseTransformDirection(velocity);
            var speed = localVelocity.z;
            _animator.SetFloat(ForwardSpeed, speed);

        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3)state;
            transform.position = position.ToVector();
        }
    }
}
