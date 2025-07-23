using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        private static readonly int ForwardSpeed = Animator.StringToHash("forwardSpeed");
        [SerializeField] private Transform target;
        private NavMeshAgent _agent;

        private Animator _animator;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateAnimations();
        }

        public void MoveTo(Vector3 hit)
        {
            _agent.destination = hit;
        }

        private void UpdateAnimations()
        {
            var velocity = _agent.velocity;
            var localVelocity = transform.InverseTransformDirection(velocity);
            var speed = localVelocity.z;
            _animator.SetFloat(ForwardSpeed, speed);

        }
    }
}
