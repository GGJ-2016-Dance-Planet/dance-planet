using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class music_source_behavior : MonoBehaviour {

	private static music_source_behavior music;
	public static music_source_behavior getInstance() {
		if (music == null) {
			music = new music_source_behavior();
		}
		return music;
	}

	//The music controller
	GameObject music_controller;

	//The Music Source
	AudioSource music_source;

	//The bpm of current track
	public float bpm;

	//The number of beats (moves) per chunk
	public float beats_per_chunk = 4f;

	//The sample rate of the current track
	public float sample_rate;

	//The variables associated with level 1 track
	float level_1_bpm;
	float level_1_sample_rate;
	float level_1_beats_per_chunk;


	//The delegate to alert subscribers of which button to press and when
	public delegate void press_button(List<button_to_press> chunk);

	//The button press event to broadcast
	public event press_button pressButton;

	//The list of buttons_to_pres
	public List<button_to_press> required_user_input;
	List<button_to_press> computer_beats;


	void Start () 
	{
		//Find music controller
		music_controller = GameObject.Find ("Music Controller");

		//Find music source
		music_source = music_controller.GetComponent<AudioSource> ();

		//Set bpm/beats per chunk
		if(music_source.clip.name == "level1")
		{
			bpm = level_1_bpm;
			sample_rate = level_1_sample_rate;
			beats_per_chunk = level_1_beats_per_chunk;
		}

		//Generate list of button_to_press structs
		//Calculate number of beats in song
		int num_beats = (int) ((music_source.clip.length / 60f) * bpm);

		//Play audio clip -- make sure that clip isn't already playing
		music_source.Play ();

		//Get initial play time
		float intial_play_time = Time.time;

		//Generate list of buttons to press 
		for(int i = 0; i < num_beats; i++)
		{
			//Get Timestamp, window and key for each beat
			float timestamp = intial_play_time + (i * (60/bpm));
			float win = timestamp + (25f/bpm);
			KeyCode[] keys = generateKey();
			button_to_press b = new button_to_press(timestamp,win,keys);

			//Add button_to_press to list
			required_user_input.Add(b);

		}

		//Divide out computer's beats
		int num_chunks = (int)(num_beats / beats_per_chunk);

		for(int j = 0; j < num_chunks; j++)
		{
			if(j%2 == 0)
			{
                int start_index = (int)(beats_per_chunk*j);
				for(int k = 0; k < beats_per_chunk; k++)
				{
					//Add computer beats to relevant list
					computer_beats.Add(required_user_input[start_index + k]);

					//Delete computer beats from user list
					required_user_input.RemoveAt(start_index + k);

				}
			}
		}

		//Sync user keys to computer keys 
		for(int l = 0; l < required_user_input.Count; l++)
		{
            var userOldButtonToPress = required_user_input [l];
            var computerOldButtonToPress = computer_beats[l];
            var newButton = new button_to_press (userOldButtonToPress.timestamp, userOldButtonToPress.window, computerOldButtonToPress.buttons);
            required_user_input[l] = newButton;
		}

		if (pressButton != null)
			pressButton (required_user_input);

	}

	KeyCode[] generateKey()
	{

			//Array of potential keys
			KeyCode[] potential_keys = new KeyCode[2];
			potential_keys[1] = KeyCode.W;
			potential_keys[2] =KeyCode.S;

			//Create array
			KeyCode[] return_array = new KeyCode[1];

			//Add keys to return array
			for(int i = 0; i < return_array.Length; i++)
			{
				int random_key = Random.Range(0,potential_keys.Length);

				return_array[i] = potential_keys[random_key];

			}

			return return_array;
	}
	
	void Update () 
	{
		//Send off 

	}


}
