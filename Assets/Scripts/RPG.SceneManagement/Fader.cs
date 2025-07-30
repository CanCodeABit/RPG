using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        Coroutine currentlyActiveRoutine = null;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void FadeImmediately()
        {
            _canvasGroup.alpha = 1;
        }
        private Coroutine Fade(float target, float time)
        {
            if (currentlyActiveRoutine != null)
            {
                StopCoroutine(currentlyActiveRoutine);
            }
            currentlyActiveRoutine = StartCoroutine(FadeRoutine(target, time));
            return currentlyActiveRoutine;
        }
        public Coroutine FadeIn(float time)
        {
            return Fade(0, time);
        }
        public Coroutine FadeOut(float time)
        {
            return Fade(1, time);
        }
        private IEnumerator FadeRoutine(float target, float time)
        {
            while (Mathf.Approximately(_canvasGroup.alpha, target)) yield break;
            {
                _canvasGroup.alpha = Mathf.MoveTowards(_canvasGroup.alpha, target, Time.deltaTime / time);
                yield return null;
            }
        }
    }
}
