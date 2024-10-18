using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavKeypad
{
    public class KeypadButton : MonoBehaviour
    {
        [Header("Value")]
        [SerializeField] private string value; // Valeur associée au bouton

        [Header("Button Animation Settings")]
        [SerializeField] private float bttnspeed = 0.1f; // Vitesse de l'animation du bouton
        [SerializeField] private float moveDist = 0.0025f; // Distance de déplacement du bouton
        [SerializeField] private float buttonPressedTime = 0.1f; // Durée pendant laquelle le bouton reste enfoncé

        [Header("Component References")]
        [SerializeField] private Keypad keypad; // Référence au composant Keypad

        private bool moving; // Indique si le bouton est en train de se déplacer

        // Méthode appelée pour appuyer sur le bouton
        public void PressButton()
        {
            if (!moving)
            {
                keypad.AddInput(value); // Ajoute la valeur du bouton au keypad
                StartCoroutine(MoveSmooth()); // Lance l'animation de déplacement du bouton
            }
        }

        // Coroutine pour animer le déplacement du bouton
        private IEnumerator MoveSmooth()
        {
            moving = true; // Indique que le bouton est en cours de déplacement
            Vector3 startPos = transform.localPosition; // Position de départ du bouton
            Vector3 endPos = transform.localPosition + new Vector3(0, 0, moveDist); // Position finale du bouton

            float elapsedTime = 0; // Temps écoulé depuis le début de l'animation
            while (elapsedTime < bttnspeed)
            {
                elapsedTime += Time.deltaTime; // Met à jour le temps écoulé
                float t = Mathf.Clamp01(elapsedTime / bttnspeed); // Calcule la valeur d'interpolation

                transform.localPosition = Vector3.Lerp(startPos, endPos, t); // Déplace le bouton en douceur

                yield return null; // Attend la prochaine frame
            }
            transform.localPosition = endPos; // Assure que la position finale est atteinte
            yield return new WaitForSeconds(buttonPressedTime); // Attend que le bouton reste enfoncé

            startPos = transform.localPosition; // Met à jour la position de départ
            endPos = transform.localPosition - new Vector3(0, 0, moveDist); // Position finale du bouton après le relâchement

            elapsedTime = 0; // Réinitialise le temps écoulé
            while (elapsedTime < bttnspeed)
            {
                elapsedTime += Time.deltaTime; // Met à jour le temps écoulé
                float t = Mathf.Clamp01(elapsedTime / bttnspeed); // Calcule la valeur d'interpolation

                transform.localPosition = Vector3.Lerp(startPos, endPos, t); // Déplace le bouton en douceur

                yield return null; // Attend la prochaine frame
            }
            transform.localPosition = endPos; // Assure que la position finale est atteinte

            moving = false; // Indique que le bouton a terminé son déplacement
        }
    }
}
