using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        private bool hasTriggered = false;
        void OnTriggerEnter(Collider other)
        {
            if (!hasTriggered && other.CompareTag("Player"))
            {
                hasTriggered = true;
                GetComponent<PlayableDirector>().Play();
            }
        }
    }
}
