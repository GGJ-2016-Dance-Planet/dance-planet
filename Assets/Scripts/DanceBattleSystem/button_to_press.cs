using UnityEngine;
using System.Collections;

public struct button_to_press
{
	//Variables
	public float timestamp;
	public float window;
	public KeyCode[] button;
	
	//Constructor
	public button_to_press(float _timestamp,
	                       float _window,
	                        KeyCode[] _button)
	{
		timestamp = _timestamp;
		window = _window;
		button = _button;
	}
	
}

