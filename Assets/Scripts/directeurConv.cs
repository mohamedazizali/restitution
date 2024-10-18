using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class directeurConv : MonoBehaviour
{
    [SerializeField] private NPCConversation myConv;   // R�f�rence � la premi�re conversation
    [SerializeField] private NPCConversation myConv2;  // R�f�rence � la deuxi�me conversation (non utilis�e ici)
    [SerializeField] public bool Quest;                // Indicateur pour les qu�tes (non utilis� ici)

    private bool inRange = false;  // Indique si le joueur est � proximit� du PNJ

    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si le joueur entre dans la zone de d�clenchement
        if (other.CompareTag("Player"))
        {
            inRange = true;
            Debug.Log("enter");  // Affiche un message dans la console pour le d�bogage
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // V�rifie si le joueur quitte la zone de d�clenchement
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    private void Update()
    {
        // V�rifie si le joueur est � proximit� et appuie sur la touche E
        if (inRange && Input.GetKeyUp(KeyCode.E))
        {
            // D�marre la conversation avec le PNJ
            ConversationManager.Instance.StartConversation(myConv);
        }
    }
}
