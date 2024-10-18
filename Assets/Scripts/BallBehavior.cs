using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public float bounceForce = 5f; // Force applied when bouncing off the player

    private Rigidbody rb;

    private void Start()
    {
        // Get the Rigidbody component attached to the ball
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Calculate the direction away from the player
            Vector3 bounceDirection = collision.contacts[0].normal;

            // Apply a force to the ball in the bounce direction
            rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
        }
    }
}
