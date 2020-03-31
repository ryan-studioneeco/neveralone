using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StudioNeeco {
    public class EventStepper : MonoBehaviour
    {
        public List<UnityEvent> events = new List<UnityEvent>();
        public UnityEvent initEvent;
        public int eventIndex;
        public bool shouldLoop;
        void Awake()
        {
            this.initEvent.Invoke();
        }
        public void TriggerEvent() {
            if (this.shouldLoop || this.eventIndex < this.events.Count) {
                this.events[this.eventIndex % this.events.Count].Invoke();
                this.eventIndex++;
            }
        }
    }
}