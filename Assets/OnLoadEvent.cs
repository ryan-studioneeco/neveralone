using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StudioNeeco {
    public class OnLoadEvent : MonoBehaviour
    {
        public List<UnityEvent> events = new List<UnityEvent>();
        public float delay;
        void Start()
        {
            this.StartCoroutine(this.RunOnLoadEventCoroutine());
        }
        IEnumerator RunOnLoadEventCoroutine() {
            yield return new WaitForSecondsRealtime(this.delay);
            this.events.ForEach(loadEvent => loadEvent.Invoke());
        }
    }
}