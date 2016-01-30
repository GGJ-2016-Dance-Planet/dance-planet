using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class MusicChunkAdapter : Singleton<MusicChunkAdapter> {

    private static float chunkDelay = 5f;

    public music_source_behavior music;
    public event Action<button_to_press> OnChunk;

	void Start () {
//        music.pressButton += RerouteChunks;
        List<button_to_press> randomButtons = new List<button_to_press>();
        for (float f = 0; f < 100; f++) {
            var timestamp = f * chunkDelay;
            var offset = 0.5f;
            randomButtons.Add (new button_to_press (timestamp, offset, new KeyCode[]{ getRandomKey() }));
        }
        RerouteChunks (randomButtons);
	}

    KeyCode getRandomKey() {
        var randfloat = UnityEngine.Random.value;
        //KeyCode key = null;
        if (randfloat < 0.25f) {
            return KeyCode.W;
        } else if (randfloat < 0.5f) {
            return KeyCode.A;
        } else if (randfloat < 0.75f) {
            return KeyCode.S;
        } else {
            return KeyCode.D;
        }
    }

    IEnumerator ChunkDaemon(Queue<button_to_press> sequence) {
        while (true) {
            var chunk = sequence.Peek ();
            var now = Time.time;
            if (chunk.timestamp - now < chunkDelay) {
                if (OnChunk != null) {
                    OnChunk (sequence.Dequeue ());
                }
            }
            yield return null;
        }
    }

    void RerouteChunks(List<button_to_press> sequence) {
        StartCoroutine (ChunkDaemon (new Queue<button_to_press> (sequence)));
    }
}
