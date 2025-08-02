using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D
        }
        [SerializeField] private int sceneToLoad = -1;
        [SerializeField] private float fadeOutDuration = 1f;
        [SerializeField] private float fadeInDuration = 1f;
        [SerializeField] private float fadeWaitDuration = 1f;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private DestinationIdentifier destination;
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if(sceneToLoad < 0)
            {
                Debug.LogError("Scene to load is not set in " + gameObject.name);
                yield break;
            }
            DontDestroyOnLoad(gameObject);
            Fader fader = FindAnyObjectByType<Fader>();
            yield return fader.FadeOut(fadeOutDuration);
            SavingWrapper savingWrapper = FindAnyObjectByType<SavingWrapper>();
            savingWrapper.Save();
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            savingWrapper.Load();
            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);
            savingWrapper.Save();
            yield return new WaitForSeconds(fadeWaitDuration);
            yield return fader.FadeIn(fadeInDuration);
            Destroy(gameObject);
        }
        private void UpdatePlayer(Portal otherPortal)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            var navMeshAgent = player.GetComponent<NavMeshAgent>();
            navMeshAgent.Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }
        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsByType<Portal>(FindObjectsSortMode.None))
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;
                return portal;
            }
            return null;
        }
    }
}
