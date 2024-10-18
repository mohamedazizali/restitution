using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopTrigger : MonoBehaviour
{
    [SerializeField] GameObject shopUI; // R�f�rence � l'interface utilisateur du magasin
    public bool inRange = false; // Indique si le joueur est dans la zone de d�clenchement

    // M�thode appel�e lorsque le joueur entre dans le collider
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true; // Le joueur est dans la zone de d�clenchement
            shopUI.SetActive(true); // Active l'interface utilisateur du magasin
            Cursor.lockState = CursorLockMode.None; // D�verrouille le curseur
            Cursor.visible = true; // Rend le curseur visible
        }
    }

    // M�thode appel�e lorsque le joueur sort du collider
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false; // Le joueur est sorti de la zone de d�clenchement
            shopUI.SetActive(false); // D�sactive l'interface utilisateur du magasin
            Cursor.lockState = CursorLockMode.Locked; // Verrouille le curseur
            Cursor.visible = false; // Rend le curseur invisible
        }
    }
}
