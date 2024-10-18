using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// Classe Sequence : Représente un nœud de séquence dans un arbre de comportement.
    /// Ce nœud évalue ses enfants séquentiellement et retourne un état en fonction des résultats des enfants.
    /// </summary>
    public class Sequence : Node
    {
        /// <summary>
        /// Constructeur par défaut de la classe Sequence.
        /// Initialise un nœud de séquence sans enfants.
        /// </summary>
        public Sequence() : base() { }

        /// <summary>
        /// Constructeur de la classe Sequence avec une liste d'enfants.
        /// Initialise un nœud de séquence avec une liste de nœuds enfants.
        /// </summary>
        /// <param name="children">Liste des nœuds enfants associés à ce nœud de séquence.</param>
        public Sequence(List<Node> children) : base(children) { }

        /// <summary>
        /// Méthode Evaluate : Évalue les nœuds enfants de manière séquentielle.
        /// Retourne l'état FAILURE dès qu'un enfant échoue, SUCCESS si tous les enfants réussissent, 
        /// ou RUNNING si l'un des enfants est en cours d'exécution.
        /// </summary>
        /// <returns>Retourne l'état actuel du nœud après évaluation.</returns>
        public override NodeState Evaluate()
        {
            bool anyChildIsRunning = false;
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }
            state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state;
        }
    }
}
