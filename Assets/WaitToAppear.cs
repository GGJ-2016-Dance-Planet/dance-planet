using UnityEngine;
using System.Collections;

public class WaitToAppear : MonoBehaviour {

    public float wait;
    public float fadeTime;

	void Start () {
        this.GetComponent<SpriteRenderer> ().color = Color.clear;
        StartCoroutine (DoThing ());
	}

    IEnumerator DoThing() {
        yield return new WaitForSeconds (wait);
        yield return StartCoroutine(CoUtils.Interpolate(fadeTime, (t) => {
            this.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.clear, Color.white, t/1.5f);
        }));
    }
}
