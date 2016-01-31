using UnityEngine;
using System.Collections;

public class Changescene : MonoBehaviour {

public void ChangeToscene (string ChangeToscene) {
#pragma warning disable CS0618 // Type or member is obsolete
        Application.LoadLevel(ChangeToscene);
#pragma warning restore CS0618 // Type or member is obsolete
    }
}