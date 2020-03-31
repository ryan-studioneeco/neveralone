using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace StudioNeeco {
  /// <summary>
  /// Listens clicks and trigger an ordered series of events per click
  /// </summary>
  public class WalkingClickEvents : MonoBehaviour {
      
    public FadeWalkController fadeWalkController;
    public int hitIndex;
    void Update() {
      if (Input.GetMouseButtonDown(0)){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit)) {
            this.fadeWalkController.AnimateCurrentFrame();
        }
      }
    }
  }
}