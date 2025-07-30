using System.Runtime.InteropServices.WindowsRuntime;
using RPG.Core;
using UnityEngine;
using RPG.Movement;
using RPG.Saving;
using RPG.Attributes;
using Unity.VisualScripting;
using RPG.Stats;
using System.Collections.Generic;
using System;
namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable, IModifierProvider
    {
        private static readonly int Attack1 = Animator.StringToHash("attack");
        private static readonly int StopAttack = Animator.StringToHash("stopAttack");
       
        [SerializeField] private float timeBetweenAttacks = 1.5f;
        [SerializeField] private Transform rightHandTransform = null;
        [SerializeField] private Transform leftHandTransform = null;
        [SerializeField] private WeaponConfig defaultWeapon = null;
        private WeaponConfig currentWeaponConfig = null;
        private Health _target;
        private Mover _mover;
        private Animator _animator;
        private Weapon _currentWeapon;

        private float _timeSinceLastAttack = Mathf.Infinity;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            _animator = GetComponent<Animator>();
            _mover = GetComponent<Mover>();
            currentWeaponConfig = defaultWeapon;
            _currentWeapon = SetDefaultWeapon();
        }
        void Start()
        {
            EquipWeapon(currentWeaponConfig);
        }
        void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
            if(_target == null) return;
            if(_target.IsDead()) return;
            if (!GetInRange(_target.transform))
            {
                _mover.MoveTo(_target.transform.position, 1f);
            }
            else
            {
                _mover.Cancel();
                AttackBehavior();
            }
        }
        private Weapon SetDefaultWeapon()
        {
            return AttachWeapon(defaultWeapon);
        }
        public void EquipWeapon(WeaponConfig weapon)
        {
            currentWeaponConfig = weapon;
            AttachWeapon(weapon);
        }
        private Weapon AttachWeapon(WeaponConfig weapon)
        {
           return weapon.Spawn(rightHandTransform, leftHandTransform, _animator);
        }
        private void AttackBehavior()
        {
            transform.LookAt(_target.transform);
            if (_timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                _timeSinceLastAttack = 0f;
            }
        }

        private void TriggerAttack()
        {
            _animator.ResetTrigger(StopAttack);
            _animator.SetTrigger(Attack1);
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null)
            {
                return false;
            };
            if (!_mover.CanMoveTo(combatTarget.transform.position) &&
            !GetInRange(combatTarget.transform))
            {
                return false;
            }
            Health healthToTest = combatTarget.GetComponent<Health>();
            return combatTarget != null && !healthToTest.IsDead();
        }
        private void Hit()
        {
            if(_target == null) return;
            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);
            if(_currentWeapon != null) //TODO: lazy value shit check
            {
                _currentWeapon.OnHit();
            }
            if (currentWeaponConfig.HasProjectile())
            {
                currentWeaponConfig.LaunchProjectile(rightHandTransform, leftHandTransform, _target, gameObject, damage);
            }
            else
            {
                _target.TakeDamage(gameObject, damage);
            }
           
        }
        private void Shoot()
        {
            Hit();
        }
        private bool GetInRange(Transform targetTransform)
        {
            return Vector3.Distance(transform.position, targetTransform.position) < currentWeaponConfig.GetWeaponRange();
        }
        public Health GetTarget()
        {
            return _target;
        }
        public void Cancel()
        {
            _target = null;
            StopAttackTrigger();
            GetComponent<Mover>().Cancel();
        }

        private void StopAttackTrigger()
        {
            GetComponent<Animator>().ResetTrigger(Attack1);
            GetComponent<Animator>().SetTrigger(StopAttack);
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            _target = combatTarget.GetComponent<Health>();
        }

        public object CaptureState()
        {
            return currentWeaponConfig.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            WeaponConfig weapon = Resources.Load<WeaponConfig>(weaponName);
            if (weapon == null)
            {
                Debug.LogWarning($"Weapon {weaponName} not found, equipping default weapon.");
                weapon = defaultWeapon;
            }
            EquipWeapon(weapon);
        }

        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            if(stat == Stat.Damage)
            {
                yield return currentWeaponConfig.GetWeaponDamage();
            }
        }

         public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            if(stat == Stat.Damage)
            {
                yield return currentWeaponConfig.GetPercentageBonus();
            }
        }

        public float GetFlatModifier(Stat stat)
        {
            throw new System.NotImplementedException();
        }

        public float GetMultiplicativeModifier(Stat stat)
        {
            throw new System.NotImplementedException();
        }
    }
}
