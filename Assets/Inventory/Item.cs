using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public int id; // Identifiant unique de l'objet
    public string itemName; // Nom de l'objet
    public float value; // Valeur de l'objet
    public Sprite icon; // Icône représentant l'objet
    public GameObject prefab; // Préfabriqué représentant l'objet dans le jeu
    public string prefabTag; // Tag du préfabriqué
    public int price; // Prix de l'objet

    // Constructeur avec paramètres
    public Item(int id, string itemName, int value, Sprite icon, int price, GameObject prefab)
    {
        this.id = id;
        this.itemName = itemName;
        this.value = value;
        this.icon = icon;
        this.prefab = prefab;
        this.prefabTag = prefabTag; // Assignation du tag du préfabriqué
        this.price = price;
    }

    // Constructeur sans paramètres (constructeur par défaut)
    public Item()
    {
        // Valeurs par défaut
        id = 0;
        itemName = "New Item";
        value = 0;
        icon = null;
        prefab = null;
        prefabTag = ""; // Initialisation du tag du préfabriqué
        price = 0;
    }
}
