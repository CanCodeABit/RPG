using System;
using RPG.Attributes;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] private AnimatorOverrideController animatorOverride = null;
        [SerializeField] private Weapon weaponPrefab = null;
        [SerializeField] private float weaponRange;
        [SerializeField] private float weaponDamage = 10f;
        [SerializeField] private float percentageBonus = 0;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;
        const string weaponName = "Weapon";
        public Weapon Spawn(Transform rightHand, Transform leftHand, Animator _animator)
        {
            Weapon weapon = null;
            DestroyOldWeapon(rightHand, leftHand);
            if (weaponPrefab != null)
            {
                var weaponTransform = isRightHanded ? rightHand : leftHand;
                weapon = Instantiate(weaponPrefab, weaponTransform);
                weapon.gameObject.name = weaponName;

            }
            var overrideController = _animator.runtimeAnimatorController as AnimatorOverrideController;
            if (animatorOverride != null) _animator.runtimeAnimatorController = animatorOverride;
            else if (overrideController != null) _animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            return weapon;
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            var oldWeapon = rightHand.Find(weaponName);
            if (oldWeapon == null)
            {
                oldWeapon = leftHand.Find(weaponName);
            }
            if (oldWeapon == null) return;
            oldWeapon.name = "Destroying Old Weapon";
            Destroy(oldWeapon.gameObject);
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target, GameObject instigator, float calculatedDamage)
        {
            if (projectile == null) return;
            var weaponTransform = isRightHanded ? rightHand : leftHand;
            var projectileInstance = Instantiate(projectile, weaponTransform.position, Quaternion.identity);
            projectileInstance.SetTarget(instigator, target, calculatedDamage);
        }
        public float GetWeaponRange()
        {
            return weaponRange;
        }
        public float GetWeaponDamage()
        {
            return weaponDamage;
        }
        public float GetPercentageBonus()
        {
            return percentageBonus;
        }
        public bool HasProjectile()
        {
            return projectile != null;
        }
    }
}
