using System;
using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    public Health health;

    public float perfect = 1f/4f;
    public float good = 1f/2f;
    public float okay = 1f;

    //public event Action<List<Tuple<KeyCode, float, float>>> myEvent;

	// Use this for initialization
	void Start () {
        //myEvent += respondToInput;
	}

    void Update () {
        
    }

    IEnumerator makeRandomEvents() {
        while (true) {
            yield return new WaitForSeconds(30f);
            for (int i = 0; i < 10; i++) {
                
            }
            //var key = getRandomKey ();
            //myEvent(key, Time.time + 1f, )
        }
    }

    KeyCode getRandomKey() {
        var randfloat = UnityEngine.Random.value;
        //KeyCode key = null;
        if (randfloat < 1f) {
            return KeyCode.W;
        } else if (randfloat < 0.75f) {
            return KeyCode.A;
        } else if (randfloat < 0.5f) {
            return KeyCode.S;
        } else {
            return KeyCode.D;
        }
    }

    void respondToInput(KeyCode key, float timestamp, float offset) {
        var rating = getRatingForInput (timestamp, offset);
        health.resultHappened (rating);
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
