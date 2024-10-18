using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// Classe Selector : Repr�sente un n�ud s�lecteur dans un arbre de comportement.
    /// Ce n�ud �value ses enfants dans l'ordre et retourne un �tat bas� sur le premier enfant qui r�ussit ou qui est en cours d'ex�cution.
    /// </summary>
    public class Selector : Node
    {
        /// <summary>
        /// Constructeur par d�faut de la classe Selector.
        /// Initialise un n�ud s�lecteur sans enfants.
        /// </summary>
        public Selector() : base() { }

        /// <summary>
        /// Constructeur de la classe Selector avec une liste d'enfants.
        /// Initialise un n�ud s�lecteur avec une liste de n�uds enfants.
        /// </summary>
        /// <param name="children">Liste des n�uds enfants associ�s � ce n�ud s�lecteur.</param>
        public Selector(List<Node> children) : base(children) { }

        /// <summary>
        /// M�thode Evaluate : �value les n�uds enfants de mani�re s�quentielle.
        /// Retourne SUCCESS d�s qu'un enfant r�ussit, RUNNING si un enfant est en cours d'ex�cution,
        /// ou FAILURE si tous les enfants �chouent.
        /// </summary>
        /// <returns>Retourne l'�tat actuel du n�ud apr�s �valuation.</returns>
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
