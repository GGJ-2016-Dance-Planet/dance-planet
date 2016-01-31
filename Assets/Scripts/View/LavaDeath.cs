using UnityEngine;
using System.Collections;

public class LavaDeath : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {

        // player disappears
        Destroy(other.gameObject);

        // lava splash
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ps.Play();
        
        
    }
}