using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavKeypad
{
    public class SlidingDoor : MonoBehaviour
    {
        [SerializeField] private Animator anim; // R�f�rence � l'Animator contr�lant la porte
        public bool IsOpoen => isOpen; // Propri�t� en lecture seule pour savoir si la porte est ouverte
        private bool isOpen = false; // �tat de la porte (ouverte ou ferm�e)

        // M�thode pour basculer l'�tat de la porte (ouverte/ferm�e)
        public void ToggleDoor()
        {
            isOpen = !isOpen; // Inverse l'�tat actuel de la porte
            anim.SetBool("isOpen", isOpen); // Met � jour le param�tre "isOpen" de l'Animator
        }

        // M�thode pour ouvrir la porte
        public void OpenDoor()
        {
            isOpen = true; // D�finit l'�tat de la porte sur ouverte
            anim.SetBool("isOpen", isOpen); // Met � jour le param�tre "isOpen" de l'Animator
        }

        // M�thode pour fermer la porte
        public void CloseDoor()
        {
            isOpen = false; // D�finit l'�tat de la porte sur ferm�e
            anim.SetBool("isOpen", isOpen); // Met � jour le param�tre "isOpen" de l'Animator
        }
    }
}
