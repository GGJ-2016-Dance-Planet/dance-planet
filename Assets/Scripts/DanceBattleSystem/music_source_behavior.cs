using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class music_source_behavior : MonoBehaviour {

	//The music controller
	GameObject music_controller;

	//The Music Source
	AudioSource music_source;

	//The bpm of current track
	public float bpm;

	//The number of beats (moves) per chunk
	public float beats_per_chunk = 4f;

	//The sample rate of the current track
	//public int sample_rate;

	//The variables associated with level 1 track
	float level_1_bpm;
	//float level_1_sample_rate;
	float level_1_beats_per_chunk;


	//The delegate to alert subscribers of which button to press and when
	public delegate void press_button(List<button_to_press> chunk);

	//The button press event to broadcast
	public event press_button pressButton;


	void Start () 
	{
		//Find music controller
		music_controller = GameObject.Find ("Music Controller");

		//Find music source
		music_source = music_controller.GetComponent<AudioSource> ();

		//Set bpm/sample rate
		if(music_source.clip.name == "level1")
		{
			bpm = level_1_bpm;
			//sample_rate = level_1_sample_rate;
			beats_per_chunk = level_1_beats_per_chunk;
		}

	}
	
	void Update () 
	{
		//Send off 

	}

	public IEnumerator createButtonChunk()
	{
		yield return null;
	}

	public struct button_to_press
	{
		//Variables
		public float timestamp;
		public float window;
		string button;

		//Constructor
		public button_to_press(float _timestamp,
		                       float _window,
		                       string _button)
		{
			timestamp = _timestamp;
			window = _window;
			button = _button;
		}

	}
}
