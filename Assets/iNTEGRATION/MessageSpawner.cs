using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MessageSpawner : MonoBehaviour
{
    public GameObject messagePrefab;
    public Transform spawnPoint;
    public GameObject messagePanel;
    public GameObject otherPanel;
    public GameObject reportPanel;
    public CoinUIManager coinUIManager; // Reference to CoinUIManager script
    public float minSpawnTime = 5f;
    public float maxSpawnTime = 10f;
    public float notificationDuration = 3f;
    public GameObject notificationPanel; // Reference to the notification panel
    void Start()
    {
        StartCoroutine(SpawnMessages());
    }

    IEnumerator SpawnMessages()
    {
        while (true)
        {
            // Calculate random spawn time
            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);

            // Wait for the calculated spawn time
            yield return new WaitForSeconds(spawnTime);

            // Instantiate the message prefab at the spawn point
            GameObject messageInstance = Instantiate(messagePrefab, spawnPoint);

            // Get the button component from the instantiated message prefab
            Button button = messageInstance.GetComponent<Button>();

            // Add an onClick event to the button
            button.onClick.AddListener(() =>
            {
                // Activate other panel
                otherPanel.SetActive(false);
                // Deactivate message panel
                messagePanel.SetActive(true);
            });
            StartCoroutine(ShowNotificationPanel());
            // Get the button component from the report panel
            Button reportButton = reportPanel.GetComponent<Button>();

            // Add an onClick event to the report button
            reportButton.onClick.AddListener(() =>
            {
                // Check if the message is bad or good
                bool isBadMessage = CheckIfBadMessage();

                // Perform actions based on whether the message is bad or good
                if (isBadMessage)
                {
                    // Handle bad message
                    coinUIManager.UpdateCoinCountUI(100); // Add 10 coins for reporting a bad message
                }
                else
                {
                    // Handle good message
                    coinUIManager.SubtractCoins(5); // Subtract 5 coins for reporting a good message
                }

                Destroy(messageInstance);
            });
        }
    }
    IEnumerator ShowNotificationPanel()
    {
        // Activate the notification panel
        notificationPanel.SetActive(true);

        // Wait for the specified duration
        yield return new WaitForSeconds(notificationDuration);

        // Deactivate the notification panel after the duration
        notificationPanel.SetActive(false);
    }
    bool CheckIfBadMessage()
    {
        // Randomly determine if the message is bad or good
        return Random.value < 0.5f; // 50% chance of being a bad message
    }
}
