using RPG.Attributes;
using UnityEngine;

namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        private Experience experience;
        void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            GetComponent<UnityEngine.UI.Text>().text = experience.GetPoints().ToString("F0");
        }
    }
}
