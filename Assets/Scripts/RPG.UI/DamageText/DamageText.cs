using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace RPG.UI.DamageText
{

    public class DamageText : MonoBehaviour
    {
        [SerializeField] private TMP_Text damageText = null;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public void DestroyText()
        {
            Destroy(gameObject);
        }
        public void SetDamage(float damageAmount)
        {
            if (damageText == null) return;
            damageText.text = damageAmount.ToString();
            damageText.color = damageAmount < 0 ? Color.green : Color.red;
            damageText.fontSize = Mathf.Abs(damageAmount) * 0.1f; // Adjust font size based on damage amount
        }
    }
}
