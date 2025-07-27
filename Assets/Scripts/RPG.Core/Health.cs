using RPG.Saving;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        private static readonly int Dead = Animator.StringToHash("dead");
        [SerializeField] private float health = 100f;
        
        private bool _isDead = false;

        public bool IsDead()
        {
            return _isDead;
        }
        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            if (health == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if(_isDead) return;
            _isDead = true;
            GetComponent<Animator>().SetTrigger(Dead);
            GetComponent<ActionScheduler>().CancelAction();
        }

        public object CaptureState()
        {
            return health;
        }

        public void RestoreState(object state)
        {
           health = (float)state;
           if (health == 0)
           {
               Die();
           }
        }
    }
}
