using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace StudioNeeco.Events {
  public class RandomEventToggle : MonoBehaviour {
    public UnityEvent toggleTrueEvent;
    public UnityEvent toggleFalseEvent;
    public EventWithBool toggleEvent;
    public bool toggleState = false;
    [Tooltip("Random offset applied to the firing period")]
    public float trueStateRandomOffset = 0.2f;
    [Tooltip("Period of event firing in seconds")]
    public float trueStatePeriod = 1;
    [Tooltip("Random offset applied to the firing period")]
    public float falseStateRandomOffset = 0.2f;
    [Tooltip("Period of event firing in seconds")]
    public float falseStatePeriod = 1;
    [Tooltip("If true, will automatically start firing the event loop on load")]
    public bool eventEnabled = true;
    
    private void Start() {
      if (this.toggleState) this.toggleTrueEvent.Invoke();
      else this.toggleFalseEvent.Invoke();
      this.toggleEvent.Invoke(this.toggleState);
      this.StartCoroutine(this.StartEventLoop());
    }
    public void SetEventEnabled(bool enabled) {
        this.eventEnabled = enabled;
    }
    private IEnumerator StartEventLoop() {
      while (true) {
        if (this.eventEnabled) {
            Debug.Log("running?");
          this.toggleState = !this.toggleState;
          this.toggleEvent.Invoke(this.toggleState);
          if (this.toggleState) {
              Debug.Log("invoke true event");
            if (this.eventEnabled) {
                this.toggleTrueEvent.Invoke();
                yield return new WaitForSeconds(this.trueStatePeriod + Random.Range(0, this.trueStateRandomOffset));
            }
          }
          else {
              Debug.Log("invoke false event");
            if (this.eventEnabled) {
                this.toggleFalseEvent.Invoke();
                yield return new WaitForSeconds(this.falseStatePeriod + Random.Range(0, this.falseStateRandomOffset));
            }
          }
        }
        else yield return new WaitForFixedUpdate();
      }
    }
  }
}