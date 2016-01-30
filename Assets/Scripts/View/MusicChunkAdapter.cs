using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class MusicChunkAdapter : Singleton<MusicChunkAdapter> {

    private static float chunkDelay = 1f;

    public music_source_behavior music;
    public event Action<float, button_to_press> OnChunk;

	void Start () {
//        music.pressButton += RerouteChunks;
        List<button_to_press> randomButtons = new List<button_to_press>();
        for (float f = 1; f < 100; f++) {
            var timestamp = f * chunkDelay;
            var offset = 0.9f;
            randomButtons.Add (new button_to_press (timestamp, offset, new KeyCode[]{ getRandomKey() }));
        }
        RerouteChunks (randomButtons);
	}

    KeyCode getRandomKey() {
        var randfloat = UnityEngine.Random.value;
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
        for (int i = 3; i > 0; i--) {
            yield return new WaitForSeconds (chunkDelay);
            UIManager.Instance.DisplayCenterText (i.ToString (), chunkDelay / 2f, false);
        }
        yield return new WaitForSeconds (chunkDelay);
        float timeOffset = Time.time;
        while (true) {
            var chunk = sequence.Peek ();
            var now = Time.time - timeOffset;
            if (chunk.timestamp - chunk.window - now < 0) {
                if (OnChunk != null) {
                    OnChunk (timeOffset, sequence.Dequeue ());
                }
            }
            yield return null;
        }
    }

    void RerouteChunks(List<button_to_press> sequence) {
        StartCoroutine (ChunkDaemon (new Queue<button_to_press> (sequence)));
    }
}
