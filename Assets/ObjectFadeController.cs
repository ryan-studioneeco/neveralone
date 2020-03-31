/*
    Fade any mesh or sprite object
*/

using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace StudioNeeco {
    public class ObjectFadeController : MonoBehaviour {
        public UnityEvent finishedFadingEvent;
        public UnityEvent startedFadingEvent;
        public bool isFading;
        public bool fadeOnStart;
        public bool initToFromValueOnStart;
        public float fromOpacity;
        public float toOpacity;
        public float duration;
        private void Start() {
            if (this.initToFromValueOnStart) this.UpdateAlpha(this.fromOpacity);
            if (this.fadeOnStart) this.StartFade();
        }
        public void StartFade () {
            this.StartCoroutine(this.Fade());
        }
        public IEnumerator Fade(float fromOpacity, float toOpacity, float duration) {
            yield return this.FadeCoroutine(fromOpacity, toOpacity, duration);
        }
        public IEnumerator Fade() {
            yield return this.FadeCoroutine(this.fromOpacity, this.toOpacity, this.duration);
        }
        public void UpdateMeshRendererAlpha(MeshRenderer renderer, float alpha) {
            Color color = renderer.material.color;
            color.a = alpha;
            renderer.material.color = color;
        }
        public void UpdateSpriteAlpha(SpriteRenderer spriteRenderer, float alpha) {
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }
        public void UpdateAlpha(float alpha) {
            if (this.GetComponent<SpriteRenderer>()) this.UpdateSpriteAlpha(this.GetComponent<SpriteRenderer>(), alpha);
            else this.UpdateMeshRendererAlpha(this.GetComponent<MeshRenderer>(), alpha);
        }
        public IEnumerator FadeCoroutine(float fromOpacity, float toOpacity, float duration) {
            this.UpdateAlpha(fromOpacity);
            if (!this.isFading) {
                this.startedFadingEvent.Invoke();
                this.isFading = true;
                float timeFading = 0;
                while (timeFading < duration) {
                    float newAlpha = Mathf.Lerp(fromOpacity, toOpacity, timeFading / duration);
                    this.UpdateAlpha(newAlpha);
                    yield return new WaitForFixedUpdate();
                    timeFading += Time.deltaTime;
                }
                this.UpdateAlpha(toOpacity);
                this.isFading = false;
                this.finishedFadingEvent.Invoke();
                yield return null;
            }
        }
    }
}