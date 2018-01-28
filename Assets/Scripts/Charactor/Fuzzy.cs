using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuzzy : MonoBehaviour {
    //カーブはすべて右肩上がり
    [SerializeField] AnimationCurve linear; //直線
    [SerializeField] AnimationCurve quadraticUp; //上目の2次間数
    [SerializeField] AnimationCurve quadraticDown; //下目の2次間数

    public float FuzzyLinear(float value, float max) {
        return linear.Evaluate(value/max);
    }

    public float FuzzyQuadraticUp(float value, float max) {
        return quadraticUp.Evaluate(value/max);
    }

    public float FuzzyQuadraticDown(float value, float max) {
        return quadraticDown.Evaluate(value/max);
    }

    //論理和
    public float FuzzyOr(float a, float b) {
        return Mathf.Max(a, b);
    }

    //論理積
    public float FuzzyAnd(float a, float b) {
        return Mathf.Min(a, b);
    }

    //論理否定
    public float FuzzyNot(float a) {
        return 1f - a;
    }

}
