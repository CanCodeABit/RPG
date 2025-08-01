using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float healthPoints = 100f;

        private bool isDead = false;
        private void Die()
        {
            if (isDead) return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
        }

        ////========PUBLIC METHODS========////
        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints <= 0)
            {
                Die();
                GetComponent<ActionScheduler>().CancelCurrentAction();

            }
        }
        public bool IsDead()
        {
            return isDead;
        }
    }
}
