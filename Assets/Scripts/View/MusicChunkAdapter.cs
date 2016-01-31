using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class MusicChunkAdapter : Singleton<MusicChunkAdapter> {

    public music_source_behavior music;
    public event Action<float, button_to_press> OnPlayerChunk;
    public event Action<float, button_to_press> OnComputerChunk;
    public AudioSource audioSource;
    private float timeOffset = -1f;
    public event Action OnWin;
    public int chunksCleared = 0;

	void Awake () {
        music.userPressButton += ReroutePlayerChunks;
        music.computerPressButton += RerouteComputerChunks;
    }

    IEnumerator PlayerChunkDaemon(Queue<button_to_press> sequence, float chunkDelay) {
        audioSource.Play ();
        timeOffset = audioSource.time;
        for (int i = 3; i > 0; i--) {
            yield return new WaitForSeconds (chunkDelay/4f);
            UIManager.Instance.DisplayCenterText (i.ToString (), chunkDelay / 8f, false);
        }
        yield return new WaitForSeconds (chunkDelay/4f);
        while (sequence.Count >= 1) {
            var chunk = sequence.Peek ();
            var now = Time.timeSinceLevelLoad - timeOffset;
            if (chunk.timestamp - chunk.window - now < 0) {
                if (OnPlayerChunk != null) {
                    OnPlayerChunk (timeOffset, sequence.Dequeue ());
                }
            }
            yield return null;
        }
        yield return new WaitForSeconds (5f);
        chunksCleared++;
    }

    IEnumerator ComputerChunkDaemon(Queue<button_to_press> sequence) {
        while (timeOffset == -1f) {
            yield return null;
        }
        while (sequence.Count >= 1) {
            var chunk = sequence.Peek ();
            var now = Time.timeSinceLevelLoad - timeOffset;
            if (chunk.timestamp - chunk.window - now < 0) {
                if (OnComputerChunk != null) {
                    OnComputerChunk (timeOffset, sequence.Dequeue ());
                }
            }
            yield return null;
        }
        yield return new WaitForSeconds (5f);
        chunksCleared++;
    }

    void Update() {
        if (chunksCleared == 2) {
            OnWin ();
            chunksCleared++;
        }
    }

    void ReroutePlayerChunks(List<button_to_press> sequence, float chunkDelay) {
        StartCoroutine (PlayerChunkDaemon (new Queue<button_to_press> (sequence), chunkDelay));
    }

    void RerouteComputerChunks(List<button_to_press> sequence, float chunkDelay) {
        StartCoroutine (ComputerChunkDaemon (new Queue<button_to_press> (sequence)));
    }
}
