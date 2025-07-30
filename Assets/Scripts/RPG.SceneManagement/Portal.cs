using System.Collections;
using RPG.Control;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    enum DestinationIdentifier
    {
        A, B, C, D
    }
    public class Portal : MonoBehaviour
    {
        [SerializeField] private int sceneToLoad;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private DestinationIdentifier destination;
        [SerializeField] private float fadeInTime = 1f;
        [SerializeField] private float fadeOutTime = 0.5f;
        [SerializeField] private float fadeWaitTime = 0.5f;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if (sceneToLoad < 0)
            {
                yield break;
            }
            DontDestroyOnLoad(gameObject);
            Fader fader = FindAnyObjectByType<Fader>();
            GameObject player = GameObject.FindWithTag("Player");
            var playerController = player.GetComponent<PlayerController>();
            playerController.enabled = false;

            yield return fader.FadeOut(fadeOutTime);
            
            SavingWrapper savingWrapper = FindAnyObjectByType<SavingWrapper>();
            savingWrapper.Save();
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
             GameObject newPlayer = GameObject.FindWithTag("Player");
            var newplayerController = newPlayer.GetComponent<PlayerController>();
            newplayerController.enabled = false;
            
            savingWrapper.Load();
            
            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);
            savingWrapper.Save();
            yield return new WaitForSeconds(fadeWaitTime);
            fader.FadeIn(fadeInTime);
            newplayerController.enabled = true;
            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
           GameObject player = GameObject.FindWithTag("Player");
           player.GetComponent<NavMeshAgent>().enabled = false;
           player.transform.position = otherPortal.spawnPoint.position;
           player.transform.rotation = otherPortal.spawnPoint.rotation;
           player.GetComponent<NavMeshAgent>().enabled = true;
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal otherPortal in FindObjectsByType<Portal>(FindObjectsSortMode.None))
            {
                if(otherPortal == this) continue;
                if (otherPortal.destination != destination) continue;
                return otherPortal;
            }
            return null;
        }
    }
}
