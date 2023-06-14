using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerParticle : MonoBehaviour
{

    // Player, Particle, and Weapon Rigidbodies
    private Rigidbody2D particleRb;
    public Rigidbody2D weaponRb;

    // Speed
    public float particleSpeed;


    // Start is called before the first frame update
    void Start()
    {

        // Particle rigidbody
        particleRb = GetComponent<Rigidbody2D>();

        // Actually change the rotation
        particleRb.rotation = weaponRb.rotation;
        particleRb.velocity = new Vector2(Mathf.Cos(particleRb.rotation*Mathf.Deg2Rad), Mathf.Sin(particleRb.rotation*Mathf.Deg2Rad)) * particleSpeed;
    }

    // Destroy particle when it hits wall
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
    
}
