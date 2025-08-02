using UnityEngine;
namespace RPG.Combat
{

    [CreateAssetMenu(fileName = "Weapons", menuName = "Weapons/New Weapon")]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private AnimatorOverrideController animatorOverride = null;
        [SerializeField] private GameObject equippedPrefab = null;
        [SerializeField] private float weaponDamage = 5f;
        [SerializeField] private float weaponRange = 2f;



        public void SpawnWeapon(Transform handTransform, Animator animator)
        {
            if (equippedPrefab == null) return;
            GameObject weaponInstance = Instantiate(equippedPrefab, handTransform);
            if (animator != null && animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }
        public float GetWeaponDamage()
        {
            return weaponDamage;
        }
        public float GetWeaponRange()
        {
            return weaponRange;
        }
    }
}