using UnityEngine;

public class SlidingPlateform : MonoBehaviour
{
    public float distance = 5.0f; // Distance the platform will slide from the starting position
    public float speed = 2.0f; // Speed of the platform's movement

    private Vector3 startPosition;
    private bool movingRight = true;

    void Start()
    {
        // Store the initial position of the platform
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the movement
        float movement = speed * Time.deltaTime;

        if (movingRight)
        {
            // Move the platform to the right
            transform.position += new Vector3(movement, 0, 0);

            // Check if the platform has reached the maximum distance to the right
            if (Vector3.Distance(startPosition, transform.position) >= distance)
            {
                movingRight = false;
            }
        }
        else
        {
            // Move the platform to the left
            transform.position -= new Vector3(movement, 0, 0);

            // Check if the platform has returned to the starting position or moved too far to the left
            if (Vector3.Distance(startPosition, transform.position) >= distance)
            {
                movingRight = true;
            }
        }
    }

    void OnDrawGizmos()
    {
        // Draw lines to visualize the movement path in the editor
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(startPosition, startPosition + new Vector3(distance, 0, 0));
            Gizmos.DrawLine(startPosition, startPosition - new Vector3(distance, 0, 0));
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(distance, 0, 0));
            Gizmos.DrawLine(transform.position, transform.position - new Vector3(distance, 0, 0));
        }
    }
}
