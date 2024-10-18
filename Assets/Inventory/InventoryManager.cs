using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    // Liste des objets dans cet inventaire
    public List<Item> Items = new List<Item>();

    // Références aux éléments UI
    public Transform ItemContent; // Conteneur pour les éléments d'inventaire dans l'UI
    public GameObject InventoryItem; // Modèle d'élément d'inventaire à instancier
    private GameObject polaroid; // Référence au holder du polaroid
    private Transform PhotoFrameBG; // Référence au cadre de la photo
    private Transform PhotoFrameBGBigChild; // Référence à la zone d'affichage de la photo

    private void Start()
    {
        gameObject.SetActive(false); // Désactive l'inventaire au démarrage
        polaroid = GameObject.FindGameObjectWithTag("polaroidHolder"); // Trouve l'objet polaroidHolder
        PhotoFrameBG = polaroid.transform.Find("PhotoFrameBG"); // Trouve le cadre de la photo
        PhotoFrameBGBigChild = polaroid.transform.Find("PhotoFrameBG/PhotoHolderMask/PhotoDisplayArea"); // Trouve la zone d'affichage de la photo
    }

    // Awake est appelé lorsque l'instance du script est chargée
    void Awake()
    {
        // Assure que cette instance est définie comme l'Instance uniquement si ce n'est pas déjà fait
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Ajouter un objet à cet inventaire
    public void Add(Item item)
    {
        Items.Add(item);
    }

    // Retirer un objet de cet inventaire
    public void Remove(Item item)
    {
        Items.Remove(item);
    }

    // Lister tous les objets dans cet inventaire
    public void ListItems()
    {
        // Détruire les éléments UI existants
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        // Créer des éléments UI pour chaque objet
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            // Définir le nom et l'icône de l'objet
            itemIcon.sprite = item.icon;
            itemName.text = item.itemName;
            obj.GetComponent<DropItem>().polaroidholder = PhotoFrameBG.gameObject;
            obj.GetComponent<DropItem>().imageholder = PhotoFrameBGBigChild.gameObject.GetComponent<Image>();
            // Définir l'objet pour le composant DropItem
            obj.GetComponent<DropItem>().item = item;
        }
    }

    // Vérifier si cet inventaire contient un objet avec un nom donné
    public bool HasItem(string itemName)
    {
        foreach (var item in Items)
        {
            if (item.itemName == itemName)
            {
                return true;
            }
        }
        return false;
    }

    // Retirer un objet de cet inventaire par son nom
    public void RemoveWithName(string itemName)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].itemName == itemName)
            {
                Items.RemoveAt(i);
                return;
            }
        }
    }

    // Calculer la valeur totale des objets dans cet inventaire
    public float CalculateTotalValue()
    {
        float totalValue = 0f;
        foreach (var item in Items)
        {
            totalValue += item.value;
        }
        return totalValue;
    }

    int i = 1;
    // Ajouter un objet de capture d'écran à cet inventaire
    public void AddScreenshotItem(Sprite screenshotSprite, string prefabTag, float val)
    {
        Item screenshotItem = ScriptableObject.CreateInstance<Item>();
        screenshotItem.itemName = "Screenshot_" + i.ToString();
        i++;
        screenshotItem.icon = screenshotSprite;
        Add(screenshotItem);
        screenshotItem.prefabTag = prefabTag;
        screenshotItem.value = val;
    }

    // Vérifier le nombre d'objets avec le tag "Planet"
    public int checkplanets()
    {
        int count = 0;
        foreach (var item in Items)
        {
            if (item.prefabTag == "Planet")
            {
                count++;
            }
        }
        return count;
    }

    // Vérifier le nombre de preuves collectées (avec les tags "Picture" ou "Paper")
    public int checkcollectedProves()
    {
        int count = 0;
        foreach (var item in Items)
        {
            if (item.prefabTag == "Picture" || item.prefabTag == "Paper")
            {
                count++;
            }
        }
        return count;
    }
    // Vérifie si l'inventaire contient au moins trois objets avec le tag "Picture"
    public bool HasAtLeastThreePictures()
    {
        int count = 0; // Initialisation du compteur

        // Boucle à travers chaque élément dans l'inventaire
        foreach (var item in Items)
        {
            // Vérifie si l'élément a le tag "Picture"
            if (item.prefabTag == "Picture")
            {
                count++; // Incrémente le compteur

                // Si au moins trois éléments sont trouvés, retourne true
                if (count >= 3)
                {
                    return true;
                }
            }
        }

        // Si moins de trois éléments sont trouvés, retourne false
        return false;
    }
    // Retirer les objets de cet inventaire avec un tag spécifié
    public void RemoveItemsWithTag(string tag)
    {
        // Créer une liste pour stocker les objets à retirer
        List<Item> itemsToRemove = new List<Item>();

        // Trouver les objets avec le tag spécifié et les ajouter à la liste
        foreach (var item in Items)
        {
            if (item.prefabTag == tag)
            {
                itemsToRemove.Add(item);
            }
        }

        // Retirer les objets de l'inventaire
        foreach (var itemToRemove in itemsToRemove)
        {
            Items.Remove(itemToRemove);
        }
    }

    // Référence à l'instance active de InventoryManager
    public static InventoryManager Instance;
}
