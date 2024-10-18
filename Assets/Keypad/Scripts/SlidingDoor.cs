using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavKeypad
{
    public class SlidingDoor : MonoBehaviour
    {
        [SerializeField] private Animator anim; // Référence à l'Animator contrôlant la porte
        public bool IsOpoen => isOpen; // Propriété en lecture seule pour savoir si la porte est ouverte
        private bool isOpen = false; // État de la porte (ouverte ou fermée)

        // Méthode pour basculer l'état de la porte (ouverte/fermée)
        public void ToggleDoor()
        {
            isOpen = !isOpen; // Inverse l'état actuel de la porte
            anim.SetBool("isOpen", isOpen); // Met à jour le paramètre "isOpen" de l'Animator
        }

        // Méthode pour ouvrir la porte
        public void OpenDoor()
        {
            isOpen = true; // Définit l'état de la porte sur ouverte
            anim.SetBool("isOpen", isOpen); // Met à jour le paramètre "isOpen" de l'Animator
        }

        // Méthode pour fermer la porte
        public void CloseDoor()
        {
            isOpen = false; // Définit l'état de la porte sur fermée
            anim.SetBool("isOpen", isOpen); // Met à jour le paramètre "isOpen" de l'Animator
        }
    }
}
