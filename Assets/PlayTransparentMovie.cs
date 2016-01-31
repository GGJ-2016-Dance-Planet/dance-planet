using UnityEngine;
using System.Collections;

public class PlayTransparentMovie : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ((MovieTexture)GetComponent<Renderer> ().material.mainTexture).loop = true;
        ((MovieTexture)GetComponent<Renderer> ().material.GetTexture("_AlphaTex")).loop = true;
        ((MovieTexture)GetComponent<Renderer> ().material.mainTexture).Play ();
        ((MovieTexture)GetComponent<Renderer> ().material.GetTexture ("_AlphaTex")).Play ();
	}
}
