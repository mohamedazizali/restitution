using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AN_PlugScript : MonoBehaviour
{
    [Tooltip("Feature for one using only")]
    public bool OneTime = false; // Indique si l'objet peut être utilisé une seule fois
    [Tooltip("Plug follow this local EmptyObject")]
    public Transform HeroHandsPosition; // Référence à la position des mains du héros pour suivre l'objet
    [Tooltip("SocketObject with collider(shpere, box etc.) (is trigger = true)")]
    public Collider Socket; // Socket avec un collider (doit être un trigger)
    public AN_DoorScript DoorObject; // Référence au script de la porte

    // Variables pour la méthode NearView()
    float distance; // Distance entre l'objet et la caméra
    float angleView; // Angle de vue entre la direction de la caméra et la direction vers l'objet
    Vector3 direction; // Direction de l'objet par rapport à la caméra

    bool follow = false, isConnected = false, followFlag = false, youCan = true; // États de l'objet
    Rigidbody rb; // Référence au Rigidbody de l'objet

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Initialisation du Rigidbody
    }

    void Update()
    {
        if (youCan) Interaction(); // Appelle la méthode Interaction si l'objet peut être utilisé

        // Gel l'objet si connecté au PowerOut
        if (isConnected)
        {
            gameObject.transform.position = Socket.transform.position; // Positionne l'objet à la position du socket
            gameObject.transform.rotation = Socket.transform.rotation; // Aligne la rotation de l'objet avec celle du socket
            DoorObject.isOpened = true; // Ouvre la porte
        }
        else
        {
            DoorObject.isOpened = false; // Ferme la porte
        }
    }

    void Interaction()
    {
        if (NearView() && Input.GetKeyDown(KeyCode.E) && !follow)
        {
            isConnected = false; // Déverrouille l'objet
            follow = true; // L'objet commence à suivre
            followFlag = false;
        }

        if (follow)
        {
            rb.drag = 10f; // Augmente le drag pour ralentir le mouvement
            rb.angularDrag = 10f; // Augmente le drag angulaire pour ralentir la rotation
            if (followFlag)
            {
                distance = Vector3.Distance(transform.position, Camera.main.transform.position); // Calcule la distance à la caméra
                if (distance > 3f || Input.GetKeyDown(KeyCode.E)) // Si la distance est trop grande ou la touche 'E' est pressée
                {
                    follow = false; // Arrête de suivre
                }
            }

            followFlag = true; // Marque que le suivi est activé
            rb.AddExplosionForce(-1000f, HeroHandsPosition.position, 10f); // Applique une force d'explosion pour suivre l'objet
            // Variante de suivi alternatif
            //gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, objectLerp.position, 1f);
        }
        else
        {
            rb.drag = 0f; // Réinitialise le drag
            rb.angularDrag = .5f; // Réinitialise le drag angulaire
        }
    }

    bool NearView() // Retourne vrai si l'objet est proche de l'objet interactif
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position); // Calcule la distance à la caméra
        direction = transform.position - Camera.main.transform.position; // Calcule la direction vers l'objet
        angleView = Vector3.Angle(Camera.main.transform.forward, direction); // Calcule l'angle de vue
        if (distance < 3f && angleView < 35f) return true; // Vérifie si l'objet est à moins de 3 unités et dans un angle de vue de 35°
        else return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == Socket)
        {
            isConnected = true; // L'objet est connecté au socket
            follow = false; // Arrête de suivre
            DoorObject.rbDoor.AddRelativeTorque(new Vector3(0, 0, 20f)); // Ajoute un couple à la porte pour l'ouvrir
        }
        if (OneTime) youCan = false; // Désactive l'interaction si l'objet peut être utilisé une seule fois
    }
}
