using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    // Transform of the target object (player, but can be adjusted to other gameObjects)

    public Transform target;
    public float followDrag; // how much to drag the enemy behind the player
    public float maxHealth = 5;
    public float currentHealth;
    public Transform healthBar;
    private Transform enemyTransform;
    private Rigidbody2D enemyRb;
    public GameObject Enemy;

    public Animator enemyAnim;

    // Used for calculating rotation of enemy
    private float angle;

    // Attack Variables
    public float attackCooldown = 50f;
    private float attackTimer = 0f;
    public string meleeOrProjectile = "projectile";
    public GameObject particlePrefab;
    public float attackStrength = 1f;
    public float damageRadius = 1.5f;
    private GameObject playerGameObject; // Stored when damaging player
    private ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; //gives enemy full health upon spawning
        enemyTransform = GetComponent<Transform>();
        enemyRb = GetComponent<Rigidbody2D>();

        // Set the attack timer
        attackTimer = attackCooldown;

        // Find the player game object
        playerGameObject = GameObject.FindGameObjectsWithTag("Player")[0];
        target = playerGameObject.transform;
        GameObject scoreManagerGameObject = GameObject.FindGameObjectsWithTag("Score Manager")[0];
        scoreManager = scoreManagerGameObject.GetComponent<ScoreManager>();

    }

    // Update is called once per frame
    void Update()
    {

        // Vectors to hold the current position and the goal position that the enemy is trying to get to
        Vector3 goalDestination;
        Vector3 currentPos;

        // Find the player's position
        goalDestination = target.position;

        // Current position is an interpolation between the current position and the player's
        currentPos = Vector3.Lerp(enemyRb.position, goalDestination, followDrag * Time.deltaTime);

        // Assign the new position to enemy's rigidbody
        enemyRb.position = currentPos;

        // Subtract the two vectors to get the direction's vector
        Vector3 enemyLookDir = goalDestination - currentPos;

        // Find the angle from the direction vector, and change the rotation according to it
        angle = Mathf.Atan2(enemyLookDir.y, enemyLookDir.x) * Mathf.Rad2Deg; // <-- converts from radians to degrees
        enemyTransform.rotation = Quaternion.Euler(0, 0, angle + 90);

        // Count down the attack timer
        attackTimer -= 1 * Time.deltaTime*60;
        if (attackTimer <= 0)
        {
            attackTimer = attackCooldown;

            // Do projectile attack
            if (meleeOrProjectile == "projectile")
            {
                GameObject particle = Instantiate(particlePrefab, transform.position, Quaternion.Euler(0, 0, angle + 90));
                particle.GetComponent<EnemyParticle>().playerRb = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Rigidbody2D>(); // Set references
                particle.GetComponent<EnemyParticle>().playerMvtScript = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerMovement>(); // Set references
            }

            // Do melee attack
            if (meleeOrProjectile == "melee")
            {
                // Play attack animation
                enemyAnim.SetTrigger("attack");

                // Find the player game object
                playerGameObject = GameObject.FindGameObjectsWithTag("Player")[0];

                // If the player's rigidbody is within a certain radius of the enemy...
                if (Vector2.Distance(enemyRb.position, playerGameObject.GetComponent<Rigidbody2D>().position) <= damageRadius)
                {
                    // Damage player
                    playerGameObject.GetComponent<PlayerMovement>().DamagePlayer(attackStrength);
                }
            }

        }

    }

    // Collider 
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        // If the enemy collides with a weapon...
        if (other.CompareTag("Weapon") && currentHealth > 0)
        {
            Debug.Log("Hit with weapon");

            // Deal damage according to game object name
            if (other.gameObject.name == "Sword")
            {
                currentHealth -= 3;
                if(currentHealth < 0)
                currentHealth=0;
            }
            if (other.gameObject.name == "Lance")
            {
                currentHealth -= 1;
                if(currentHealth < 0)
                currentHealth=0;
            }
            if (other.gameObject.name == "Mace")
            {
                currentHealth -= 5;
                if(currentHealth < 0)
                currentHealth=0;
            }

            Debug.Log("health:" + currentHealth);

            //Plays hit or death animation.
            if(currentHealth > 0){
                enemyAnim.SetTrigger("isHit");
            }
            else{
                enemyAnim.SetTrigger("isDead");
                scoreManager.AddPoint();
                Destroy(Enemy, 1);
            }


            // Resize health bar
            healthBar.localScale = new Vector3(currentHealth / maxHealth, healthBar.localScale.y, healthBar.localScale.z);

        }

        // If the enemy collides with a projectile
        if (other.CompareTag("Projectile") && currentHealth > 0)
        {
            Debug.Log("Hit with projectile");

            // Damage the enemy according to the particle's strength
            ParticleData pData = other.gameObject.GetComponent<ParticleData>();
            currentHealth -= pData.damageAmount;
            if(currentHealth < 0)
            currentHealth=0;

            Debug.Log("health:" + currentHealth);

            Destroy(other.gameObject);

            
            //Plays hit or death animation.
            if(currentHealth > 0){
                enemyAnim.SetTrigger("isHit");
            }
            else{
                enemyAnim.SetTrigger("isDead");
                scoreManager.AddPoint();
                
                Destroy(Enemy, 1);
            }

            // Resize health bar
            healthBar.localScale = new Vector3(currentHealth / maxHealth, healthBar.localScale.y, healthBar.localScale.z);

        }

    }
}
