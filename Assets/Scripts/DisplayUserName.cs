using UnityEngine;
using TMPro;

public class DisplayUserName : MonoBehaviour
{
    // R�f�rence au composant TextMeshProUGUI pour afficher le nom de l'utilisateur
    public TextMeshProUGUI userNameText;

    private void Start()
    {
        // V�rifie si PersistentDataManager est disponible
        if (PersistentDataManager.Instance != null)
        {
            // Met � jour le texte avec le nom de l'utilisateur
            userNameText.text = "Bienvenue, " + PersistentDataManager.Instance.UserName;
        }
        else
        {
            // Affiche un message d'erreur si PersistentDataManager est absent
            Debug.LogWarning("PersistentDataManager n'est pas disponible.");
        }
    }
}
