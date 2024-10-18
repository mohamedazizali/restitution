using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterCustomUI : MonoBehaviour
{
    [SerializeField] private Button ShirtColorButton; // Bouton pour changer la couleur de la chemise
    [SerializeField] private Button PantsColorButton; // Bouton pour changer la couleur du pantalon
    [SerializeField] private Button ShoesColorButton; // Bouton pour changer la couleur des chaussures
    [SerializeField] private Button HairColorButton; // Bouton pour changer la couleur des cheveux

    [SerializeField] private Button ShirtColorPreviousButton; // Bouton pour revenir � la couleur pr�c�dente de la chemise
    [SerializeField] private Button PantsColorPreviousButton; // Bouton pour revenir � la couleur pr�c�dente du pantalon
    [SerializeField] private Button ShoesColorPreviousButton; // Bouton pour revenir � la couleur pr�c�dente des chaussures
    [SerializeField] private Button HairColorPreviousButton; // Bouton pour revenir � la couleur pr�c�dente des cheveux
    [SerializeField] private Button SaveButton; // Nouveau bouton pour enregistrer la personnalisation
    [SerializeField] private CharacterCustomizied characterCustomizer; // R�f�rence au script de personnalisation du personnage

    private void Update()
    {
        // Trouve le personnage avec le tag "Player" et obtient le composant CharacterCustomizied
        characterCustomizer = GameObject.FindWithTag("Player")?.GetComponent<CharacterCustomizied>();
    }

    private void Awake()
    {
        // Ajoute les �couteurs pour les boutons afin de d�clencher les changements de couleur
        ShirtColorButton.onClick.AddListener(() =>
        {
            characterCustomizer.ChangeColor(CharacterCustomizied.CharacterPart.Shirt);
        });

        PantsColorButton.onClick.AddListener(() =>
        {
            characterCustomizer.ChangeColor(CharacterCustomizied.CharacterPart.Pants);
        });

        ShoesColorButton.onClick.AddListener(() =>
        {
            characterCustomizer.ChangeColor(CharacterCustomizied.CharacterPart.Shoes);
        });

        HairColorButton.onClick.AddListener(() =>
        {
            characterCustomizer.ChangeColor(CharacterCustomizied.CharacterPart.Hair);
        });

        // Ajoute les �couteurs pour les boutons de couleur pr�c�dente
        ShirtColorPreviousButton.onClick.AddListener(() =>
        {
            characterCustomizer.ChangeColorToPrevious(CharacterCustomizied.CharacterPart.Shirt);
        });

        PantsColorPreviousButton.onClick.AddListener(() =>
        {
            characterCustomizer.ChangeColorToPrevious(CharacterCustomizied.CharacterPart.Pants);
        });

        ShoesColorPreviousButton.onClick.AddListener(() =>
        {
            characterCustomizer.ChangeColorToPrevious(CharacterCustomizied.CharacterPart.Shoes);
        });

        HairColorPreviousButton.onClick.AddListener(() =>
        {
            characterCustomizer.ChangeColorToPrevious(CharacterCustomizied.CharacterPart.Hair);
        });

        // Ajoute l'�couteur pour le bouton d'enregistrement
        SaveButton.onClick.AddListener(() =>
        {
            characterCustomizer.SaveCustomization();
            // Optionnel : Charger la sc�ne suivante
            //SceneManager.LoadScene(3); // Remplacer par le nom de votre sc�ne
        });
    }
}
