using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFollowMouse : MonoBehaviour
{
    // Intializing references
    public Camera cam;
    public Vector2 mousePos;
    public Rigidbody2D playerRb;
    private Rigidbody2D weaponRb;

    // Start is called before the first frame update
    void Start()
    {

        // Get the weapon's rigidbody (just so I can rotate it easier)
        weaponRb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        // Find the mouse's position in the game scene
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        // Subtract the two vectors to get the direction's vector
        Vector2 lookDir = mousePos - playerRb.position;

        // Find the angle from the direction vector
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg; // <-- converts from radians to degrees

        // Actually change the rotation and position
        weaponRb.rotation = angle;
        weaponRb.position = playerRb.position;


    }
}
