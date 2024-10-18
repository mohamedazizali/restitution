using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// Classe Selector : Représente un nœud sélecteur dans un arbre de comportement.
    /// Ce nœud évalue ses enfants dans l'ordre et retourne un état basé sur le premier enfant qui réussit ou qui est en cours d'exécution.
    /// </summary>
    public class Selector : Node
    {
        /// <summary>
        /// Constructeur par défaut de la classe Selector.
        /// Initialise un nœud sélecteur sans enfants.
        /// </summary>
        public Selector() : base() { }

        /// <summary>
        /// Constructeur de la classe Selector avec une liste d'enfants.
        /// Initialise un nœud sélecteur avec une liste de nœuds enfants.
        /// </summary>
        /// <param name="children">Liste des nœuds enfants associés à ce nœud sélecteur.</param>
        public Selector(List<Node> children) : base(children) { }

        /// <summary>
        /// Méthode Evaluate : Évalue les nœuds enfants de manière séquentielle.
        /// Retourne SUCCESS dès qu'un enfant réussit, RUNNING si un enfant est en cours d'exécution,
        /// ou FAILURE si tous les enfants échouent.
        /// </summary>
        /// <returns>Retourne l'état actuel du nœud après évaluation.</returns>
        public override NodeState Evaluate()
        {
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }
            state = NodeState.FAILURE;
            return state;
        }
    }
}
