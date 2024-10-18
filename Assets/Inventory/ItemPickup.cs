using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item; // R�f�rence � l'objet Item � ramasser

    // Fonction appel�e pour ramasser l'objet
    void Pickup()
    {
        InventoryManager.Instance.Add(item); // Ajouter l'objet � l'inventaire
        Destroy(gameObject); // D�truire l'objet dans le monde du jeu
    }
}
