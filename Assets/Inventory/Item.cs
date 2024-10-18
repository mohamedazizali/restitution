using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public int id; // Identifiant unique de l'objet
    public string itemName; // Nom de l'objet
    public float value; // Valeur de l'objet
    public Sprite icon; // Ic�ne repr�sentant l'objet
    public GameObject prefab; // Pr�fabriqu� repr�sentant l'objet dans le jeu
    public string prefabTag; // Tag du pr�fabriqu�
    public int price; // Prix de l'objet

    // Constructeur avec param�tres
    public Item(int id, string itemName, int value, Sprite icon, int price, GameObject prefab)
    {
        this.id = id;
        this.itemName = itemName;
        this.value = value;
        this.icon = icon;
        this.prefab = prefab;
        this.prefabTag = prefabTag; // Assignation du tag du pr�fabriqu�
        this.price = price;
    }

    // Constructeur sans param�tres (constructeur par d�faut)
    public Item()
    {
        // Valeurs par d�faut
        id = 0;
        itemName = "New Item";
        value = 0;
        icon = null;
        prefab = null;
        prefabTag = ""; // Initialisation du tag du pr�fabriqu�
        price = 0;
    }
}
