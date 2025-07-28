using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] private int startingLevel = 1;
        [SerializeField] private CharacterClass characterClass = CharacterClass.Player;
        [SerializeField] private Progression progression = null;

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, startingLevel);
        }
        public int GetLevel()
        {
            //float currentExperience = GetComponent<Experience>().GetPoints();
            return 0;
        }
    }
}
