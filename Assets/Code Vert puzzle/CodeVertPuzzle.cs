using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CodeVertPuzzle : MonoBehaviour
{
    public TMP_InputField digit1Field;
    public TMP_InputField digit2Field;
    public TMP_InputField digit3Field;
    public TMP_InputField digit4Field;
    public TMP_Text hintText;
    public TMP_Text feedbackText;
    public Button submitButton;
    public Button hintButton;
    public GameObject hintborder;
    private int attempts = 0;
    [SerializeField] private string correctAnswer = "3018";  // Modifiable via Inspector
    public GameObject endmessage;
    public Animator Hintanimator;
    public GameObject[] Chalks;
    public AudioClip chalkAudio;  // Audio clip for chalk sound
    public AudioClip victoryAudio;  // Added AudioClip for victory sound
    private AudioSource audioSource;  // AudioSource to play audio  

    // Array to hold references to input fields
    private TMP_InputField[] inputFields;

    void Start()
    {
        // Initialize the array with the input fields
        inputFields = new TMP_InputField[] { digit1Field, digit2Field, digit3Field, digit4Field };

        hintText.gameObject.SetActive(false);
        feedbackText.text = "";
        submitButton.onClick.AddListener(CheckAnswer);
        hintButton.onClick.AddListener(ShowHint);

        // Set character limit and add listeners to input fields
        foreach (var field in inputFields)
        {
            field.characterLimit = 1;
            field.onValueChanged.AddListener(delegate { ValidateDigit(field); });
        }

        // Initialize AudioSource component
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = chalkAudio;

        // Add listener for input field selection
        foreach (var field in inputFields)
        {
            field.onSelect.AddListener(delegate { StartCoroutine(HandleArrowKeyInput()); });
        }
    }

    void ValidateDigit(TMP_InputField input)
    {
        if (input.text.Length > 0 && !char.IsDigit(input.text, 0))
        {
            input.text = "";
        }
    }

    void CheckAnswer()
    {
        string playerAnswer = digit1Field.text + digit2Field.text + digit3Field.text + digit4Field.text;
        if (playerAnswer == correctAnswer)
        {
            feedbackText.text = "Bien joué ! Tu as déchiffré l’énigme";
            StartCoroutine(PlayVictorySoundAndShowEndMessage());
        }
        else
        {
            attempts++;

            feedbackText.text = "Incorrect. Essaye encore.";
            // Destroy a Chalk GameObject when an incorrect attempt is made
            if (attempts <= Chalks.Length)
            {
                Destroy(Chalks[attempts - 1]);
                StartCoroutine(PlayChalkSound());
            }

            if (attempts >= 3)
            {
                feedbackText.text = "La réponse est " + correctAnswer + ".";
                StartCoroutine(PlayVictorySoundAndShowEndMessage());
            }
        }
    }

    IEnumerator PlayChalkSound()
    {
        audioSource.PlayOneShot(chalkAudio);  // Play the chalk audio clip
        yield return new WaitForSeconds(1);   // Wait for 1 second
        audioSource.Stop();                   // Stop the audio playback
    }

    IEnumerator PlayVictorySoundAndShowEndMessage()
    {
        if (victoryAudio != null)
        {
            audioSource.PlayOneShot(victoryAudio);  // Play the victory audio clip
        }
        yield return new WaitForSeconds(5);    // Wait for 5 seconds
        endmessage.SetActive(true);
    }

    void ShowHint()
    {
        hintborder.SetActive(true);
        hintText.gameObject.SetActive(true);
    }

    IEnumerator HandleArrowKeyInput()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                // Find the currently selected input field
                for (int i = 0; i < inputFields.Length; i++)
                {
                    if (inputFields[i].isFocused)
                    {
                        // Move to the next input field
                        int nextIndex = (i + 1) % inputFields.Length;
                        inputFields[nextIndex].Select();
                        yield return null;  // Wait until next frame to avoid immediate re-selection
                        break;
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                // Find the currently selected input field
                for (int i = 0; i < inputFields.Length; i++)
                {
                    if (inputFields[i].isFocused)
                    {
                        // Move to the previous input field
                        int prevIndex = (i - 1 + inputFields.Length) % inputFields.Length;
                        inputFields[prevIndex].Select();
                        yield return null;  // Wait until next frame to avoid immediate re-selection
                        break;
                    }
                }
            }
            yield return null;  // Check for input each frame
        }
    }
}
