using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StudioNeeco {
    [RequireComponent (typeof (UnityEngine.AudioSource))]
    public class MusicPlayer : MonoBehaviour
    {
        public static MusicPlayer instance;
        public List<MusicPlayerSceneClip> sceneClips = new List<MusicPlayerSceneClip> ();
        public UnityEngine.AudioClip playingClip;
        public float fadeOutTime = 3f;
        private UnityEngine.AudioSource audioSource;
        private bool completing;

        private void Awake () {
            MusicPlayerSceneClip sceneClip = this.sceneClips.Find(clip => clip.sceneName == SceneManager.GetActiveScene().name);
            if (sceneClip == null) sceneClip = this.sceneClips[0];
            this.audioSource = this.GetComponent<UnityEngine.AudioSource>();
            this.gameObject.transform.parent = null;
            DontDestroyOnLoad(this.gameObject);
            if (MusicPlayer.instance != null && sceneClip != null && sceneClip.clip != MusicPlayer.instance.playingClip) {
                MusicPlayer.instance.Complete();
            } else if (MusicPlayer.instance != null) {
                Destroy(this.gameObject);
                return;
            }
            MusicPlayer.instance = this;
            this.PlayAudioClip(
                sceneClip.clip,
                true,
                2f
            );
        }

        public void PlayAudioClip(UnityEngine.AudioClip clip, bool fadeOutIfPlaying, float fadeInTime) {
            this.audioSource.volume = 0f;
            this.audioSource.clip = clip;
            this.audioSource.Play();
            this.playingClip = clip;
            this.StartCoroutine(this.FadeIn(fadeInTime));
        }

        public IEnumerator FadeIn(float fadeTime) {
            fadeTime = 6f;
            yield return new WaitForSecondsRealtime(1.0f);
            while (this.audioSource.volume < 1 && !this.completing) {
                this.audioSource.volume +=  UnityEngine.Time.unscaledDeltaTime / fadeTime;
                yield return null;
            }
        }

        public  IEnumerator FadeOut(float fadeTime) {
            Debug.Log("FADE OUT");
            fadeTime = 2f;
            float startVolume = this.audioSource.volume;
            while (this.audioSource.volume > 0) {
                
                this.audioSource.volume -= startVolume * UnityEngine.Time.unscaledDeltaTime / fadeTime;
                yield return null;
            }
            this.audioSource.Stop();
        }
        public void Complete() {
            this.completing = true;
            this.StartCoroutine(this.CompleteCoroutine());
        }
        public IEnumerator CompleteCoroutine() {
            yield return this.FadeOut(this.fadeOutTime);
            Destroy(this.gameObject);
        }
    }
    [System.Serializable]
    public class MusicPlayerSceneClip
    {
        [SerializeField]
        public string sceneName;
        [SerializeField]
        public UnityEngine.AudioClip clip;
        [SerializeField]
        public float fadeInTime;
    }
}
