using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour {

    public Health health;

    public float perfect = 1f/4f;
    public float good = 1f/2f;
    public float okay = 1f;

	// Use this for initialization
	void Start () {
        MusicChunkAdapter.Instance.OnChunk += (button_to_press obj) => {
            Debug.Log(obj.buttons[0].ToString());
        };
	}

    void Update () {
        
    }

    void respondToInput(button_to_press chunks) {
        var key = chunks.buttons [0];
//        var rating = getRatingForInput (chunks, offset);
        //health.resultHappened (rating);
    }

    Ratings getRatingForInput(float timestamp, float offset) {
        var now = Time.time;
        var rating = Mathf.Abs (now - timestamp);
        if (rating < offset * perfect) {
            return Ratings.PERFECT;
        } else if (rating < offset * good) {
            return Ratings.GOOD;
        } else if (rating < offset * okay) {
            return Ratings.OKAY;
        } else {
            return Ratings.BAD;
        }
    }
}
