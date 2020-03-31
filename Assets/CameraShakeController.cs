using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

namespace StudioNeeco {
    public class CameraShakeController : MonoBehaviour
    {
        public float magnitude = 1f;
        public float roughness = 1f;
        public float fadeInTime = 0f;
        void Start()
        {
            this.GetComponent<CameraShaker>().StartShake(this.magnitude, this.roughness, this.fadeInTime);
        }
    }
}