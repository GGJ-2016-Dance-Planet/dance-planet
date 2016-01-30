using UnityEngine;
using System.Collections;
using System;

public class CoUtils : MonoBehaviour {
    public static IEnumerator Interpolate(float time, Action<float> callback) {
        float startTime = Time.time;
        while (Time.time-startTime < time) {
            callback((Time.time - startTime)/time);
            yield return null;
        }
        callback (1);
    }
}
