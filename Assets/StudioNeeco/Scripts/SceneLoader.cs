using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StudioNeeco {
    public class SceneLoader : MonoBehaviour
    {
        public FadeController fadeController;
        private bool useFadePanel = true;
        public bool shouldFade = true;
        public bool useDefaultFade = true;
        private float defaultFadeTime = 1f;
        public float fadeTime = 1f;
        public List<string> sceneNames;
        public string currentSceneName;
        public int currentSceneIndex;
        public void Awake()
        {
            this.currentSceneName = SceneManager.GetActiveScene().name;
            this.currentSceneIndex = this.sceneNames.FindIndex(sceneName => sceneName == this.currentSceneName);
        }
        public void ReloadThisScene() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        public void LoadNextScene() {
            this.LoadSceneWithName(this.sceneNames[(this.currentSceneIndex + 1) % this.sceneNames.Count]);
        }
        public void LoadPreviousScene() {
            if (this.currentSceneIndex > 0) {
                this.LoadSceneWithName(this.sceneNames[this.currentSceneIndex - 1]);
            }
        }
        public void LoadSceneWithName(string name) {
            StartCoroutine(this.LoadSceneWithNameCoroutine(name));
        }
        public float FadeDuration() {
            return this.useDefaultFade ? this.defaultFadeTime : this.fadeTime;
        }
        public IEnumerator LoadSceneWithNameCoroutine(string name) {
            Debug.Log(this.fadeController == null);
            if (this.fadeController != null && this.shouldFade) yield return this.fadeController.Fade(0, 1, this.FadeDuration());
            SceneManager.LoadScene(name);
        }
    }
}