using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// Classe abstraite Tree : Représente la structure de base d'un arbre de comportement dans le jeu.
    /// Gère l'initialisation et l'évaluation de l'arbre de comportement.
    /// </summary>
    public abstract class Tree : MonoBehaviour
    {
        // Noeud racine de l'arbre de comportement.
        private Node _root = null;

        /// <summary>
        /// Méthode appelée au démarrage du script.
        /// Initialise l'arbre de comportement en configurant le nœud racine via la méthode SetupTree.
        /// </summary>
        protected void Start()
        {
            _root = SetupTree();
        }

        /// <summary>
        /// Méthode appelée à chaque frame.
        /// Évalue l'arbre de comportement en appelant la méthode Evaluate sur le nœud racine.
        /// </summary>
        private void Update()
        {
            if (_root != null)
                _root.Evaluate();
        }

        /// <summary>
        /// Méthode abstraite à implémenter dans les classes dérivées.
        /// Configure et retourne le nœud racine de l'arbre de comportement.
        /// </summary>
        /// <returns>Le nœud racine de l'arbre de comportement.</returns>
        protected abstract Node SetupTree();
    }
}
