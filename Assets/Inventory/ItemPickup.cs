using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item; // Référence à l'objet Item à ramasser

    // Fonction appelée pour ramasser l'objet
    void Pickup()
    {
        InventoryManager.Instance.Add(item); // Ajouter l'objet à l'inventaire
        Destroy(gameObject); // Détruire l'objet dans le monde du jeu
    }
}
