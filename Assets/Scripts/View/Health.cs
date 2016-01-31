using System;
using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    private float health;

    // declaring death delegate
    public delegate void playerdead();

    // declaring death event
    public event playerdead DeathEvent;

    // declaring health change delegate
    public delegate void healthupdate(float change);

    // declaring health change event
    public event healthupdate HealthUpdate;


    void Awake()
    {
        health = 1.0f;
        InputManager.Instance.OnRating += resultHappened;
    }

    public void resultHappened(Ratings rating) {
         
        if (rating == Ratings.BAD)
        {
            health -= 0.2f;
            HealthUpdate(health);
        }
        	
        if (health <=0)
        {
            // player death event
            if (DeathEvent != null)
            {
                DeathEvent();
            }
        }
	}

    

} 

