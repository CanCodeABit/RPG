using System;
using System.Collections;
using RPG.Saving;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        private SavingSystem _savingSystem;

        private const string DefaultSaveFile = "save";
        [SerializeField] private float fadeInTime = 0.2f;
        Fader _fader;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Awake()
        {
            StartCoroutine(LoadLastScene());
        }

        IEnumerator LoadLastScene()
        {
            
            yield return GetComponent<SavingSystem>().LoadLastScene(DefaultSaveFile);
            _fader = FindAnyObjectByType<Fader>();
            _fader.FadeImmediately();
            yield return _fader.FadeIn(fadeInTime);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S)) Save();
            if (Input.GetKeyUp(KeyCode.L)) Load();
            if (Input.GetKeyDown(KeyCode.Delete)) Delete();
        }

        public void Load()
        {
            _savingSystem.Load(DefaultSaveFile);
        }

        public void Save()
        {
            _savingSystem.Save(DefaultSaveFile);
        }
        public void Delete()
        {
            _savingSystem.Delete(DefaultSaveFile);
        }
    }
}
