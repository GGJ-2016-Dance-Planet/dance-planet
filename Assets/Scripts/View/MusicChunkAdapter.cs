using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class MusicChunkAdapter : Singleton<MusicChunkAdapter> {

    public music_source_behavior music;
    public event Action<float, button_to_press> OnChunk;
    public AudioSource audioSource;

	void Awake () {
        music.userPressButton += RerouteChunks;
    }

    IEnumerator ChunkDaemon(Queue<button_to_press> sequence, float chunkDelay) {
        audioSource.Play ();
        for (int i = 3; i > 0; i--) {
            yield return new WaitForSeconds (chunkDelay);
            UIManager.Instance.DisplayCenterText (i.ToString (), chunkDelay / 2f, false);
        }
        yield return new WaitForSeconds (chunkDelay);
        float timeOffset = audioSource.time;
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

    void RerouteChunks(List<button_to_press> sequence, float chunkDelay) {
        StartCoroutine (ChunkDaemon (new Queue<button_to_press> (sequence), chunkDelay));
    }
}
