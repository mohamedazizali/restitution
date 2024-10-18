using TMPro;
using UnityEngine;

public class scoreUIManager : MonoBehaviour
{
    public static scoreUIManager Instance;

    public TextMeshProUGUI scoreText;

    void Awake()
    {
        // Ensure there is only one instance of ScoreUIManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            // If an instance already exists, destroy this one
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Subscribe to the event for score change
        GameManager.OnPlayerScoreChanged += UpdateScoreUI;
    }

    public void UpdateScoreUI(int newScore)
    {
        // Update the displayed score
        scoreText.text = "" + newScore;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks
        GameManager.OnPlayerScoreChanged -= UpdateScoreUI;
    }
}
