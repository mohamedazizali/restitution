using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepotImages : MonoBehaviour
{
    [SerializeField] private QuestManager questManager; // Référence au gestionnaire de quêtes
    [SerializeField] private ScreenCapture screenCapture; // Référence au script de capture d'écran

    private bool inRange = false; // Indique si le joueur est à proximité de l'objet

    // Appelé lorsqu'un autre collider entre dans le collider de ce GameObject
    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet qui entre dans le collider est le joueur
        if (other.CompareTag("Player"))
        {
            inRange = true; // Le joueur est maintenant à proximité
            Debug.Log("enter"); // Affiche un message dans la console pour le débogage
        }
    }

    // Appelé lorsqu'un autre collider quitte le collider de ce GameObject
    private void OnTriggerExit(Collider other)
    {
        // Vérifie si l'objet qui quitte le collider est le joueur
        if (other.CompareTag("Player"))
        {
            inRange = false; // Le joueur n'est plus à proximité
        }
    }

    // Appelé une fois par frame
    void Update()
    {
        // Décommenter et compléter cette section pour gérer l'interaction lorsque le joueur appuie sur E
        /*
        if (inRange && Input.GetKeyUp(KeyCode.E))
        {
            // Vérifie si la quête est complétée
            if (screenCapture.questCompleted)
            {
                // Marque la quête comme complétée dans le QuestManager
                questManager.CompleteQuest2();
                Debug.Log("images deposited"); // Affiche un message dans la console pour le débogage
            }
            else
            {
                Debug.Log("no images of bullies"); // Affiche un message dans la console pour le débogage
            }
        }
        */
    }
}
