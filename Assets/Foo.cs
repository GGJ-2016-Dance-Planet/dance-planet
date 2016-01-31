using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Foo : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        SceneManager.LoadScene ("Main");
	}
}
