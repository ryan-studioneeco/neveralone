using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioFadeController : MonoBehaviour
{
    public UnityEvent finishedFadingEvent;
    public void FadeOut(float duration) {
        this.StartCoroutine(this.FadeOutCoroutine(duration));
    }
    public  IEnumerator FadeOutCoroutine(float fadeTime) {
        float startVolume = this.GetComponent<AudioSource>().volume;
        float startTime = UnityEngine.Time.unscaledTime;
        while ((UnityEngine.Time.unscaledTime - startTime) / fadeTime < 1) {
            float volume = Mathf.Lerp(startVolume, 0, (UnityEngine.Time.unscaledTime - startTime) / fadeTime);
            Debug.Log("volume: " + volume);
            this.GetComponent<AudioSource>().volume = Mathf.Lerp(startVolume, 0, (UnityEngine.Time.unscaledTime - startTime) / fadeTime);
            yield return null;
        }
        this.GetComponent<AudioSource>().Stop();
        this.finishedFadingEvent.Invoke();
    }
}
