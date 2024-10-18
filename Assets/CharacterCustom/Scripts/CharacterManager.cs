using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    /// <summary>
    /// Instance unique de CharacterManager pour acc�der � ses fonctionnalit�s de mani�re globale.
    /// </summary>
    public static CharacterManager Instance { get; private set; }

    /// <summary>
    /// Pr�fabriqu� du personnage masculin.
    /// </summary>
    public GameObject MaleCharacterPrefab;

    /// <summary>
    /// Pr�fabriqu� du personnage f�minin.
    /// </summary>
    public GameObject FemaleCharacterPrefab;

    private GameObject currentCharacter;
    /// <summary>
    /// Obtient le personnage actuellement s�lectionn�.
    /// </summary>
    public GameObject CurrentCharacter => currentCharacter;

    // Indicateur bool�en pour d�terminer le genre du personnage s�lectionn�
    private bool isMale;
    /// <summary>
    /// Obtient un indicateur bool�en indiquant si le personnage s�lectionn� est masculin.
    /// </summary>
    public bool IsMale => isMale;

    private void Awake()
    {
        // Assure que l'instance est unique et ne d�truit pas l'objet lors du chargement de nouvelles sc�nes.
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
    /// S�lectionne un pr�fabriqu� de personnage et d�finit l'indicateur de genre.
    /// </summary>
    /// <param name="characterPrefab">Le pr�fabriqu� du personnage � s�lectionner.</param>
    public void SelectCharacter(GameObject characterPrefab)
    {
        // D�truit le personnage actuel s'il existe
        if (currentCharacter != null)
        {
            Destroy(currentCharacter);
        }

        // Cr�e une nouvelle instance du personnage s�lectionn�
        currentCharacter = Instantiate(characterPrefab, gameObject.transform.position, gameObject.transform.rotation);
        DontDestroyOnLoad(currentCharacter);

        // V�rifie si le pr�fabriqu� s�lectionn� est masculin ou f�minin
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
            Debug.LogError("Pr�fabriqu� de personnage inconnu s�lectionn�.");
        }
    }
}
