using RPG.Attributes;
using UnityEngine;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        private Fighter fighter;
        void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (fighter == null || fighter.GetTarget() == null)
            {
                GetComponent<UnityEngine.UI.Text>().text = "No Target";
                return;
            }
            Health health = fighter.GetTarget();
            GetComponent<UnityEngine.UI.Text>().text = health.GetPercentage().ToString("F0") + "%";
        }
    }
}
