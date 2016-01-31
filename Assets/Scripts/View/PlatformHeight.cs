using UnityEngine;
using System.Collections;

public class PlatformHeight : MonoBehaviour
{

    public Health health;
    public float decrement = 0.3f;

    // Use this for initialization
    void Start()
    {

        // subscribe to HealthUpdate
        health.HealthUpdate += ChangeHeight;
    }

    public void ChangeHeight(float change)
    {
        
        // modify platform height amount
        transform.Translate(0, (decrement * change), 0);
        
        //if ()

    }

}

