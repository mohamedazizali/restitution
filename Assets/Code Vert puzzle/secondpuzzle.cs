using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class secondpuzzle : MonoBehaviour
{
    public TMP_InputField digit1Field;
    public TMP_Text hintText;
    public TMP_Text feedbackText;
    public Button submitButton;
    public Button hintButton;
    public GameObject hintborder;
    private string correctAnswer = "3";
    private int attempts = 0;
    public GameObject endmessage;

    /// <summary>
    /// Initialisation des �l�ments du puzzle.
    /// </summary>
    void Start()
    {
        hintText.gameObject.SetActive(false);
        feedbackText.text = "";
        submitButton.onClick.AddListener(CheckAnswer);
        hintButton.onClick.AddListener(ShowHint);

        digit1Field.characterLimit = 1;
        digit1Field.onValueChanged.AddListener(delegate { ValidateDigit(digit1Field); });
    }

    /// <summary>
    /// Valide l'entr�e de l'utilisateur pour s'assurer qu'elle est un chiffre.
    /// </summary>
    /// <param name="input">Le champ de saisie � valider.</param>
    void ValidateDigit(TMP_InputField input)
    {
        if (input.text.Length > 0 && !char.IsDigit(input.text, 0))
        {
            input.text = "";
        }
    }

    /// <summary>
    /// V�rifie la r�ponse de l'utilisateur.
    /// </summary>
    void CheckAnswer()
    {
        string playerAnswer = digit1Field.text; // Puisqu'il n'y a qu'un seul champ de saisie
        if (playerAnswer == "3")
        {
            feedbackText.text = "Bien jou� ! Tu as d�chiffr� l��nigme";
            StartCoroutine(ShowEndMessageAfterDelay());
        }
        else
        {
            attempts++;
            feedbackText.text = "Incorrect. Essaye encore.";
            if (attempts >= 3)
            {
                feedbackText.text = "La r�ponse est 3.";
                StartCoroutine(ShowEndMessageAfterDelay());
            }
        }
    }

    /// <summary>
    /// Affiche le message de fin apr�s un d�lai de 5 secondes.
    /// </summary>
    /// <returns>Un IEnumerator pour le d�lai.</returns>
    IEnumerator ShowEndMessageAfterDelay()
    {
        yield return new WaitForSeconds(5);
        endmessage.SetActive(true);
    }

    /// <summary>
    /// Affiche l'indice pour le puzzle.
    /// </summary>
    void ShowHint()
    {
        hintborder.SetActive(true);
        hintText.gameObject.SetActive(true);
        //hintText.text = "Indice: Le 2�me chiffre est le 0.";
    }
}
