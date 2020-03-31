using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class FadeWalkController : MonoBehaviour
{
    public List<GameObject> frames = new List<GameObject>();
    public List<GameObject> disableWhileAnimating = new List<GameObject>();
    public int frameIndex;
    public float betweenFrameDuration = 1f;
    public float frameDuration = 1f;
    public float fadeInTime = 0.5f;
    public float fadeOutTime = 0.5f;
    public bool shouldPlayOnAwake;
    public bool shouldLoop;
    public bool initAllFramesOnLoad = true;
    public bool initFirstFrameEnabledOnLoad = true;
    [Tooltip("If true then first frame is animated TO on state")]
    public bool shouldAnimateFirstFrameOn;
    private bool fullAnimationRunning; // used for tracking the disable objects
    void Awake()
    {
        if (this.initAllFramesOnLoad) this.InitAllFrames();
    }
    void Start()
    {
        
        if (this.shouldPlayOnAwake) this.Animate();
    }
    void DisableDisableWhileAnimatingObjects() {
        this.disableWhileAnimating.ForEach(disableObject => disableObject.SetActive(false));
    }
    void EnableDisableWhileAnimatingObjects() {
        this.disableWhileAnimating.ForEach(disableObject => disableObject.SetActive(true));
    }
    void InitAllFrames () {
        if (initFirstFrameEnabledOnLoad) this.UpdateFrame(this.frames[0], 1);
        else this.UpdateFrame(this.frames[0], 0);
        for (int frameIndex = 1; frameIndex < this.frames.Count; frameIndex ++) {
            GameObject frame = this.frames[frameIndex];
            this.UpdateFrame(frame, 0);
        }
    }
    int CurrentFrame() {
        return this.frameIndex % this.frames.Count;
    }
    public GameObject CurrentFrameObject() {
        return this.frames[this.frameIndex % this.frames.Count];
    }
    public void Animate() {
        this.StartCoroutine(this.AnimateFramesCoroutine());
    }
    public void AnimateCurrentFrame() {
        this.StartCoroutine(this.AnimateCurrentFrameCoroutine());
    }
    public void UpdateMeshRendererAlpha(MeshRenderer renderer, float alpha) {
        Color color = renderer.material.color;
        color.a = alpha;
        renderer.material.color = color;
    }
    public void UpdateSpriteAlpha(SpriteRenderer spriteRenderer, float alpha) {
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }
    public void UpdateFrame(GameObject frame, float alpha) {
        if (frame.GetComponent<SpriteRenderer>()) this.UpdateSpriteAlpha(frame.GetComponent<SpriteRenderer>(), alpha);
        else this.UpdateMeshRendererAlpha(frame.GetComponent<MeshRenderer>(), alpha);
    }
    public void UpdateCurrentFrame(float alpha) {
        this.UpdateFrame(this.CurrentFrameObject(), alpha);
    }
    IEnumerator AnimateFrameCoroutine(float fromValue, float toValue, float duration, Action<float> updateAction) {
        float timeFading = 0;
        while (timeFading < duration) {
            float newValue = Mathf.Lerp(fromValue, toValue, timeFading / duration);
            updateAction(newValue);
            yield return new WaitForFixedUpdate();
            timeFading += Time.fixedDeltaTime;
        }
        updateAction(toValue);
    }
    public void IncrementFrameIndex() {
        this.frameIndex++;
    }
    public IEnumerator AnimateCurrentFrameCoroutine() {
        if (this.frameIndex < this.frames.Count - 1 || this.shouldLoop) {
            this.DisableDisableWhileAnimatingObjects();
            if (!this.shouldAnimateFirstFrameOn || this.frameIndex > 0) {
                yield return this.AnimateFrameCoroutine(
                    1,
                    0,
                    this.fadeInTime,
                    this.UpdateCurrentFrame
                );
                yield return new WaitForSecondsRealtime(this.betweenFrameDuration);
                this.IncrementFrameIndex();
            }
            if (this.frameIndex < this.frames.Count || this.shouldLoop) {
                yield return this.AnimateFrameCoroutine(
                    0,
                    1,
                    this.fadeInTime,
                    this.UpdateCurrentFrame
                );
            }
            this.shouldAnimateFirstFrameOn = false;
            // We only want to enable them if we were running this animation discretely
            if (!this.fullAnimationRunning) this.EnableDisableWhileAnimatingObjects();
        }
    }
    public IEnumerator AnimateFramesCoroutine() {
        this.fullAnimationRunning = true;
        yield return new WaitForSecondsRealtime(this.frameDuration);
        while (this.frameIndex < this.frames.Count || this.shouldLoop) {
            yield return this.AnimateCurrentFrameCoroutine();
            yield return new WaitForSecondsRealtime(this.frameDuration);
        }
        this.EnableDisableWhileAnimatingObjects();
        this.fullAnimationRunning = false;
        yield return null;
    }

}
