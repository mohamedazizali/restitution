using UnityEngine;

public class Smoke : MonoBehaviour
{
    public float extinguishAmount = 5f; // Amount to extinguish per particle collision

    void OnParticleCollision(GameObject other)
    {
        Fire fire = other.GetComponent<Fire>();
        if (fire != null)
        {
            fire.ExtinguishProgressively(extinguishAmount);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fire"))
        {

            Fire fire = other.GetComponent<Fire>();
            if (fire != null)
            {
                fire.ExtinguishProgressively(extinguishAmount);
            }
        }
    }
}
