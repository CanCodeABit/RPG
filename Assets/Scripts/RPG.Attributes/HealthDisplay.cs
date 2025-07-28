using UnityEngine;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        private Health health;
        void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            GetComponent<UnityEngine.UI.Text>().text = health.GetPercentage().ToString("F0") + "%";
        }
    }
}
