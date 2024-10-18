using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class karimstartconv : MonoBehaviour
{
    [SerializeField] private NPCConversation myConv; // Conversation à démarrer si le joueur n'a pas le livre
    [SerializeField] private NPCConversation myConv2; // Conversation à démarrer si le joueur a le livre
    [SerializeField] public bool Quest; // Indicateur de quête (pas utilisé dans le script actuel)

    private bool inRange = false; // Indique si le joueur est dans la zone de déclenchement

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si le joueur entre dans la zone de déclenchement
        if (other.CompareTag("Player"))
        {
            inRange = true;
            Debug.Log("enter"); // Affiche un message dans la console lorsque le joueur entre dans la zone
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Vérifie si le joueur quitte la zone de déclenchement
        if (other.CompareTag("Player"))
        {
            inRange = false; // Met à jour l'état lorsque le joueur quitte la zone
        }
    }

    private void Update()
    {
        // Vérifie si le joueur est dans la zone de déclenchement et a appuyé sur la touche "E"
        if (inRange && Input.GetKeyUp(KeyCode.E))
        {
            // Démarre la conversation appropriée en fonction de l'état de l'inventaire
            if (InventoryManager.Instance.HasItem("livre") == false)
            {
                // Si le joueur n'a pas l'objet "livre", démarre la première conversation
                ConversationManager.Instance.StartConversation(myConv);
            }
            else
            {
                // Si le joueur a l'objet "livre", démarre la deuxième conversation et retire l'objet de l'inventaire
                ConversationManager.Instance.StartConversation(myConv2);
                InventoryManager.Instance.RemoveWithName("livre");
                InventoryManager.Instance.ListItems(); // Met à jour l'affichage des éléments de l'inventaire
            }
        }
    }
}
