﻿using System;
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

    // declaring death delegate
    public delegate void playerdead();

    // declaring death event
    public event playerdead DeathEvent;

    // declaring health change delegate
    public delegate void healthupdate(float currentHealth);

    // declaring health change event
    public event healthupdate HealthUpdate;


    void Start()
    {
        InputManager.Instance.OnRating += resultHappened;
    }

    public void resultHappened(Ratings rating) {
         
        if (rating == Ratings.BAD)
        {
            HealthStatus -= 0.1f;
        }
        if (rating == Ratings.PERFECT)
        {
            HealthStatus += 0.1f;
        }

        HealthUpdate(health);
        	
        if (HealthStatus <=0)
        {
            // player death event
            if (DeathEvent != null)
            {
                DeathEvent();
            }
        }
	}

    

} 

