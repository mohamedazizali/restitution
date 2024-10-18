using UnityEngine;

public class Fire : MonoBehaviour
{
    public float fireIntensity = 100f; // Maximum fire intensity
    public float extinguishAmount = 5f; // Amount to extinguish per particle collision
    private float currentIntensity;
    private bool isBurning = true;

    private ParticleSystem[] childParticleSystems;

    void Start()
    {
        currentIntensity = fireIntensity;
        childParticleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (isBurning && currentIntensity <= 0)
        {
            ExtinguishCompletely();
        }
    }

    public void ExtinguishProgressively(float amount)
    {
        currentIntensity -= amount * Time.deltaTime;
        if (currentIntensity < 0) currentIntensity = 0;

        foreach (var ps in childParticleSystems)
        {
            Debug.Log("kaada tih");
            var emission = ps.emission;
            emission.rateOverTime = Mathf.Lerp(0, emission.rateOverTime.constantMax, currentIntensity / fireIntensity);

            var main = ps.main;
            main.startLifetime = Mathf.Lerp(0, main.startLifetime.constantMax, currentIntensity / fireIntensity);
        }

        if (currentIntensity <= 0)
        {
            ExtinguishCompletely();
        }
    }

    void ExtinguishCompletely()
    {
        isBurning = false;
        foreach (var ps in childParticleSystems)
        {
            ps.Stop();
        }
        // Additional logic for completely extinguished fire (e.g., disable object, play extinguished sound, etc.)
        gameObject.SetActive(false); // Optionally, deactivate the fire object
    }
}
