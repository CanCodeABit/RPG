using System;
using RPG.Saving;
using UnityEngine;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        public event Action OnExperienceGained;
        [SerializeField] private float experiencePoints = 0f;

        public object CaptureState()
        {
            return experiencePoints;
        }

        public void GainExperience(float points)
        {
            experiencePoints += points;
            OnExperienceGained();
            Debug.Log($"Gained {points} experience. Total: {experiencePoints}");
        }

        public void RestoreState(object state)
        {
            experiencePoints = (float)state;
        }

        public float GetPoints()
        {
           return experiencePoints;
        }
    }
}
