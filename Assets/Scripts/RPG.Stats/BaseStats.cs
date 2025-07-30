using System;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] private int startingLevel = 1;
        [SerializeField] private CharacterClass characterClass = CharacterClass.Player;
        [SerializeField] private Progression progression = null;
        [SerializeField] private GameObject levelUpParticleEffect = null;
        [SerializeField] private bool useModifiers = false;

        public event Action OnLevelUp;
        private int currentLevel = 0;
        Experience experience;
        void Awake()
        {
           experience = GetComponent<Experience>();
        }
        void Start()
        {
            currentLevel = CalculateLevel();
        }
        void OnEnable()
        {
             if (experience != null)
            {
                experience.OnExperienceGained += UpdateLevel;
            }
        }
        void OnDisable()
        {
            if (experience != null)
            {
                experience.OnExperienceGained -= UpdateLevel;
            }
        }
        void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if (newLevel > currentLevel)
            {
                currentLevel = newLevel;
                LevelUpEffect();
                OnLevelUp?.Invoke();
            }
        }

        private void LevelUpEffect()
        {
            Instantiate(levelUpParticleEffect, transform);
        }

        public float GetStat(Stat stat)
        {
            return (GetBaseStat(stat) + GetAdditiveModifier(stat)) * (1 + GetPercentageModifier(stat) / 100);
        }

        private float GetPercentageModifier(Stat stat)
        {
            if (!useModifiers) return 0;
            float total = 0;
            foreach (var provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetPercentageModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        private float GetBaseStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, CalculateLevel());
        }

        private float GetAdditiveModifier(Stat stat)
        {
            if (!useModifiers) return 0;
            float total = 0;
            foreach (var provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetAdditiveModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        public int GetLevel()
        {
            if (currentLevel < 1)
            {
                currentLevel = CalculateLevel();
            }
            return currentLevel;
        }
        public int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();
            if (experience == null) return startingLevel;
            float currentExperience = experience.GetPoints();
            int levels = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);
            for (int level = 1; level <= levels; level++)
            {
                float experienceToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
                if (currentExperience > experienceToLevelUp)
                {
                    return level;
                }
            }
            return levels + 1;
        }
    }
}
