using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown (KeyCode.Space)) {
            SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
        }
	}
}