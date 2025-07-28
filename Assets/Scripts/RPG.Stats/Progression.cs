using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression")]
    public class Progression : ScriptableObject
    {
        [SerializeField] private ProgressionCharacterClass[] characterClasses = null;
        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookupTable = null;
        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            BuildLookupTable();
            float[] levels = lookupTable[characterClass][stat];
            if (levels.Length < level)
            {
                return 0;
            }
            return levels[level - 1];
        }
        private void BuildLookupTable()
        {
            if(lookupTable != null) return;
            lookupTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();
            foreach (ProgressionCharacterClass progressionCharacterClass in characterClasses)   
            {
                var statsLookup = new Dictionary<Stat, float[]>();
                foreach (ProgressionStat progressionStat in progressionCharacterClass.stats)
                {
                    statsLookup[progressionStat.stat] = progressionStat.levels;
                }
                lookupTable[progressionCharacterClass.characterClass] = statsLookup;
            }
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            //public float[] health;
            public ProgressionStat[] stats;
        }

        [System.Serializable]
        class ProgressionStat
        {
            public Stat stat;
            public float[] levels;
        }

    }
}