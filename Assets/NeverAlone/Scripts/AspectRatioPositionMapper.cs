using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace NKO {
    public class AspectRatioPositionMapper : MonoBehaviour
    {
        public float screenWidth;
        public float screenHeight;
        public float startAspect = 0.4618f;
        public Vector3 startPosition;
        public float endAspect;
        public Vector3 endPosition;
        public Transform target;
        public bool updateContinuous;
        public float aspect;
        void Awake()
        {
            this.screenHeight = Screen.height;
            this.screenWidth = Screen.width;
            this.UpdatePosition();
        }
        void Start()
        {   
            this.UpdatePosition();
        }
        float Aspect() {
            this.aspect = this.screenWidth / this.screenHeight;
            return this.aspect;
        }
        void UpdatePosition() {
            this.target.transform.position = Vector3.Lerp(
                this.startPosition,
                this.endPosition, 
                (this.Aspect() - this.startAspect) / (this.endAspect - this.startAspect)
            );
        }
        void Update()
        {
            
            if (Screen.width != this.screenWidth || Screen.height != this.screenHeight || updateContinuous) {
                this.screenHeight = Screen.height;
                this.screenWidth = Screen.width;
                this.UpdatePosition();
            }
        }
    }
}