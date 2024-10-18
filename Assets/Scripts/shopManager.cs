using System.Collections.Generic;
using UnityEngine;

public class shopManager : MonoBehaviour
{
    // Référence statique à l'instance de shopManager
    public static shopManager Instance;

    // Liste des articles disponibles dans ce magasin
    public List<Item> shopItems = new List<Item>();

    // Références aux éléments de l'interface utilisateur
    public Transform shopItemContent;
    public GameObject shopItemPrefab;

    void Awake()
    {
        // Assure qu'il n'existe qu'une seule instance de shopManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PopulateShop(); // Remplit le magasin avec des articles au démarrage
    }

    // Remplit le magasin avec des articles
    void PopulateShop()
    {
        foreach (var item in shopItems)
        {
            GameObject shopItemObject = Instantiate(shopItemPrefab, shopItemContent);
            BuyButton buyButton = shopItemObject.GetComponent<BuyButton>();

            if (buyButton != null)
            {
                buyButton.item = item; // Assigne l'article au bouton d'achat
            }
            else
            {
                Debug.LogError("Composant BuyButton non trouvé sur le prefab d'article du magasin.");
            }

            ShopItemUI shopItemUI = shopItemObject.GetComponent<ShopItemUI>();
            shopItemUI.SetItem(item); // Configure l'UI de l'article du magasin
        }
    }

    // Ajoute un article au magasin
    public void AddItem(Item item)
    {
        shopItems.Add(item);
    }

    // Supprime un article du magasin
    public void RemoveItem(Item item)
    {
        shopItems.Remove(item);
    }
}
