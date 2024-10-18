using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int coinCount = 0;
    public int playerScore = 0; // Add player score variable

    // Define delegate and event for coin count change
    public delegate void CoinCountChanged(int newCoinCount);
    public static event CoinCountChanged OnCoinCountChanged;

    // Define delegate and event for player score change
    public delegate void PlayerScoreChanged(int newPlayerScore);
    public static event PlayerScoreChanged OnPlayerScoreChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Function to update the coin count
    public void UpdateCoinCount(int amount)
    {
        coinCount += amount;

        // Trigger the event when coin count changes
        if (OnCoinCountChanged != null)
        {
            OnCoinCountChanged(coinCount);
        }
    }

    // Function to update the player score
    public void UpdatePlayerScore(int amount)
    {
        playerScore += amount;

        // Trigger the event when player score changes
        if (OnPlayerScoreChanged != null)
        {
            OnPlayerScoreChanged(playerScore);
        }
    }
}
