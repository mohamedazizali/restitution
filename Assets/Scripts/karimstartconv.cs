using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class karimstartconv : MonoBehaviour
{
    [SerializeField] private NPCConversation myConv; // Conversation � d�marrer si le joueur n'a pas le livre
    [SerializeField] private NPCConversation myConv2; // Conversation � d�marrer si le joueur a le livre
    [SerializeField] public bool Quest; // Indicateur de qu�te (pas utilis� dans le script actuel)

    private bool inRange = false; // Indique si le joueur est dans la zone de d�clenchement

    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si le joueur entre dans la zone de d�clenchement
        if (other.CompareTag("Player"))
        {
            inRange = true;
            Debug.Log("enter"); // Affiche un message dans la console lorsque le joueur entre dans la zone
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // V�rifie si le joueur quitte la zone de d�clenchement
        if (other.CompareTag("Player"))
        {
            inRange = false; // Met � jour l'�tat lorsque le joueur quitte la zone
        }
    }

    private void Update()
    {
        // V�rifie si le joueur est dans la zone de d�clenchement et a appuy� sur la touche "E"
        if (inRange && Input.GetKeyUp(KeyCode.E))
        {
            // D�marre la conversation appropri�e en fonction de l'�tat de l'inventaire
            if (InventoryManager.Instance.HasItem("livre") == false)
            {
                // Si le joueur n'a pas l'objet "livre", d�marre la premi�re conversation
                ConversationManager.Instance.StartConversation(myConv);
            }
            else
            {
                // Si le joueur a l'objet "livre", d�marre la deuxi�me conversation et retire l'objet de l'inventaire
                ConversationManager.Instance.StartConversation(myConv2);
                InventoryManager.Instance.RemoveWithName("livre");
                InventoryManager.Instance.ListItems(); // Met � jour l'affichage des �l�ments de l'inventaire
            }
        }
    }
}
