using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    // Liste des objets dans cet inventaire
    public List<Item> Items = new List<Item>();

    // R�f�rences aux �l�ments UI
    public Transform ItemContent; // Conteneur pour les �l�ments d'inventaire dans l'UI
    public GameObject InventoryItem; // Mod�le d'�l�ment d'inventaire � instancier
    private GameObject polaroid; // R�f�rence au holder du polaroid
    private Transform PhotoFrameBG; // R�f�rence au cadre de la photo
    private Transform PhotoFrameBGBigChild; // R�f�rence � la zone d'affichage de la photo

    private void Start()
    {
        gameObject.SetActive(false); // D�sactive l'inventaire au d�marrage
        polaroid = GameObject.FindGameObjectWithTag("polaroidHolder"); // Trouve l'objet polaroidHolder
        PhotoFrameBG = polaroid.transform.Find("PhotoFrameBG"); // Trouve le cadre de la photo
        PhotoFrameBGBigChild = polaroid.transform.Find("PhotoFrameBG/PhotoHolderMask/PhotoDisplayArea"); // Trouve la zone d'affichage de la photo
    }

    // Awake est appel� lorsque l'instance du script est charg�e
    void Awake()
    {
        // Assure que cette instance est d�finie comme l'Instance uniquement si ce n'est pas d�j� fait
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Ajouter un objet � cet inventaire
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
        // D�truire les �l�ments UI existants
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        // Cr�er des �l�ments UI pour chaque objet
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            // D�finir le nom et l'ic�ne de l'objet
            itemIcon.sprite = item.icon;
            itemName.text = item.itemName;
            obj.GetComponent<DropItem>().polaroidholder = PhotoFrameBG.gameObject;
            obj.GetComponent<DropItem>().imageholder = PhotoFrameBGBigChild.gameObject.GetComponent<Image>();
            // D�finir l'objet pour le composant DropItem
            obj.GetComponent<DropItem>().item = item;
        }
    }

    // V�rifier si cet inventaire contient un objet avec un nom donn�
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
    // Ajouter un objet de capture d'�cran � cet inventaire
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

    // V�rifier le nombre d'objets avec le tag "Planet"
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

    // V�rifier le nombre de preuves collect�es (avec les tags "Picture" ou "Paper")
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
    // V�rifie si l'inventaire contient au moins trois objets avec le tag "Picture"
    public bool HasAtLeastThreePictures()
    {
        int count = 0; // Initialisation du compteur

        // Boucle � travers chaque �l�ment dans l'inventaire
        foreach (var item in Items)
        {
            // V�rifie si l'�l�ment a le tag "Picture"
            if (item.prefabTag == "Picture")
            {
                count++; // Incr�mente le compteur

                // Si au moins trois �l�ments sont trouv�s, retourne true
                if (count >= 3)
                {
                    return true;
                }
            }
        }

        // Si moins de trois �l�ments sont trouv�s, retourne false
        return false;
    }
    // Retirer les objets de cet inventaire avec un tag sp�cifi�
    public void RemoveItemsWithTag(string tag)
    {
        // Cr�er une liste pour stocker les objets � retirer
        List<Item> itemsToRemove = new List<Item>();

        // Trouver les objets avec le tag sp�cifi� et les ajouter � la liste
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

    // R�f�rence � l'instance active de InventoryManager
    public static InventoryManager Instance;
}
