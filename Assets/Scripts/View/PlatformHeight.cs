using UnityEngine;
using System.Collections;

public class PlatformHeight : MonoBehaviour
{

    public Health health;

    // Use this for initialization
    void Start()
    {

        // subscribe to HealthUpdate
        health.HealthUpdate += ChangeHeight;
    }

    public void ChangeHeight(float change)
    {
        // modify platform height "change" amount
        transform.localScale = new Vector3(0f, change, 0f);

    }

}

