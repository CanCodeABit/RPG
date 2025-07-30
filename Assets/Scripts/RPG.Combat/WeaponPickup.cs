using System.Collections;
using RPG.Control;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] private Weapon weapon = null;
        [SerializeField] private float respawnTime = 5f;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                PickUp(other.GetComponent<Fighter>());
            }
        }

        private void PickUp(Fighter fighter)
        {
          //  fighter.EquipWeapon(weapon);
            StartCoroutine(HideForSeconds(respawnTime));
        }

        private IEnumerator HideForSeconds(float seconds)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(seconds);
            ShowPickup(true);
        }
        private void ShowPickup(bool shouldShow)
        {
            GetComponent<Collider>().enabled = shouldShow;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow);
            }
        }

        public bool HandleRaycast(PlayerController playerController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PickUp(playerController.GetComponent<Fighter>());
            }
            return true; // Indicate that the raycast was handled
        }

        public CursorType GetCursorType()
        {
            return CursorType.PickUp; // Return the appropriate cursor type for weapon pickups
        }
    }
}
