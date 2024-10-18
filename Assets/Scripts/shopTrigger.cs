using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopTrigger : MonoBehaviour
{
    [SerializeField] GameObject shopUI; // Référence à l'interface utilisateur du magasin
    public bool inRange = false; // Indique si le joueur est dans la zone de déclenchement

    // Méthode appelée lorsque le joueur entre dans le collider
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true; // Le joueur est dans la zone de déclenchement
            shopUI.SetActive(true); // Active l'interface utilisateur du magasin
            Cursor.lockState = CursorLockMode.None; // Déverrouille le curseur
            Cursor.visible = true; // Rend le curseur visible
        }
    }

    // Méthode appelée lorsque le joueur sort du collider
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false; // Le joueur est sorti de la zone de déclenchement
            shopUI.SetActive(false); // Désactive l'interface utilisateur du magasin
            Cursor.lockState = CursorLockMode.Locked; // Verrouille le curseur
            Cursor.visible = false; // Rend le curseur invisible
        }
    }
}
