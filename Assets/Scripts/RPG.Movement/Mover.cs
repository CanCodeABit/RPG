using System;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    // Mover class handles character movement using NavMeshAgent
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        void Start()
        {

        }
        void Update()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            var velocity = GetComponent<NavMeshAgent>().velocity;
            var inverseVelocity = transform.InverseTransformDirection(velocity);
            var speed = inverseVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
        public void MoveTo(Vector3 targetPosition)
        {
            GetComponent<NavMeshAgent>().destination = targetPosition;

        }
    }
}
