using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    private float health;
    private float HealthStatus {
        get {
            return health;
        }
        set {
            health = Mathf.Clamp01 (health + value);
        }
    }
    public float maxHealth;

    public void resultHappened(Ratings rating) {
        if (rating == Ratings.BAD) {
            HealthStatus -= 0.1f;
        }
    }

	// Update is called once per frame
	void Update () {
        HealthStatus = 4;
        Debug.Log (HealthStatus);
	}
}
