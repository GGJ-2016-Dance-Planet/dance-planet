using UnityEngine;
using System.Collections;

public struct button_to_press
{
	//Variables
	public float timestamp;
	public float window;
    public KeyCode[] buttons;
	
	//Constructor
	public button_to_press(float _timestamp,
	                       float _window,
                           KeyCode[] _buttons)
	{
		timestamp = _timestamp;
		window = _window;
        buttons = _buttons;
	}
	
}

