using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace StudioNeeco {
    public class EyesOffController_09 : MonoBehaviour
    {
        public void AnimateAllOff() {
            Debug.Log("ANIMATE OFF!");
            List<EyeController_09> eyes = FindObjectsOfType<EyeController_09>().ToList();
            eyes.ForEach(eye => eye.AnimateOff());
        }
    }
}