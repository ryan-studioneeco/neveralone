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
        public UnityEvent startedFadingEvent;
        private CanvasGroup canvasGroup;
        public bool isFading;
        public bool fadeOnStart;
        public float fromOpacity;
        public float toOpacity;
        public float duration;
        private void Start() {
            if (this.fadeOnStart) this.StartFadeCoroutine();
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
                        Debug.Log(newAlpha);
                        this.canvasGroup.alpha = newAlpha;
                    }
                    else Debug.LogWarning("FadeController is attempting to use a canvas group, but it was not assigned.");
                    yield return new WaitForFixedUpdate();
                    timeFading += Time.deltaTime;
                }
                this.canvasGroup.alpha = toOpacity; // guarantee it is the expected final value
                this.isFading = false;
                this.finishedFadingEvent.Invoke();
                Debug.Log("finishedFadingEvent");
                yield return null;
            } else {
                Debug.LogWarning("FadeController.Fade was called while FadeController was already performing a fade. Check the isFading property before running this coroutine.");
            }
        }
    }
}