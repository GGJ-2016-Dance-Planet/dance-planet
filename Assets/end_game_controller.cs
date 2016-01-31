using UnityEngine;
using System.Collections;

public class end_game_controller : MonoBehaviour {

	//The player animator
	public Animator player_anim;

	//The array of dance moves
	KeyCode[] dance_moves;

	//Flag to stop dancing
	bool stop_dancing;

	void Start () {
		//Generate our dance moves
		dance_moves = generateKey();

		//DANCE!
		StartCoroutine(Dance());
	}

	void Update()
	{
		//Exit on space hit
		if (Input.GetKeyDown (KeyCode.Space)) {
			Application.Quit ();
		}
	}

	IEnumerator Dance()
	{
		//Loop Count
		int loop_count = 0;

		//Dance FOREVER
		while (!stop_dancing) 
		{
			for (int i = 0; i < dance_moves.Length; i++) {

				//Make the name of the trigger
				string trigger_name = dance_moves [i].ToString ().ToLower () + "_trigger";

				//Call the animation trigger
				player_anim.SetTrigger(trigger_name);

				//Wait 1 second before doing next dance move
				yield return new WaitForSeconds(1f);


			}

			//Increment loop count
			loop_count++;

			if (loop_count > 10)
				stop_dancing = true;

		}
			
			
	}


	KeyCode[] generateKey()
	{

		//Array of potential keys
		KeyCode[] potential_keys = new KeyCode[4];
		potential_keys [0] = KeyCode.W;
		potential_keys [1] = KeyCode.A;
		potential_keys [2] = KeyCode.S;
		potential_keys [3] = KeyCode.D;

		//Create array
		KeyCode[] return_array = new KeyCode[100];

		//Add keys to return array
		for(int i = 0; i < return_array.Length; i++)
		{
			int random_key = Random.Range(0,potential_keys.Length);

			return_array[i] = potential_keys[random_key];

		}

		Debug.Log ("Return Array Length: " + return_array.Length);

		return return_array;
	}
}
