using System;
using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;

namespace RPG.Attributes
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
        public void TakeDamage(GameObject instigator, float damage)
        {
            health = Mathf.Max(health - damage, 0);
            if (health == 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        private void AwardExperience(GameObject instigator)
        {
            var experience = instigator.GetComponent<Experience>();
            if (experience == null) return;
            var baseStats = GetComponent<BaseStats>();
            if (baseStats == null) return;
            experience.GainExperience(baseStats.GetStat(Stat.ExperienceReward));
        }

        public float GetPercentage()
        {
            return 100 * ( health / GetComponent<BaseStats>().GetStat(Stat.Health));
        }

        private void Die()
        {
            if (_isDead) return;
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
