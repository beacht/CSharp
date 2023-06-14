using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAttacks : MonoBehaviour
{

    // Current Attack
    public int currentAttack = 1;

    // Prefabs
    public GameObject pokerPrefab;
    public GameObject arrowPrefab;
    public GameObject slotMachinePrefab;

    // Weapon Rigidbody
    public Rigidbody2D weaponRb;

    // Slot Machine Timer Variables
    public float slotMachineFireRate = 20;
    private float slotMachineTimer = 0;

    // Arrow Fire Rate
    public float arrowFireRate = 20;
    private float arrowTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Attack mapped to left click now
        if (Input.GetMouseButtonDown(0))
        {

            if(currentAttack == 5)
            { // Poker chip
                GameObject particle = Instantiate(pokerPrefab, transform.position, Quaternion.Euler(0, 0, weaponRb.rotation));
                particle.GetComponent<playerParticle>().weaponRb = weaponRb; // Set references
            }
        }

        // If mouse button is held...
        if (Input.GetMouseButton(0))
        {

            // Pick correct particle based on current weapon equipped
            if (currentAttack == 4 && arrowTimer <= 0)
            { // Arrow
                GameObject particle = Instantiate(arrowPrefab, transform.position, Quaternion.Euler(0, 0, weaponRb.rotation));
                particle.GetComponent<playerParticle>().weaponRb = weaponRb; // Set references

                arrowTimer = arrowFireRate;
            }

            if (currentAttack == 6 && slotMachineTimer <= 0)
            { // Slot machine gun
                GameObject particle = Instantiate(slotMachinePrefab, transform.position, Quaternion.Euler(0, 0, weaponRb.rotation));
                particle.GetComponent<playerParticle>().weaponRb = weaponRb; // Set references

                slotMachineTimer = slotMachineFireRate;
            }

        }

        // Decrease arrow timer
        arrowTimer -= 1 * Time.deltaTime * 60;

        // Decrease timer based on time elapsed since previous frame
        slotMachineTimer -= 1 * Time.deltaTime * 60;

    }
}
