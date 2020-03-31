using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace StudioNeeco {
  /// <summary>
  /// Listens clicks and trigger an ordered series of events per click
  /// </summary>
  public class ClickEvents : MonoBehaviour {
    public List<UnityEvent> hitEvents = new List<UnityEvent>();
    public int hitIndex;
    public List<EventWithVector3> hitEventsWithPoint = new List<EventWithVector3>();
    public float minimumTimeBetweenClicks;
    public float timeOfLastClick;
    public bool loopEvents;
    void Update() {
      if (Input.GetMouseButtonDown(0)){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit) && Time.time > this.timeOfLastClick + this.minimumTimeBetweenClicks) {
            if (this.loopEvents || this.hitIndex < this.hitEvents.Count && this.hitEvents.Count > 0) {
                this.hitEvents[this.hitIndex % this.hitEvents.Count].Invoke();
            }
            if (this.loopEvents || this.hitIndex < this.hitEventsWithPoint.Count && this.hitEventsWithPoint.Count > 0) {
                Debug.Log("hit.point: " + hit.point);
                this.hitEventsWithPoint[this.hitIndex % this.hitEventsWithPoint.Count].Invoke(hit.point);
            }
            this.timeOfLastClick = Time.time;
            hitIndex++;
        }
      }
    }
  }
}