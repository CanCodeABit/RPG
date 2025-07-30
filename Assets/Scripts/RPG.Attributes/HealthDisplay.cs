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
            GetComponent<UnityEngine.UI.Text>().text = 
                string.Format("{0:0}/{1:0} ({2:0}%)", health.GetHealthPoints(), health.GetMaxHealthPoints(), health.GetPercentage());
        }
    }
}
