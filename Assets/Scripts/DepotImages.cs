using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepotImages : MonoBehaviour
{
    [SerializeField] private QuestManager questManager; // R�f�rence au gestionnaire de qu�tes
    [SerializeField] private ScreenCapture screenCapture; // R�f�rence au script de capture d'�cran

    private bool inRange = false; // Indique si le joueur est � proximit� de l'objet

    // Appel� lorsqu'un autre collider entre dans le collider de ce GameObject
    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si l'objet qui entre dans le collider est le joueur
        if (other.CompareTag("Player"))
        {
            inRange = true; // Le joueur est maintenant � proximit�
            Debug.Log("enter"); // Affiche un message dans la console pour le d�bogage
        }
    }

    // Appel� lorsqu'un autre collider quitte le collider de ce GameObject
    private void OnTriggerExit(Collider other)
    {
        // V�rifie si l'objet qui quitte le collider est le joueur
        if (other.CompareTag("Player"))
        {
            inRange = false; // Le joueur n'est plus � proximit�
        }
    }

    // Appel� une fois par frame
    void Update()
    {
        // D�commenter et compl�ter cette section pour g�rer l'interaction lorsque le joueur appuie sur E
        /*
        if (inRange && Input.GetKeyUp(KeyCode.E))
        {
            // V�rifie si la qu�te est compl�t�e
            if (screenCapture.questCompleted)
            {
                // Marque la qu�te comme compl�t�e dans le QuestManager
                questManager.CompleteQuest2();
                Debug.Log("images deposited"); // Affiche un message dans la console pour le d�bogage
            }
            else
            {
                Debug.Log("no images of bullies"); // Affiche un message dans la console pour le d�bogage
            }
        }
        */
    }
}
