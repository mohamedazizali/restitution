using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    /// <summary>
    /// Instance unique de CharacterManager pour accéder à ses fonctionnalités de manière globale.
    /// </summary>
    public static CharacterManager Instance { get; private set; }

    /// <summary>
    /// Préfabriqué du personnage masculin.
    /// </summary>
    public GameObject MaleCharacterPrefab;

    /// <summary>
    /// Préfabriqué du personnage féminin.
    /// </summary>
    public GameObject FemaleCharacterPrefab;

    private GameObject currentCharacter;
    /// <summary>
    /// Obtient le personnage actuellement sélectionné.
    /// </summary>
    public GameObject CurrentCharacter => currentCharacter;

    // Indicateur booléen pour déterminer le genre du personnage sélectionné
    private bool isMale;
    /// <summary>
    /// Obtient un indicateur booléen indiquant si le personnage sélectionné est masculin.
    /// </summary>
    public bool IsMale => isMale;

    private void Awake()
    {
        // Assure que l'instance est unique et ne détruit pas l'objet lors du chargement de nouvelles scènes.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    /// <summary>
    /// Sélectionne un préfabriqué de personnage et définit l'indicateur de genre.
    /// </summary>
    /// <param name="characterPrefab">Le préfabriqué du personnage à sélectionner.</param>
    public void SelectCharacter(GameObject characterPrefab)
    {
        // Détruit le personnage actuel s'il existe
        if (currentCharacter != null)
        {
            Destroy(currentCharacter);
        }

        // Crée une nouvelle instance du personnage sélectionné
        currentCharacter = Instantiate(characterPrefab, gameObject.transform.position, gameObject.transform.rotation);
        DontDestroyOnLoad(currentCharacter);

        // Vérifie si le préfabriqué sélectionné est masculin ou féminin
        if (characterPrefab == MaleCharacterPrefab)
        {
            isMale = true;
        }
        else if (characterPrefab == FemaleCharacterPrefab)
        {
            isMale = false;
        }
        else
        {
            Debug.LogError("Préfabriqué de personnage inconnu sélectionné.");
        }
    }
}
