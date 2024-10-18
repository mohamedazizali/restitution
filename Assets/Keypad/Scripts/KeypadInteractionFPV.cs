using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavKeypad
{
    public class KeypadInteractionFPV : MonoBehaviour
    {
        private Camera cam; // Référence à la caméra principale

        private void Awake() => cam = Camera.main; // Initialise la référence à la caméra principale au démarrage du script

        private void Update()
        {
            // Crée un rayon partant de la position de la souris à l'écran
            var ray = cam.ScreenPointToRay(Input.mousePosition);

            // Vérifie si le bouton gauche de la souris est enfoncé
            if (Input.GetMouseButtonDown(0))
            {
                // Effectue un lancer de rayon depuis la position de la souris
                if (Physics.Raycast(ray, out var hit))
                {
                    // Vérifie si l'objet touché possède un composant KeypadButton
                    if (hit.collider.TryGetComponent(out KeypadButton keypadButton))
                    {
                        // Appelle la méthode PressButton() du composant KeypadButton
                        keypadButton.PressButton();
                    }
                }
            }
        }
    }
}
