using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    // What the camera will follow
    public Transform target;
    public Rigidbody2D targetRb;

    public float smoothAmount;
    public float lookAheadAmount;
    public Vector3 camOffset;
    public float rumbleTimer = 0;
    public float rumbleLength = 80;
    public float rumbleStrength = 0.5f;

    private Vector3 currentPos;


    // Start is called before the first frame update
    void Start()
    {
        // Pick the correct location for the camera when it loads
        transform.position = target.position + camOffset;
        currentPos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        // Vectors to hold camera positions
        Vector3 goalDestination;

        // Pick a goal destination (target + an arbitrary offset + look ahead amount) * deltaTime
        goalDestination = target.position + camOffset + new Vector3(targetRb.velocity.x, targetRb.velocity.y, 0) * lookAheadAmount;

        // Current position is an interpolation between the current position and the goal destination 
        currentPos = Vector3.Lerp(currentPos, goalDestination, smoothAmount * Time.deltaTime*60);

        // If the camera's rumbling...
        if (rumbleTimer > 0)
        {

            // Decrease the rumble timer
            rumbleTimer--;

            // Calculate the offset
            float currentStrength = (rumbleTimer / rumbleLength) * rumbleStrength;
            Vector3 rumbleOffset = new Vector3(Random.Range(-rumbleStrength * currentStrength, rumbleStrength * currentStrength), Random.Range(-rumbleStrength * currentStrength, rumbleStrength * currentStrength), 0);

            // Add it to the camera position
            transform.position = currentPos + rumbleOffset;

        // Otherwise...
        } else
        {
            // Assign the position to the camera's transform
            transform.position = currentPos;
        }


    }

    public void ShakeCamera()
    {
        rumbleTimer = rumbleLength;
    }
}
