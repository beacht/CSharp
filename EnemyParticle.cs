using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticle : MonoBehaviour
{

    // Player, Particle, and Weapon Rigidbodies
    private Rigidbody2D particleRb;
    public Rigidbody2D playerRb;
    public PlayerMovement playerMvtScript; // For accessing the player health

    // Speed
    public float particleSpeed;
    public float damageAmount;

    // Start is called before the first frame update
    void Start()
    {

        // Particle rigidbody
        particleRb = GetComponent<Rigidbody2D>();

        // Subtract the two vectors to get the direction's vector
        Vector2 lookDir = playerRb.position - particleRb.position;

        // Find the angle from the direction vector
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg; // <-- converts from radians to degrees

        // Actually change the rotation and position
        particleRb.rotation = angle;

        // Change the velocity
        particleRb.velocity = new Vector2(Mathf.Cos(particleRb.rotation * Mathf.Deg2Rad), Mathf.Sin(particleRb.rotation * Mathf.Deg2Rad)) * particleSpeed;
    }

    // Destroy particle when it hits wall
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("Player"))
        {
            playerMvtScript.DamagePlayer(damageAmount);
            Destroy(gameObject);
        }
    }

}
