using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordButton : MonoBehaviour
{
    [SerializeField] public List<string> goodWordPool; // Pool of good words to choose from
    [SerializeField] public List<string> badWordPool;  // Pool of bad words to choose from

    private TextMeshProUGUI buttonText;
    private string currentWord;
    private bool isGoodWord;

    void Awake()
    {
        // Get the TextMeshProUGUI component on the button
        buttonText = GetComponentInChildren<TextMeshProUGUI>();

        if (buttonText == null)
        {
            Debug.LogError("Button does not have a TextMeshProUGUI component.");
            return;
        }

        // Set the button text to a random word from either good or bad word pool
        SetButtonText();

        // Schedule the button to be destroyed after 15 seconds
        Destroy(gameObject, 15f);
    }

    void SetButtonText()
    {
        // Randomly decide if the word should be good or bad
        if (Random.Range(0, 2) == 0 && goodWordPool.Count > 0)
        {
            isGoodWord = true;
            currentWord = goodWordPool[Random.Range(0, goodWordPool.Count)];
        }
        else if (badWordPool.Count > 0)
        {
            isGoodWord = false;
            currentWord = badWordPool[Random.Range(0, badWordPool.Count)];
        }
        else
        {
            Debug.LogError("Both word pools are empty.");
            return;
        }

        buttonText.text = currentWord;
    }

    public void OnButtonClick()
    {
        Debug.Log("Button clicked: " + currentWord);

        // Update score based on whether the word was good or bad
        if (isGoodWord)
        {
            // Decrease score for destroying good words
            GameManager.Instance.UpdatePlayerScore(-1);
        }
        else
        {
            // Increase score for destroying bad words
            GameManager.Instance.UpdatePlayerScore(1);
        }

        Destroy(gameObject);
    }
}
