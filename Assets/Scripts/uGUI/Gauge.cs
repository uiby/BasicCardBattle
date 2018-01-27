using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour {
    Image gauge;
    [SerializeField] AnimationCurve decreaseCurve;
    float maxAmount;
    float nowAmount;
    void Awake() {
        gauge = GetComponent<Image>();
    }

    //ゲージの初期化
    public void Initialize(float now, float max) {
        nowAmount = now;
        maxAmount = max;
        UpdateView(nowAmount);
    }

    public void UpdateAmount(float idealAmount) {
        StartCoroutine(Animation(idealAmount, 1f));
    }

    void UpdateView(float now) {
        gauge.fillAmount = now/maxAmount;
    }

    IEnumerator Animation(float idealAmount, float duration) {
        var startAmount = nowAmount;
        var timer = 0f;
        var rate = 0f;

        while (rate < 1) {
            timer += Time.deltaTime;
            rate = Mathf.Clamp(timer/duration, 0f, 1f);
            var curveRate = decreaseCurve.Evaluate(rate);
            UpdateView(Mathf.Lerp(startAmount, idealAmount, curveRate));
            yield return null;
        }

        nowAmount = idealAmount;
    }
}
