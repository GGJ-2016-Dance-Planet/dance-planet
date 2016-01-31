using UnityEngine;
using System.Collections;

public class PlayMovie : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ((MovieTexture)GetComponent<Renderer>().material.mainTexture).Play();
	}

    void Update() {
        if (Time.timeSinceLevelLoad > 30) {
            ((MovieTexture)GetComponent<Renderer> ().material.mainTexture).Pause ();
        }
    }
}
