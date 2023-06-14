using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // The Rigidbody Component on the Player
    Rigidbody2D playerRb;

    // Animator for za great and glorious goober
    public Animator gooberAnim;
    public Transform gooberTransform;

    //Weapon variables
    public GameObject WeaponContainer;
    public GameObject Sword;
    public GameObject Lance;
    public GameObject Mace;
    public GameObject Bow;
    public GameObject PokerChip;
    public GameObject SlotMachineGun;

    //Expression variables
    public GameObject exp01;
    public GameObject exp02;
    public GameObject exp03;
    public GameObject exp04;
    public GameObject exp05;
    public GameObject exp06;

    //Health Variables
    public float maxHealth = 5;
    public float currentHealth;

    // Particle Animator
    public ParticleSystem rollParticles;

    // Floats for storing the inputs
    float hInput;
    float vInput;

    // Previous velocities (stored to decide the player's idle animation)
    float previousXVel;
    float previousYVel;

    // Player Dash Variables
    public float currentSpeed; // The current speed of the player
    public float playerSpeed; // The normal player speed

    public bool dashing; //duh
    public float dashStrength; // The strength of the dash
    public float dashLength; // The length of the dash
    private float speedDecrease; // How much to decrease the speed per frame to match the dash length

    public float cooldownLength; // How long the cooldown should last in frames
    private float cooldownTimer = 0; // Manages the timer for the cooldown

    private bool readyToDash = false; // is the player ready to dash

    // Managing player rotation
    public float rotationSpeed = 15;

    public TriggerAttacks triggerAttacks;

    // Store reference to camera object
    public FollowTarget camScript;

    // Sound Audio Sources
    private AudioSource steppingLoop;
    public AudioSource dashSound;
    public AudioSource weaponSelectSound;
    public AudioSource damageSound;
    private bool startingUpTest;

    // Start is called before the first frame update
    void Start()
    {
        // Get the rigidbody component
        playerRb = GetComponent<Rigidbody2D>();

        //Sets health to full
        currentHealth = maxHealth;

        // Dash initialization
        currentSpeed = playerSpeed;
        speedDecrease = dashStrength / dashLength; //This is dividing the maximum speed of the dash by the cooldown time. The player's speed gets decremented by this every frame during the cooldown.

        // Get the audio sources
        steppingLoop = GetComponent<AudioSource>();

        // So that the weapon select does not play when the player loads in the scene
        startingUpTest = true;

    }

    // Update is called once per frame
    void Update()
    {
        // Store inputs each frame
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        // Change animation movement state
        if (hInput != 0 || vInput != 0)
        {
            gooberAnim.SetBool("isMoving", true);
            if (!steppingLoop.isPlaying)
            {
                steppingLoop.Play();
            }
        }
        else
        {
            gooberAnim.SetBool("isMoving", false);
            if (steppingLoop.isPlaying)
            {
                steppingLoop.Pause();
            }
        }

        // Decide if the player can dash this frame
        readyToDash = cooldownTimer >= cooldownLength;

        // If space key is pressed and ready to dash...
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("SPACE BUTTON PRESSED");
            if (readyToDash)
            {
                //Debug.Log("DASH");
                currentSpeed += dashStrength;
                cooldownTimer = 0;
                gooberAnim.SetTrigger("dashTrigger");
                rollParticles.Play();
                dashSound.PlayDelayed(0.5f);
                weaponSelectSound.PlayDelayed(1.5f);

                int newWeapon = (Random.Range(1, 1000001) % 6) + 1; //1000001 because it's range is inclusive to exclusive. Do not touch it. Just change the modulo 5 to modulo 6 in this line and line 95 once we add weapon #6
                while (newWeapon == triggerAttacks.currentAttack)
                {
                    newWeapon = (Random.Range(1, 1000001) % 6) + 1; //This ensures you are getting a new weapon every roll
                }

                Sword.SetActive(false);
                Lance.SetActive(false);
                Mace.SetActive(false);
                Bow.SetActive(false);
                PokerChip.SetActive(false);
                SlotMachineGun.SetActive(false);

                triggerAttacks.currentAttack = newWeapon; //We have now changed the weapon

                //Debug.Log(triggerAttacks.currentAttack + "selected");
            }
            //Debug.Log(gooberAnim.GetBool("isDashing"));
        }

        //if health is 0 or less, kill
        if (currentHealth <= 0)
        {

        }
    }

    private void FixedUpdate()
    {
        // Decrease the current speed to match the normal player speed
        if (currentSpeed > playerSpeed)
        {
            currentSpeed -= speedDecrease * Time.fixedDeltaTime * 60;
            dashing = true;
            //gooberAnim.SetBool("isDashing", false);
        }
        // Set the current speed to the normal player speed if it ever goes below
        if (currentSpeed < playerSpeed)
        {
            currentSpeed = playerSpeed;
            dashing = false;
        }

        // Increase the cooldown if the player is at normal speed
        if (currentSpeed <= playerSpeed && cooldownTimer < cooldownLength)
        {

            dashing = false;
            cooldownTimer += 1 * Time.fixedDeltaTime * 60;
            if (triggerAttacks.currentAttack == 1) //Sword
            {
                Sword.SetActive(true);
                Lance.SetActive(false);
                Mace.SetActive(false);
                Bow.SetActive(false);
                PokerChip.SetActive(false);
                SlotMachineGun.SetActive(false);

                //Change Goober's Expression
                exp01.SetActive(true);
                exp02.SetActive(false);
                exp03.SetActive(false);
                exp04.SetActive(false);
                exp05.SetActive(false);
                exp06.SetActive(false);
            }
            if (triggerAttacks.currentAttack == 2) //Lance
            {
                Sword.SetActive(false);
                Lance.SetActive(true);
                Mace.SetActive(false);
                Bow.SetActive(false);
                PokerChip.SetActive(false);
                SlotMachineGun.SetActive(false);

                //Change Goober's Expression
                exp01.SetActive(false);
                exp02.SetActive(true);
                exp03.SetActive(false);
                exp04.SetActive(false);
                exp05.SetActive(false);
                exp06.SetActive(false);
            }
            if (triggerAttacks.currentAttack == 3) //Mace
            {
                Sword.SetActive(false);
                Lance.SetActive(false);
                Mace.SetActive(true);
                Bow.SetActive(false);
                PokerChip.SetActive(false);
                SlotMachineGun.SetActive(false);

                //Change Goober's Expression
                exp01.SetActive(false);
                exp02.SetActive(false);
                exp03.SetActive(true);
                exp04.SetActive(false);
                exp05.SetActive(false);
                exp06.SetActive(false);
            }
            if (triggerAttacks.currentAttack == 4) //Bow
            {
                Sword.SetActive(false);
                Lance.SetActive(false);
                Mace.SetActive(false);
                Bow.SetActive(true);
                PokerChip.SetActive(false);
                SlotMachineGun.SetActive(false);

                //Change Goober's Expression
                exp01.SetActive(false);
                exp02.SetActive(false);
                exp03.SetActive(false);
                exp04.SetActive(true);
                exp05.SetActive(false);
                exp06.SetActive(false);
            }
            if (triggerAttacks.currentAttack == 5) //PokerChip
            {
                Sword.SetActive(false);
                Lance.SetActive(false);
                Mace.SetActive(false);
                Bow.SetActive(false);
                PokerChip.SetActive(true);
                SlotMachineGun.SetActive(false);

                //Change Goober's Expression
                exp01.SetActive(false);
                exp02.SetActive(false);
                exp03.SetActive(false);
                exp04.SetActive(false);
                exp05.SetActive(true);
                exp06.SetActive(false);
            }
            if (triggerAttacks.currentAttack == 6) //PokerChip
            {
                Sword.SetActive(false);
                Lance.SetActive(false);
                Mace.SetActive(false);
                Bow.SetActive(false);
                PokerChip.SetActive(false);
                SlotMachineGun.SetActive(true);

                //Change Goober's Expression
                exp01.SetActive(false);
                exp02.SetActive(false);
                exp03.SetActive(false);
                exp04.SetActive(false);
                exp05.SetActive(false);
                exp06.SetActive(true);
            }

        }

        // Add forces to the player rigidbody
        playerRb.AddForce(transform.right * hInput * currentSpeed);
        playerRb.AddForce(transform.up * vInput * currentSpeed);


        // Store the velcities whenever the player is moving
        // and change one to 0 if the player is moving an axis
        if (playerRb.velocity.x != 0)
        {
            previousXVel = playerRb.velocity.x;
        }
        else if (playerRb.velocity.x == 0 && playerRb.velocity.y != 0)
        {
            previousXVel = 0;
        }
        if (playerRb.velocity.y != 0)
        {
            previousYVel = playerRb.velocity.y;
        }
        else if (playerRb.velocity.y == 0 && playerRb.velocity.x != 0)
        {
            previousYVel = 0;
        }

        // Rotate goober based on velocities
        float angle = Mathf.Atan2(previousYVel, previousXVel) * Mathf.Rad2Deg + 90f; // <-- converts from radians to degrees
        gooberTransform.rotation = Quaternion.Euler(0, 0, angle);

    }

    // Damage player reference script
    public void DamagePlayer(float dmgAmount)
    {
        if (!dashing)
        {
            damageSound.Play();
            currentHealth -= dmgAmount;
            Debug.Log("health reduced to: " + currentHealth);
            if(currentHealth <= 0){
                gooberAnim.SetTrigger("isDead");
                WeaponContainer.SetActive(false);
            }
            else
            {
                gooberAnim.SetTrigger("isHit");
            }

            // Shake the camera
            camScript.ShakeCamera();

        } else
        {
            Debug.Log("resisted attack!");
        }
    }
}
