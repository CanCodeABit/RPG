using System;
using RPG.Core;
using RPG.Saving;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    // Mover class handles character movement using NavMeshAgent
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] private float maxSpeed = 6f;
        private NavMeshAgent agent;
        private Health health;
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }
        void Update()
        {
            agent.enabled = !health.IsDead();
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            var inverseVelocity = transform.InverseTransformDirection(agent.velocity);
            var speed = inverseVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }


        ////========PUBLIC METHODS========////
        public void MoveTo(Vector3 targetPosition, float speedFraction)
        {
            agent.destination = targetPosition;
            agent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            agent.isStopped = false;
        }
        public void Cancel()
        {
            agent.isStopped = true;
        }
        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public object CaptureState()
        {
            return  new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 serializableVector3 = (SerializableVector3)state;
            GetComponent<NavMeshAgent>().enabled = false; // Disable NavMeshAgent to prevent movement during restoration
            transform.position = serializableVector3.ToVector();
            GetComponent<NavMeshAgent>().enabled = true; // Re-enable NavMeshAgent after setting position
        }
    }
}
