using UnityEngine;
using System.Collections;

public class fading : MonoBehaviour {

	//The texture that will overlay the screen
	public Texture2D fadeOutTexture;
	public float fadeSpeed = 0.6f;

	private int drawDepth = -1000;
	private float alpha = 1.0f;
	private int fadeDirection = -1;

	void Start()
	{
		DontDestroyOnLoad (gameObject);
	}

	void OnGUI()
	{
		//fade out/in the alpha value using a direction, a speed and time.delta time
		alpha += fadeDirection * fadeSpeed * Time.unscaledDeltaTime;

		//Clamp the value between 0 and 1 because GUI.color uses alpha between 0 and 1
		alpha = Mathf.Clamp01 (alpha);

		//Set color/depth of our GUI texture
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeOutTexture);
	}

	public float BeginFade(int direction)
	{
		fadeDirection = direction;
		return(fadeSpeed);
	}

	void OnLevelWasLoaded()
	{
		alpha = 1;

		//We will fade in
		BeginFade (-1);
	}

}
