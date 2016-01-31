using UnityEngine;
using System.Collections;

public class DummyTrigger : MonoBehaviour {

    Animator anim;

	// Use this for initialization
	void Awake () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Flash");
            Debug.Log("space");
        }
	}

    public void Flash() {
        anim.SetTrigger("Flash");
    }
}
