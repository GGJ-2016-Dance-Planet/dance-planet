using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (Time.timeSinceLevelLoad > 30f || Input.GetKeyDown (KeyCode.Space)) {
            SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
        }
	}
}
