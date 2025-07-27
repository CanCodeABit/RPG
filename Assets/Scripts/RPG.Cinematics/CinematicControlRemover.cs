using System;
using RPG.Control;
using RPG.Core;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        private GameObject _player;
        private void Start()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        public void DisableControl(PlayableDirector director)
        {
            
            _player.GetComponent<ActionScheduler>().CancelAction();
            _player.GetComponent<PlayerController>().enabled = false;
        }

        public void EnableControl(PlayableDirector director)
        {
            _player.GetComponent<PlayerController>().enabled = true;
        }
    }
}
