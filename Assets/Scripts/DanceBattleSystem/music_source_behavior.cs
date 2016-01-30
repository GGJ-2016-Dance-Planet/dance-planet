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
	float level_1_bpm = 99f;
	float level_1_beats_per_chunk = 4f;


	//The delegate to alert subscribers of which user/computer button to press and when
	public delegate void user_press_button(List<button_to_press> user_input);
	public delegate void computer_press_button(List<button_to_press> computer_input);

	//The user/computer button press event to broadcast
	public event user_press_button userPressButton;
	public event computer_press_button computerPressButton;

	//The list of buttons_to_pres
	public List<button_to_press> required_user_input = new List<button_to_press>();
	public List<button_to_press> computer_beats = new List<button_to_press>();


	void Start () 
	{
		//Find music controller
		music_controller = GameObject.Find ("Music Controller");

		if (music_controller != null)
			Debug.Log ("Found Music Controller");

		//Find music source
		music_source = music_controller.GetComponent<AudioSource> ();

		if (music_source != null)
			Debug.Log ("Source Found: " + music_source.clip.name);

		//Set bpm/beats per chunk
		//if (music_source.clip.name == "level1") {
			bpm = level_1_bpm;
			sample_rate = music_source.clip.frequency;
			beats_per_chunk = level_1_beats_per_chunk;
		//}

		//Generate list of button_to_press structs
		//Calculate number of beats in song

		int num_beats = (int) ((music_source.clip.length / 60f) * bpm);

		//Generate list of buttons to press 
		//Flag to flip between adding to user/computer beat stream
		bool add_to_computer = false;

		for(int i = 0; i < num_beats; i++)
		{
			//Toggle beat stream flag
			if(i % beats_per_chunk == 0)
				add_to_computer = !add_to_computer;

			//Get Timestamp, window and key for each beat
			float timestamp = (i * (60/bpm));
			float win = timestamp + (25f/bpm);
			KeyCode[] keys = generateKey();
			button_to_press b = new button_to_press(timestamp,win,keys);

			//Add button_to_press to correct list
			if(add_to_computer)
				computer_beats.Add (b);
			else
				required_user_input.Add(b);

		}

		//Send off list of computer beats
		if(computerPressButton != null)
				computerPressButton(computer_beats);
		

		//Send off list of user beats
		if (userPressButton != null)
			userPressButton (required_user_input);

		//Output our user timestamps
		outputBeatTimes (required_user_input, "User");

		//Output Computer timestamps
		outputBeatTimes (computer_beats, "Computer");

	}

	KeyCode[] generateKey()
	{

			//Array of potential keys
			KeyCode[] potential_keys = new KeyCode[2];
			potential_keys[0] = KeyCode.W;
			potential_keys[1] =KeyCode.S;

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

	void outputBeatTimes(List<button_to_press> list, string user_or_computer)
	{
		//Iterate through list and output timestamps of each button press
		for(int i = 0; i < list.Count; i++)
		{
			Debug.Log(user_or_computer + " | Beat: " + i + " | Timestamp: " + list[i].timestamp);
		}
	}


}
