using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    public float sprayRange = 5f; // Range of the extinguisher spray
    public GameObject sprayEffect; // The particle system for the extinguisher spray

    private bool isPickedUp = false;
    public Transform nozzle; // The position where the spray effect will be instantiated
    public float sprayForce = 10f; // The force applied to the spray projectile
    // Range of the extinguisher spray
    public GameObject player;
    public GameObject playerHand;
    void Update()
    {
        if (isPickedUp)
        {
            if (Input.GetMouseButton(0)) // Left mouse button
            {
                Spray();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(transform.position, playerHand.transform.position) < 2f)
            {
                PickUp();
            }
        }
    }

    void Spray()
    {
        // Instantiate the spray effect prefab at the nozzle position and rotation
        GameObject sprayInstance = Instantiate(sprayEffect, nozzle.position, nozzle.rotation);

        // Apply a forward force to the spray instance to simulate spraying
        Rigidbody rb = sprayInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(nozzle.forward * sprayForce, ForceMode.Impulse);
        }

        // Destroy the spray instance after a certain time to prevent cluttering
        Destroy(sprayInstance, 2f); // Adjust the lifetime as needed
    }
    void PickUp()
    {
        isPickedUp = true;
        transform.SetParent(playerHand.transform);
        transform.localPosition = Vector3.zero; // Adjust position relative to the hand
        transform.localRotation = Quaternion.identity;
    }

    public void Drop()
    {
        isPickedUp = false;
        transform.SetParent(null);
    }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }
}
