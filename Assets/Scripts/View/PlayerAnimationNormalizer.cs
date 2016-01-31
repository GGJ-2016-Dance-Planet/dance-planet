using UnityEngine;
using System.Collections;

public class PlayerAnimationNormalizer : MonoBehaviour {

    public Animator animator;
    public music_source_behavior music;

	// Use this for initialization
	void Awake () {
        music.computerPressButton += (computer_input, chunkDelay) => {
            var foo = 1f / (chunkDelay / (float)music.beats_per_chunk);
            animator.speed = foo;
        };
	}
}
