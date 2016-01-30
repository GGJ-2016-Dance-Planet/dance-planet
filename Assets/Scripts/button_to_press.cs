using UnityEngine;
using System.Collections;

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

