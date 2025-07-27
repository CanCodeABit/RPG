using System;
using UnityEngine;
using UnityEngine.Playables;


namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnTriggerEnter(Collider other)
        {
            bool alreadyTriggered = false;
            if(!alreadyTriggered && other.CompareTag("Player"))
                GetComponent<PlayableDirector>().Play();
        }
    }
}
