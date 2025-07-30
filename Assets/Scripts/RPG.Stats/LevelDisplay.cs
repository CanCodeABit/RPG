using UnityEngine;

namespace RPG.Stats
{

    public class LevelDisplay : MonoBehaviour
    {
        private BaseStats baseStats;
        void Awake()
        {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            GetComponent<UnityEngine.UI.Text>().text = baseStats.CalculateLevel().ToString("F0");
        }
    }
}
