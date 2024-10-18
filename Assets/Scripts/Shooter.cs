using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab for the bullet object
    public Transform shootPoint;    // Starting point for shooting
    public RectTransform miniGameUI; // Reference to the mini-game UI RectTransform

    private Vector2 miniGamePosition; // Position of the mini-game UI within the canvas
    private Vector2 miniGameSize;     // Size of the mini-game UI within the canvas
    private Rect miniGameBounds;      // Bounds of the mini-game UI in world space
    [SerializeField] public float bulletSpeed = 10f;
    void Start()
    {
        // Calculate the position and size of the mini-game UI within the canvas
        miniGamePosition = miniGameUI.anchoredPosition;
        miniGameSize = miniGameUI.sizeDelta;

        // Convert the canvas coordinates to world space coordinates
        Vector3[] corners = new Vector3[4];
        miniGameUI.GetWorldCorners(corners);
        miniGameBounds = new Rect(corners[0], corners[2] - corners[0]);
    }

    void Update()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("left mouse clicked");

            // Get the mouse position in world space
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // Set the z-coordinate to 0 for 2D space
            Debug.Log("Mouse position: " + mousePosition);

            // Log the mini-game UI bounds
            Debug.Log("Mini-game bounds: " + miniGameBounds);

            // Check if the mouse click is within the boundaries of the mini-game UI
            if (miniGameBounds.Contains(new Vector2(mousePosition.x, mousePosition.y)))
            {
                // Calculate the direction towards the mouse position
                Vector3 shootDirection = mousePosition - shootPoint.position;
                shootDirection.z = 0f; // Ensure the direction is flat in 2D space
                Debug.Log("Shoot direction: " + shootDirection);

                // Create and shoot the bullet in the calculated direction
                ShootBullet(shootDirection.normalized);
                Debug.Log("bullet out");
            }
        }
    }

    void ShootBullet(Vector3 direction)
    {
        // Instantiate the bullet prefab at the shoot point position
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

        // Set the scale of the bullet GameObject to adjust its size
        bullet.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); // Adjust the scale as needed

        // Get the Rigidbody component of the bullet object and apply force in the specified direction
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = direction * bulletSpeed; // Adjust the speed as needed
    }

}
