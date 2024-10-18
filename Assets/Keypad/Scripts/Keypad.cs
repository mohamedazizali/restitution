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
        [SerializeField] private UnityEvent onAccessGranted; // Événement déclenché lorsque l'accès est accordé
        [SerializeField] private UnityEvent onAccessDenied; // Événement déclenché lorsque l'accès est refusé

        [Header("Combination Code (9 Numbers Max)")]
        [SerializeField] private int keypadCombo = 12345; // Code de combinaison du clavier (maximum 9 chiffres)

        public UnityEvent OnAccessGranted => onAccessGranted; // Propriété pour accéder à l'événement d'accès accordé
        public UnityEvent OnAccessDenied => onAccessDenied; // Propriété pour accéder à l'événement d'accès refusé

        [Header("Settings")]
        [SerializeField] private string accessGrantedText = "Granted"; // Texte affiché lorsque l'accès est accordé
        [SerializeField] private string accessDeniedText = "Denied"; // Texte affiché lorsque l'accès est refusé

        [Header("Visuals")]
        [SerializeField] private float displayResultTime = 1f; // Temps d'affichage du résultat
        [Range(0, 5)]
        [SerializeField] private float screenIntensity = 2.5f; // Intensité de l'émission de lumière du panneau

        [Header("Colors")]
        [SerializeField] private Color screenNormalColor = new(0.98f, 0.50f, 0.032f, 1f); // Couleur normale du panneau (orangé)
        [SerializeField] private Color screenDeniedColor = new(1f, 0f, 0f, 1f); // Couleur lorsque l'accès est refusé (rouge)
        [SerializeField] private Color screenGrantedColor = new(0f, 0.62f, 0.07f); // Couleur lorsque l'accès est accordé (verdâtre)

        [Header("SoundFx")]
        [SerializeField] private AudioClip buttonClickedSfx; // Son joué lorsque le bouton est cliqué
        [SerializeField] private AudioClip accessDeniedSfx; // Son joué lorsque l'accès est refusé
        [SerializeField] private AudioClip accessGrantedSfx; // Son joué lorsque l'accès est accordé

        [Header("Component References")]
        [SerializeField] private Renderer panelMesh; // Référence au composant Renderer du panneau
        [SerializeField] private TMP_Text keypadDisplayText; // Référence au composant TMP_Text pour afficher le texte du clavier
        [SerializeField] private AudioSource audioSource; // Référence au composant AudioSource pour jouer les sons

        private string currentInput; // Chaîne de caractères pour stocker l'entrée actuelle
        private bool displayingResult = false; // Indique si le résultat est actuellement affiché
        private bool accessWasGranted = false; // Indique si l'accès a été accordé

        private void Awake()
        {
            ClearInput(); // Efface l'entrée actuelle au démarrage
            panelMesh.material.SetVector("_EmissionColor", screenNormalColor * screenIntensity); // Définit la couleur d'émission normale du panneau
        }

        // Obtient la valeur du bouton pressé
        public void AddInput(string input)
        {
            audioSource.PlayOneShot(buttonClickedSfx); // Joue le son du bouton cliqué
            if (displayingResult || accessWasGranted) return; // Ignore l'entrée si le résultat est affiché ou l'accès est accordé
            switch (input)
            {
                case "enter":
                    CheckCombo(); // Vérifie la combinaison lorsque "enter" est pressé
                    break;
                default:
                    if (currentInput != null && currentInput.Length == 9) // Limite la taille de la combinaison à 9 chiffres maximum
                    {
                        return;
                    }
                    currentInput += input; // Ajoute l'entrée à la combinaison actuelle
                    keypadDisplayText.text = currentInput; // Met à jour le texte affiché du clavier
                    break;
            }
        }

        // Vérifie la combinaison actuelle
        public void CheckCombo()
        {
            if (int.TryParse(currentInput, out var currentKombo))
            {
                bool granted = currentKombo == keypadCombo; // Vérifie si la combinaison est correcte
                if (!displayingResult)
                {
                    StartCoroutine(DisplayResultRoutine(granted)); // Démarre la routine pour afficher le résultat
                }
            }
            else
            {
                Debug.LogWarning("Couldn't process input for some reason.."); // Affiche un avertissement si la combinaison ne peut pas être traitée
            }
        }

        // Routine pour afficher le résultat
        private IEnumerator DisplayResultRoutine(bool granted)
        {
            displayingResult = true; // Indique que le résultat est en cours d'affichage

            if (granted) AccessGranted(); // Accorde l'accès si la combinaison est correcte
            else AccessDenied(); // Refuse l'accès si la combinaison est incorrecte

            yield return new WaitForSeconds(displayResultTime); // Attend la durée d'affichage du résultat
            displayingResult = false; // Indique que l'affichage du résultat est terminé
            if (granted) yield break; // Si l'accès est accordé, termine la coroutine
            ClearInput(); // Efface l'entrée actuelle
            panelMesh.material.SetVector("_EmissionColor", screenNormalColor * screenIntensity); // Réinitialise la couleur d'émission normale du panneau
        }

        private void AccessDenied()
        {
            keypadDisplayText.text = accessDeniedText; // Met à jour le texte affiché pour indiquer que l'accès est refusé
            onAccessDenied?.Invoke(); // Déclenche l'événement d'accès refusé
            panelMesh.material.SetVector("_EmissionColor", screenDeniedColor * screenIntensity); // Change la couleur d'émission du panneau
            audioSource.PlayOneShot(accessDeniedSfx); // Joue le son d'accès refusé
        }

        private void ClearInput()
        {
            currentInput = ""; // Réinitialise l'entrée actuelle
            keypadDisplayText.text = currentInput; // Met à jour le texte affiché du clavier
        }

        private void AccessGranted()
        {
            accessWasGranted = true; // Indique que l'accès a été accordé
            keypadDisplayText.text = accessGrantedText; // Met à jour le texte affiché pour indiquer que l'accès est accordé
            onAccessGranted?.Invoke(); // Déclenche l'événement d'accès accordé
            panelMesh.material.SetVector("_EmissionColor", screenGrantedColor * screenIntensity); // Change la couleur d'émission du panneau
            audioSource.PlayOneShot(accessGrantedSfx); // Joue le son d'accès accordé
        }
    }
}
