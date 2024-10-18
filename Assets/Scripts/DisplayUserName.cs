using UnityEngine;
using TMPro;

public class DisplayUserName : MonoBehaviour
{
    // Référence au composant TextMeshProUGUI pour afficher le nom de l'utilisateur
    public TextMeshProUGUI userNameText;

    private void Start()
    {
        // Vérifie si PersistentDataManager est disponible
        if (PersistentDataManager.Instance != null)
        {
            // Met à jour le texte avec le nom de l'utilisateur
            userNameText.text = "Bienvenue, " + PersistentDataManager.Instance.UserName;
        }
        else
        {
            // Affiche un message d'erreur si PersistentDataManager est absent
            Debug.LogWarning("PersistentDataManager n'est pas disponible.");
        }
    }
}
