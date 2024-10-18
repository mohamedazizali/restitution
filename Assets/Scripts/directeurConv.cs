using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class directeurConv : MonoBehaviour
{
    [SerializeField] private NPCConversation myConv;   // Référence à la première conversation
    [SerializeField] private NPCConversation myConv2;  // Référence à la deuxième conversation (non utilisée ici)
    [SerializeField] public bool Quest;                // Indicateur pour les quêtes (non utilisé ici)

    private bool inRange = false;  // Indique si le joueur est à proximité du PNJ

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si le joueur entre dans la zone de déclenchement
        if (other.CompareTag("Player"))
        {
            inRange = true;
            Debug.Log("enter");  // Affiche un message dans la console pour le débogage
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Vérifie si le joueur quitte la zone de déclenchement
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    private void Update()
    {
        // Vérifie si le joueur est à proximité et appuie sur la touche E
        if (inRange && Input.GetKeyUp(KeyCode.E))
        {
            // Démarre la conversation avec le PNJ
            ConversationManager.Instance.StartConversation(myConv);
        }
    }
}
