using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace StudioNeeco {
    public class DelayedEventTrigger : MonoBehaviour
    {
        public UnityEvent triggerEvent;
        public float delay;
        public void TriggerEvent(float delay) {
            this.delay = delay;
            this.StartCoroutine(this.TriggerEventCoroutine());
        }
        public void TriggerEvent() {
            this.StartCoroutine(this.TriggerEventCoroutine());
        }
        
        private IEnumerator TriggerEventCoroutine() {
            yield return new WaitForSecondsRealtime(this.delay);
            this.triggerEvent.Invoke();
        }
    }
}