using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace NavKeypad
{
    public class Keypad : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private UnityEvent onAccessGranted; // �v�nement d�clench� lorsque l'acc�s est accord�
        [SerializeField] private UnityEvent onAccessDenied; // �v�nement d�clench� lorsque l'acc�s est refus�

        [Header("Combination Code (9 Numbers Max)")]
        [SerializeField] private int keypadCombo = 12345; // Code de combinaison du clavier (maximum 9 chiffres)

        public UnityEvent OnAccessGranted => onAccessGranted; // Propri�t� pour acc�der � l'�v�nement d'acc�s accord�
        public UnityEvent OnAccessDenied => onAccessDenied; // Propri�t� pour acc�der � l'�v�nement d'acc�s refus�

        [Header("Settings")]
        [SerializeField] private string accessGrantedText = "Granted"; // Texte affich� lorsque l'acc�s est accord�
        [SerializeField] private string accessDeniedText = "Denied"; // Texte affich� lorsque l'acc�s est refus�

        [Header("Visuals")]
        [SerializeField] private float displayResultTime = 1f; // Temps d'affichage du r�sultat
        [Range(0, 5)]
        [SerializeField] private float screenIntensity = 2.5f; // Intensit� de l'�mission de lumi�re du panneau

        [Header("Colors")]
        [SerializeField] private Color screenNormalColor = new(0.98f, 0.50f, 0.032f, 1f); // Couleur normale du panneau (orang�)
        [SerializeField] private Color screenDeniedColor = new(1f, 0f, 0f, 1f); // Couleur lorsque l'acc�s est refus� (rouge)
        [SerializeField] private Color screenGrantedColor = new(0f, 0.62f, 0.07f); // Couleur lorsque l'acc�s est accord� (verd�tre)

        [Header("SoundFx")]
        [SerializeField] private AudioClip buttonClickedSfx; // Son jou� lorsque le bouton est cliqu�
        [SerializeField] private AudioClip accessDeniedSfx; // Son jou� lorsque l'acc�s est refus�
        [SerializeField] private AudioClip accessGrantedSfx; // Son jou� lorsque l'acc�s est accord�

        [Header("Component References")]
        [SerializeField] private Renderer panelMesh; // R�f�rence au composant Renderer du panneau
        [SerializeField] private TMP_Text keypadDisplayText; // R�f�rence au composant TMP_Text pour afficher le texte du clavier
        [SerializeField] private AudioSource audioSource; // R�f�rence au composant AudioSource pour jouer les sons

        private string currentInput; // Cha�ne de caract�res pour stocker l'entr�e actuelle
        private bool displayingResult = false; // Indique si le r�sultat est actuellement affich�
        private bool accessWasGranted = false; // Indique si l'acc�s a �t� accord�

        private void Awake()
        {
            ClearInput(); // Efface l'entr�e actuelle au d�marrage
            panelMesh.material.SetVector("_EmissionColor", screenNormalColor * screenIntensity); // D�finit la couleur d'�mission normale du panneau
        }

        // Obtient la valeur du bouton press�
        public void AddInput(string input)
        {
            audioSource.PlayOneShot(buttonClickedSfx); // Joue le son du bouton cliqu�
            if (displayingResult || accessWasGranted) return; // Ignore l'entr�e si le r�sultat est affich� ou l'acc�s est accord�
            switch (input)
            {
                case "enter":
                    CheckCombo(); // V�rifie la combinaison lorsque "enter" est press�
                    break;
                default:
                    if (currentInput != null && currentInput.Length == 9) // Limite la taille de la combinaison � 9 chiffres maximum
                    {
                        return;
                    }
                    currentInput += input; // Ajoute l'entr�e � la combinaison actuelle
                    keypadDisplayText.text = currentInput; // Met � jour le texte affich� du clavier
                    break;
            }
        }

        // V�rifie la combinaison actuelle
        public void CheckCombo()
        {
            if (int.TryParse(currentInput, out var currentKombo))
            {
                bool granted = currentKombo == keypadCombo; // V�rifie si la combinaison est correcte
                if (!displayingResult)
                {
                    StartCoroutine(DisplayResultRoutine(granted)); // D�marre la routine pour afficher le r�sultat
                }
            }
            else
            {
                Debug.LogWarning("Couldn't process input for some reason.."); // Affiche un avertissement si la combinaison ne peut pas �tre trait�e
            }
        }

        // Routine pour afficher le r�sultat
        private IEnumerator DisplayResultRoutine(bool granted)
        {
            displayingResult = true; // Indique que le r�sultat est en cours d'affichage

            if (granted) AccessGranted(); // Accorde l'acc�s si la combinaison est correcte
            else AccessDenied(); // Refuse l'acc�s si la combinaison est incorrecte

            yield return new WaitForSeconds(displayResultTime); // Attend la dur�e d'affichage du r�sultat
            displayingResult = false; // Indique que l'affichage du r�sultat est termin�
            if (granted) yield break; // Si l'acc�s est accord�, termine la coroutine
            ClearInput(); // Efface l'entr�e actuelle
            panelMesh.material.SetVector("_EmissionColor", screenNormalColor * screenIntensity); // R�initialise la couleur d'�mission normale du panneau
        }

        private void AccessDenied()
        {
            keypadDisplayText.text = accessDeniedText; // Met � jour le texte affich� pour indiquer que l'acc�s est refus�
            onAccessDenied?.Invoke(); // D�clenche l'�v�nement d'acc�s refus�
            panelMesh.material.SetVector("_EmissionColor", screenDeniedColor * screenIntensity); // Change la couleur d'�mission du panneau
            audioSource.PlayOneShot(accessDeniedSfx); // Joue le son d'acc�s refus�
        }

        private void ClearInput()
        {
            currentInput = ""; // R�initialise l'entr�e actuelle
            keypadDisplayText.text = currentInput; // Met � jour le texte affich� du clavier
        }

        private void AccessGranted()
        {
            accessWasGranted = true; // Indique que l'acc�s a �t� accord�
            keypadDisplayText.text = accessGrantedText; // Met � jour le texte affich� pour indiquer que l'acc�s est accord�
            onAccessGranted?.Invoke(); // D�clenche l'�v�nement d'acc�s accord�
            panelMesh.material.SetVector("_EmissionColor", screenGrantedColor * screenIntensity); // Change la couleur d'�mission du panneau
            audioSource.PlayOneShot(accessGrantedSfx); // Joue le son d'acc�s accord�
        }
    }
}
