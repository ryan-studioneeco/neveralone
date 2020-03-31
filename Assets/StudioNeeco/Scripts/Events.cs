using UnityEngine;
using UnityEngine.Events;

namespace StudioNeeco {
  /// <summary>
  /// Several Unity events with various inputs
  /// These will be exposed through the unity inspector
  /// </summary>
  [System.Serializable]
  public class EventWithVector3 : UnityEvent<Vector3> {}
  [System.Serializable]
  public class EventWithTransform : UnityEvent<Transform> {}
  [System.Serializable]
  public class EventWithHit : UnityEvent<RaycastHit> {}
  [System.Serializable]
  public class EventWithGameObject : UnityEvent<GameObject> {}
  [System.Serializable]
  public class EventWithBool : UnityEvent<bool> {}
  
}