using System;
using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] private float regeneratationPercentage = 70;
        [SerializeField] private UnityEvent<float> onTakeDamage;
        [SerializeField] private UnityEvent onDie;
        private static readonly int Dead = Animator.StringToHash("dead");
        private float health = -1f;
        private bool _isDead = false;

        void Start()
        {
           
            if (health < 0)
            {
                health = GetComponent<BaseStats>().GetStat(Stat.Health);
            }
        }
        void OnEnable()
        {
             GetComponent<BaseStats>().OnLevelUp += RegenerateHealth;
        }
        void OnDisable()
        {
             GetComponent<BaseStats>().OnLevelUp -= RegenerateHealth;
        }

        private void RegenerateHealth()
        {
            float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * (regeneratationPercentage / 100);
            health = Mathf.Max(health, regenHealthPoints);
        }

        public bool IsDead()
        {
            return _isDead;
        }
        public void TakeDamage(GameObject instigator, float damage)
        {
            health = Mathf.Max(health - damage, 0);
            if (health == 0)
            {
                onDie.Invoke();
                Die();
                AwardExperience(instigator);
            }
            else onTakeDamage.Invoke(damage);
        }
        public float GetHealthPoints()
        {
            return health;
        }
        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
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
            return 100 * GetFraction();
        }
        public float GetFraction()
        {
            return health / GetComponent<BaseStats>().GetStat(Stat.Health);
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
