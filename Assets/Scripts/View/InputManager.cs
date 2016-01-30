﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : Singleton<InputManager> {

    public Health health;

    public float perfect = 1f/4f;
    public float good = 1f/2f;
    public float okay = 1f;

    private button_to_press currentChunk;
    private bool chunkValid = false;
    private float timeOffset;

    public Animator currentPlayerAnimator;
    public AudioSource sauce;

    private HashSet<KeyCode> validInputs = new HashSet<KeyCode> (new KeyCode[] {
        KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D,
        KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow
    });

    public event Action<Ratings> OnRating;

	// Use this for initialization
	void Start () {
        MusicChunkAdapter.Instance.OnChunk += cacheChunk;
        OnRating += (rating) => {
            Debug.Log(rating.ToString());
        };
    }

    void cacheChunk(float timeOffset, button_to_press chunk) {
        currentChunk = chunk;
        chunkValid = true;
        this.timeOffset = timeOffset;
    }

    void Update() {
        if (chunkValid) {
            var now = sauce.time - timeOffset;
            if (now > currentChunk.timestamp + currentChunk.window) {
                OnRating (Ratings.BAD);
                chunkValid = false;
            } else {
                var key = getKey ();
                if (validInputs.Contains(key)) {
                    respondToInput (getKey(), currentChunk);
                    chunkValid = false;
                }
            }
        }
    }

    KeyCode getKey() {
        foreach(KeyCode kcode in Enum.GetValues(typeof(KeyCode))) {
            if (Input.GetKeyDown (kcode)) {
                return kcode;
            }
        }
        return KeyCode.Underscore;
    }

    void respondToInput(KeyCode keyPressed, button_to_press chunk) {

        currentPlayerAnimator.SetTrigger (keyPressed.ToString ().ToLower() + "_trigger");

        var keyExpected = chunk.buttons [0];
        if (keyPressed == keyExpected) {
            var rating = getRatingForInput (chunk.timestamp, chunk.window);
            OnRating (rating);
        } else {
            OnRating (Ratings.BAD);
        }
    }

    Ratings getRatingForInput(float timestamp, float offset) {
        var now = Time.time - timeOffset;
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
