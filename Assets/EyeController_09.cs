using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EyeController_09 : MonoBehaviour
{
    public GameObject openEye;
    public GameObject closedEye;
    public float frameTime = 0.1f;
    public bool initOff = true;
    public UnityEvent openDoneEvent;
    public UnityEvent closedDoneEvent;
    void Awake()
    {
        if (this.initOff) {
            this.closedEye.SetActive(false);
            this.openEye.SetActive(false);
        }
    }
    public void AnimateOn() {
        this.StartCoroutine(this.AnimateOnCoroutine());
    }
    IEnumerator AnimateOnCoroutine() {
        this.closedEye.SetActive(true);
        this.openEye.SetActive(false);
        yield return new WaitForSecondsRealtime(this.frameTime);
        this.closedEye.SetActive(false);
        this.openEye.SetActive(true);
        this.openDoneEvent.Invoke();
    }
    public void AnimateOff() {
        this.StartCoroutine(this.AnimateOffCoroutine());
    }
    IEnumerator AnimateOffCoroutine() {
        this.closedEye.SetActive(true);
        this.openEye.SetActive(false);
        yield return new WaitForSecondsRealtime(this.frameTime);
        this.closedEye.SetActive(false);
        this.closedDoneEvent.Invoke();
    }
}
