using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class TransformAnimator : MonoBehaviour
{
    public Transform target;
    public bool useThisAsTarget = true;
    public bool animatePosition;
    public Vector3 minPosition;
    public Vector3 maxPosition;
    public bool animateRotation;
    public Quaternion minRotation;
    public Quaternion maxRotation;
    public bool animateScale;
    public Vector3 minScale;
    public Vector3 maxScale;
    public bool shouldAnimate;
    public int direction = 1;
    public float position = 0;
    public float animationSpeed = 1f;
    public float initialWait;
    public bool useRelativeTransform;
    public bool shouldLoop = true;
    public bool setMinOnAwake = true;

    Transform Target() {
        if (useThisAsTarget) return this.transform;
        else return this.target;
    }
    void Awake()
    {
        if (this.setMinOnAwake) this.SetTransform(this.minPosition, this.minRotation, this.minScale);
        this.StartCoroutine(this.AnimateCoroutine());
    }
    public void SetShouldAnimate(bool shouldAnimate) {
        this.shouldAnimate = shouldAnimate;
    }

    [Button("FlipMinMax")]
    private void FlipMinMaxTransform() {
        Quaternion tempRotation = this.minRotation;
        Vector3 tempPosition = this.minPosition;
        Vector3 tempScale = this.minScale;
        this.minRotation = this.maxRotation;
        this.minPosition = this.maxPosition;
        this.minScale = this.maxScale;
        this.maxRotation = tempRotation;
        this.maxPosition = tempPosition;
        this.maxScale = tempScale;
    }

    [Button("Set Min")]
    private void SetMinFromTransform() {
        if (this.useRelativeTransform) {
            this.minPosition = this.Target().localPosition;
            this.minRotation = this.Target().localRotation;
        } else {
            this.minPosition = this.Target().position;
            this.minRotation = this.Target().rotation;
        }
        this.minScale = this.Target().localScale;
    }
    [Button("Get Min")]
    private void SetTransformFromMin() {
        if (this.useRelativeTransform) {
            this.Target().localPosition = this.minPosition;
            this.Target().localRotation = this.minRotation;
        } else {
            this.Target().position = this.minPosition;
            this.Target().rotation = this.minRotation;
        }
        this.Target().localScale = this.minScale;
    }
    [Button("Set Max")]
    private void SetMaxFromTransform() {
        if (this.useRelativeTransform) {
            this.maxPosition = this.Target().localPosition;
            this.maxRotation = this.Target().localRotation;
        } else {
            this.maxPosition = this.Target().position;
            this.maxRotation = this.Target().rotation;
        }
        this.maxScale = this.Target().localScale;
    }
    [Button("Get Max")]
    private void SetTransformFromMax() {
        if (this.useRelativeTransform) {
            this.Target().localPosition = this.maxPosition;
            this.Target().localRotation = this.maxRotation;
        } else {
            this.Target().position = this.maxPosition;
            this.Target().rotation = this.maxRotation;
        }
        this.Target().localScale = this.maxScale;
    }
    IEnumerator AnimateTransformCoroutine(Vector3 sourcePosition, Vector3 targetPosition, Quaternion sourceRotation, Quaternion targetRotation, Vector3 sourceScale, Vector3 targetScale) {
        this.position = 0f;
        float cycleTime = 0f;
        while (this.position < 1) {
            float curve = 2f;
            float positionWithCurve = Mathf.Pow(position, curve);
            float positionWithEasing = positionWithCurve / (positionWithCurve + Mathf.Pow((1 - position), curve));
            if (this.animatePosition && this.useRelativeTransform) this.Target().localPosition = Vector3.Lerp(sourcePosition, targetPosition, positionWithEasing);
            if (this.animatePosition && !this.useRelativeTransform) this.Target().position = Vector3.Lerp(sourcePosition, targetPosition, positionWithEasing);
            if (this.animateRotation && this.useRelativeTransform) this.Target().localRotation = Quaternion.Lerp(sourceRotation, targetRotation, positionWithEasing);
            if (this.animateRotation && !this.useRelativeTransform) this.Target().rotation = Quaternion.Lerp(sourceRotation, targetRotation, positionWithEasing);
            if (this.animateScale) this.Target().localScale = Vector3.Lerp(sourceScale, targetScale, positionWithEasing);
            yield return new WaitForFixedUpdate();
            cycleTime += Time.deltaTime;
            this.position = cycleTime * this.animationSpeed;
        }
        this.position = 1f;
    }
    public void SetTransform(Vector3 targetPosition, Quaternion targetRotation, Vector3 targetScale) {
        if (this.animatePosition && this.useRelativeTransform) this.Target().localPosition = targetPosition;
        if (this.animatePosition && !this.useRelativeTransform) this.Target().position = targetPosition;
        if (this.animateRotation && this.useRelativeTransform) this.Target().localRotation = targetRotation;
        if (this.animateRotation && !this.useRelativeTransform) this.Target().rotation = targetRotation; 
        if (this.animateScale) this.Target().localScale = targetScale;
    }
    public float waitInBetweenTime = 0.0f;
    IEnumerator AnimateFreezeTransformCoroutine(Vector3 sourcePosition, Quaternion sourceRotation, Vector3 sourceScale, float duration) {
        /*
            Since we use absolute rotation this allows us to keep a child bone in the same fixed position  
        */
        float timeElapsed = 0;
        while (timeElapsed < duration) {
            this.SetTransform(sourcePosition, sourceRotation, sourceScale);
            yield return new WaitForFixedUpdate();
            timeElapsed += Time.fixedDeltaTime;
        }
    }
    IEnumerator AnimateCoroutine() {
        bool firstRun = true;
        while (true) {
            if (this.shouldAnimate) {
                if (firstRun) {
                    yield return this.AnimateFreezeTransformCoroutine(this.minPosition, this.minRotation, this.minScale, this.initialWait);
                    firstRun = false;
                }
                if (this.direction == 1) yield return this.AnimateTransformCoroutine(
                    this.minPosition,
                    this.maxPosition,
                    this.minRotation,
                    this.maxRotation,
                    this.minScale,
                    this.maxScale
                );
                else  yield return this.AnimateTransformCoroutine(
                    this.maxPosition,
                    this.minPosition,
                    this.maxRotation,
                    this.minRotation,
                    this.maxScale,
                    this.minScale
                );
                Debug.Log("wait start");
                yield return new WaitForSecondsRealtime(this.waitInBetweenTime);
                Debug.Log("wait end");
                this.direction *= -1;
                this.position = 0f;
                if (!this.shouldLoop) this.shouldAnimate = false;
            } else {
                yield return new WaitForSecondsRealtime(0.5f);
            }
        }
    }
}
