using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// Classe abstraite Tree : Repr�sente la structure de base d'un arbre de comportement dans le jeu.
    /// G�re l'initialisation et l'�valuation de l'arbre de comportement.
    /// </summary>
    public abstract class Tree : MonoBehaviour
    {
        // Noeud racine de l'arbre de comportement.
        private Node _root = null;

        /// <summary>
        /// M�thode appel�e au d�marrage du script.
        /// Initialise l'arbre de comportement en configurant le n�ud racine via la m�thode SetupTree.
        /// </summary>
        protected void Start()
        {
            _root = SetupTree();
        }

        /// <summary>
        /// M�thode appel�e � chaque frame.
        /// �value l'arbre de comportement en appelant la m�thode Evaluate sur le n�ud racine.
        /// </summary>
        private void Update()
        {
            if (_root != null)
                _root.Evaluate();
        }

        /// <summary>
        /// M�thode abstraite � impl�menter dans les classes d�riv�es.
        /// Configure et retourne le n�ud racine de l'arbre de comportement.
        /// </summary>
        /// <returns>Le n�ud racine de l'arbre de comportement.</returns>
        protected abstract Node SetupTree();
    }
}
