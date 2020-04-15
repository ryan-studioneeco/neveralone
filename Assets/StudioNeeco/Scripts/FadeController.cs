/*
    Fade any UI object with a canvas group    
*/

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace StudioNeeco {
    [RequireComponent (typeof (CanvasGroup))]
    public class FadeController : MonoBehaviour {
        public UnityEvent finishedFadingEvent;
        public UnityEvent finishedFadingOutEvent;
        public UnityEvent startedFadingEvent;
        private CanvasGroup canvasGroup;
        public bool isFading;
        public bool fadeOnStart;
        public bool fadeInOnStart;
        public float fromOpacity;
        public float toOpacity;
        public float duration;
        public float fadeInDuration;
        public float fadeOutDuration;
        private void Awake()
        {
            if (this.fadeInOnStart) this.FadeIn();
            else if (this.fadeOnStart) this.StartFadeCoroutine();
            
        }
        private void Start() {
        }
        public void FadeIn() {
            this.StartCoroutine(this.Fade(1, 0, this.fadeInDuration));
        }
        public void FadeOut() {
            this.StartCoroutine(this.FadeOutCoroutine());
        }
        public IEnumerator FadeOutCoroutine() {
            yield return this.FadeCoroutine(1, 0, this.fadeOutDuration);
            this.finishedFadingOutEvent.Invoke();
        }
        public void StartFadeCoroutine () {
            this.StartCoroutine(this.Fade());
        }
        public IEnumerator Fade(float fromOpacity, float toOpacity, float duration) {
            yield return this.FadeCoroutine(fromOpacity, toOpacity, duration);
        }
        public IEnumerator Fade() {
            yield return this.FadeCoroutine(this.fromOpacity, this.toOpacity, this.duration);
        }
        public IEnumerator FadeCoroutine(float fromOpacity, float toOpacity, float duration) {
            Debug.Log("Start Fade");
            this.canvasGroup = this.GetComponent<CanvasGroup>();
            this.canvasGroup.alpha = this.fromOpacity;
            if (!this.isFading) {
                this.startedFadingEvent.Invoke();
                this.isFading = true;
                float timeFading = 0;
                while (timeFading < duration) {
                    if (this.canvasGroup) {
                        float newAlpha = Mathf.Lerp(fromOpacity, toOpacity, timeFading / duration);
                        // Debug.Log(newAlpha);
                        this.canvasGroup.alpha = newAlpha;
                    }
                    else Debug.LogWarning("FadeController is attempting to use a canvas group, but it was not assigned.");
                    yield return new WaitForFixedUpdate();
                    timeFading += Time.deltaTime;
                }
                this.canvasGroup.alpha = toOpacity; // guarantee it is the expected final value
                this.isFading = false;
                this.finishedFadingEvent.Invoke();
                // Debug.Log("finishedFadingEvent");
                yield return null;
            } else {
                Debug.LogWarning("FadeController.Fade was called while FadeController was already performing a fade. Check the isFading property before running this coroutine.");
            }
        }
    }
}