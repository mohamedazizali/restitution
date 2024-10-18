using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public TextMeshProUGUI itemNameText; // Texte pour le nom de l'article
    public TextMeshProUGUI priceText; // Texte pour le prix de l'article
    public Image iconImage; // Image pour l'ic�ne de l'article

    // Configure l'affichage de l'article dans l'interface utilisateur
    public void SetItem(Item item)
    {
        itemNameText.text = item.itemName; // D�finit le nom de l'article
        priceText.text = item.price.ToString(); // D�finit le prix de l'article
        iconImage.sprite = item.icon; // D�finit l'ic�ne de l'article
    }
}
