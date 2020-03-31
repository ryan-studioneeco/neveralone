using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StudioNeeco {
    public class ImageSequencer : MonoBehaviour
    {
        public enum InitState {
            None,
            FirstOnOthersOff,
            AllOff
        }
        public InitState initState = InitState.None;
        public List<GameObject> frames = new List<GameObject>();
        [Tooltip("In Frames Per Second")]
        public float frameRate = 3;
        public bool playOnStart;
        void Awake() 
        {
            if (this.initState == InitState.FirstOnOthersOff) {
                frames[0].SetActive(true);   
                for (int frameIndex = 1; frameIndex < this.frames.Count; frameIndex ++) {
                    this.frames[frameIndex].SetActive(false);
                }
            } else if (this.initState == InitState.AllOff) {
                for (int frameIndex = 0; frameIndex < this.frames.Count; frameIndex ++) {
                    this.frames[frameIndex].SetActive(false);
                }
            }
        }
        void Start()
        {
            if (this.playOnStart) this.Play();
        }
        public void Play()
        {
            this.StartCoroutine(this.PlayCoroutine());
        }
        IEnumerator PlayCoroutine() {
            for (int frameIndex = 0; frameIndex < this.frames.Count; frameIndex ++) {
                this.frames[frameIndex].SetActive(true);
                yield return new WaitForSecondsRealtime(1 / this.frameRate);
                this.frames[frameIndex].SetActive(false);
            }
        }
    }
}