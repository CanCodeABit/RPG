using Unity.Mathematics.Geometry;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private RectTransform healthBar = null;
        [SerializeField] private Health health = null;
        [SerializeField] private Canvas rootCanvas = null;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Mathf.Approximately(health.GetFraction(), 0) || Mathf.Approximately(health.GetFraction(), 1))
            {
                rootCanvas.enabled = false;
                return;
            }
            rootCanvas.enabled = true;
            healthBar.localScale = new Vector3(health.GetFraction(), 1, 1);
            //healthBar.gameObject.SetActive(!health.IsDead());
        }
    }
}
