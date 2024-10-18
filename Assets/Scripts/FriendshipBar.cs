using UnityEngine;
using UnityEngine.UI;

public class FriendshipSystem : MonoBehaviour
{
    // Reference to the slider that controls the friendship bar fill
    public Slider friendshipSlider;

    // Variables to track current and max friendship points
    public int currentPoints = 0;
    public int maxPoints = 10;

    // Variables to define margins for low, average, and high friendship levels
    public int lowMargin = 3;
    public int highMargin = 9;

    // Variables for the friendship bar gradient
    public Gradient gradient;
    public Image fill;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize friendship bar fill color based on current points
        UpdateFriendshipBar();
    }

    // Update the friendship bar fill color based on current points
    public void UpdateFriendshipBar()
    {
        // Calculate the fill percentage based on current and max points
        float fillPercentage = (float)currentPoints / maxPoints;

        // Update the slider value
        friendshipSlider.value = fillPercentage;

        // Set the fill color based on the gradient and fill percentage
        fill.color = gradient.Evaluate(fillPercentage);
    }

    // Function to add points to the friendship system
    public void AddPoints(int points)
    {
        currentPoints = Mathf.Clamp(currentPoints + points, 0, maxPoints);
        UpdateFriendshipBar();
    }

    // Function to deduct points from the friendship system
    public void DeductPoints(int points)
    {
        currentPoints = Mathf.Clamp(currentPoints - points, 0, maxPoints);
        UpdateFriendshipBar();
    }
}
