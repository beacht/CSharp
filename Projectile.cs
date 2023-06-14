using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // What the camera will follow
    public Transform target;
    public Rigidbody2D targetRb;

    public float smoothAmount;
    public float lookAheadAmount;
    public Vector3 camOffset;

    // Start is called before the first frame update
    void Start()
    {
        // Pick the correct location for the camera when it loads
        transform.position = target.position + camOffset;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        // Vectors to hold camera positions
        Vector3 goalDestination;
        Vector3 currentPos;

        // Pick a goal destination (target + an arbitrary offset)
        goalDestination = target.position + camOffset + new Vector3(targetRb.velocity.x, targetRb.velocity.y, 0) * lookAheadAmount;

        // Current position is an interpolation between the current position and the goal destination 
        currentPos = Vector3.Lerp(transform.position, goalDestination, smoothAmount);

        // Assign the position to the camera's transform
        transform.position = currentPos;

    }
}
