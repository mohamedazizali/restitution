using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavKeypad
{
    public class KeypadInteractionFPV : MonoBehaviour
    {
        private Camera cam; // R�f�rence � la cam�ra principale

        private void Awake() => cam = Camera.main; // Initialise la r�f�rence � la cam�ra principale au d�marrage du script

        private void Update()
        {
            // Cr�e un rayon partant de la position de la souris � l'�cran
            var ray = cam.ScreenPointToRay(Input.mousePosition);

            // V�rifie si le bouton gauche de la souris est enfonc�
            if (Input.GetMouseButtonDown(0))
            {
                // Effectue un lancer de rayon depuis la position de la souris
                if (Physics.Raycast(ray, out var hit))
                {
                    // V�rifie si l'objet touch� poss�de un composant KeypadButton
                    if (hit.collider.TryGetComponent(out KeypadButton keypadButton))
                    {
                        // Appelle la m�thode PressButton() du composant KeypadButton
                        keypadButton.PressButton();
                    }
                }
            }
        }
    }
}
