using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace RPG.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject persistentObjectPrefab;
        static bool _hasSpawned = false;

        private void Awake()
        {
            if (_hasSpawned) return;
            SpawnPersistentObjects();
            _hasSpawned = true;
        }

        private void SpawnPersistentObjects()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}
