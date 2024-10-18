using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavKeypad
{
    public class KeypadButton : MonoBehaviour
    {
        [Header("Value")]
        [SerializeField] private string value; // Valeur associ�e au bouton

        [Header("Button Animation Settings")]
        [SerializeField] private float bttnspeed = 0.1f; // Vitesse de l'animation du bouton
        [SerializeField] private float moveDist = 0.0025f; // Distance de d�placement du bouton
        [SerializeField] private float buttonPressedTime = 0.1f; // Dur�e pendant laquelle le bouton reste enfonc�

        [Header("Component References")]
        [SerializeField] private Keypad keypad; // R�f�rence au composant Keypad

        private bool moving; // Indique si le bouton est en train de se d�placer

        // M�thode appel�e pour appuyer sur le bouton
        public void PressButton()
        {
            if (!moving)
            {
                keypad.AddInput(value); // Ajoute la valeur du bouton au keypad
                StartCoroutine(MoveSmooth()); // Lance l'animation de d�placement du bouton
            }
        }

        // Coroutine pour animer le d�placement du bouton
        private IEnumerator MoveSmooth()
        {
            moving = true; // Indique que le bouton est en cours de d�placement
            Vector3 startPos = transform.localPosition; // Position de d�part du bouton
            Vector3 endPos = transform.localPosition + new Vector3(0, 0, moveDist); // Position finale du bouton

            float elapsedTime = 0; // Temps �coul� depuis le d�but de l'animation
            while (elapsedTime < bttnspeed)
            {
                elapsedTime += Time.deltaTime; // Met � jour le temps �coul�
                float t = Mathf.Clamp01(elapsedTime / bttnspeed); // Calcule la valeur d'interpolation

                transform.localPosition = Vector3.Lerp(startPos, endPos, t); // D�place le bouton en douceur

                yield return null; // Attend la prochaine frame
            }
            transform.localPosition = endPos; // Assure que la position finale est atteinte
            yield return new WaitForSeconds(buttonPressedTime); // Attend que le bouton reste enfonc�

            startPos = transform.localPosition; // Met � jour la position de d�part
            endPos = transform.localPosition - new Vector3(0, 0, moveDist); // Position finale du bouton apr�s le rel�chement

            elapsedTime = 0; // R�initialise le temps �coul�
            while (elapsedTime < bttnspeed)
            {
                elapsedTime += Time.deltaTime; // Met � jour le temps �coul�
                float t = Mathf.Clamp01(elapsedTime / bttnspeed); // Calcule la valeur d'interpolation

                transform.localPosition = Vector3.Lerp(startPos, endPos, t); // D�place le bouton en douceur

                yield return null; // Attend la prochaine frame
            }
            transform.localPosition = endPos; // Assure que la position finale est atteinte

            moving = false; // Indique que le bouton a termin� son d�placement
        }
    }
}
