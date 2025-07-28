using System;
using RPG.Saving;
using UnityEngine;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] private float experiencePoints = 0f;

        public object CaptureState()
        {
            return experiencePoints;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public void GainExperience(float points)
        {
            experiencePoints += points;
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
