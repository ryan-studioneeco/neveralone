using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace StudioNeeco {
  /// <summary>
  /// Listens for the mouse down event and fires the hitEvent on raycast hit
  /// TODO: make this validate against the object to which it belongs
  /// TODO: global version that tries to send hit message to the object that was hit (needs only one global raycast)
  /// </summary>
  public class ClickOnObject : MonoBehaviour {
    public UnityEvent hitEvent;
    public EventWithVector3 hitEventWithPoint;
    void Update() {
      if (Input.GetMouseButtonDown(0)){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit) && !EventSystem.current.IsPointerOverGameObject(-1) && !EventSystem.current.IsPointerOverGameObject(0)) {
          hitEvent.Invoke();
          hitEventWithPoint.Invoke(hit.point);
        }
      }
    }
  }
}