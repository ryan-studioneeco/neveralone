using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace NKO {
    public class AspectRatioFieldOfViewMapper : MonoBehaviour
    {
        public float screenWidth;
        public float screenHeight;
        public float fieldOfViewMagicNumber = -15.027792905f;
        public float lastFieldOfViewMagicNumber;
        private float originalFieldOfView;
        private float originalAspect = 0.4618f;
        public float aspect;
        public float maxAspect = 1f;
        void Awake()
        {
            this.screenHeight = Screen.height;
            this.screenWidth = Screen.width;
            this.originalFieldOfView = this.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView;
            this.UpdateFieldOfView();
        }
        void Start()
        {   
            this.UpdateFieldOfView();
        }
        float Aspect() {
            return this.screenWidth / this.screenHeight;
        }
        void UpdateFieldOfView() {
            this.aspect = this.Aspect();
            float clampedAspect = Mathf.Min(this.maxAspect, this.Aspect());
            this.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = this.originalFieldOfView + this.fieldOfViewMagicNumber * (clampedAspect - this.originalAspect);
            this.lastFieldOfViewMagicNumber = this.fieldOfViewMagicNumber;
        }
        void Update()
        {
            
            if (Screen.width != this.screenWidth || Screen.height != this.screenHeight || this.fieldOfViewMagicNumber != this.lastFieldOfViewMagicNumber) {
                this.screenHeight = Screen.height;
                this.screenWidth = Screen.width;
                this.UpdateFieldOfView();
            }
        }
    }
}