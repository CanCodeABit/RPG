using System.Runtime.InteropServices.WindowsRuntime;
using RPG.Core;
using UnityEngine;
using RPG.Movement;
using RPG.Saving;
using RPG.Attributes;
namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable
    {
        private static readonly int Attack1 = Animator.StringToHash("attack");
        private static readonly int StopAttack = Animator.StringToHash("stopAttack");
       
        [SerializeField] private float timeBetweenAttacks = 1.5f;
        [SerializeField] private Transform rightHandTransform = null;
        [SerializeField] private Transform leftHandTransform = null;
        [SerializeField] private Weapon defaultWeapon = null;
        private Weapon currentWeapon = null;
        private Health _target;
        private Mover _mover;
        private Animator _animator;

        private float _timeSinceLastAttack = Mathf.Infinity;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _animator = GetComponent<Animator>();
            _mover = GetComponent<Mover>();
            if (defaultWeapon == null)
            {
                EquipWeapon(defaultWeapon);
            }
        }
        void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
            if(_target == null) return;
            if(_target.IsDead()) return;
            if (!GetInRange())
            {
                _mover.MoveTo(_target.transform.position, 1f);
            }
            else
            {
                _mover.Cancel();
                AttackBehavior();
            }
        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            weapon.Spawn(rightHandTransform,leftHandTransform, _animator);
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
            Health healthToTest = combatTarget.GetComponent<Health>();
            return combatTarget != null && !healthToTest.IsDead();
        }
        private void Hit()
        {
            if(_target == null) return;
            if(currentWeapon.HasProjectile())
            {
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, _target, gameObject);
            }
            else
            {
                _target.TakeDamage(gameObject, currentWeapon.GetWeaponDamage());
            }
           
        }
        private void Shoot()
        {
            Hit();
        }
        private bool GetInRange()
        {
            return Vector3.Distance(transform.position, _target.transform.position) < currentWeapon.GetWeaponRange();
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
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapon weapon = Resources.Load<Weapon>(weaponName);
            if (weapon == null)
            {
                Debug.LogWarning($"Weapon {weaponName} not found, equipping default weapon.");
                weapon = defaultWeapon;
            }
            EquipWeapon(weapon);
        }
    }
}
