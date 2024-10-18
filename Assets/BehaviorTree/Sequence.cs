using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// Classe Sequence : Repr�sente un n�ud de s�quence dans un arbre de comportement.
    /// Ce n�ud �value ses enfants s�quentiellement et retourne un �tat en fonction des r�sultats des enfants.
    /// </summary>
    public class Sequence : Node
    {
        /// <summary>
        /// Constructeur par d�faut de la classe Sequence.
        /// Initialise un n�ud de s�quence sans enfants.
        /// </summary>
        public Sequence() : base() { }

        /// <summary>
        /// Constructeur de la classe Sequence avec une liste d'enfants.
        /// Initialise un n�ud de s�quence avec une liste de n�uds enfants.
        /// </summary>
        /// <param name="children">Liste des n�uds enfants associ�s � ce n�ud de s�quence.</param>
        public Sequence(List<Node> children) : base(children) { }

        /// <summary>
        /// M�thode Evaluate : �value les n�uds enfants de mani�re s�quentielle.
        /// Retourne l'�tat FAILURE d�s qu'un enfant �choue, SUCCESS si tous les enfants r�ussissent, 
        /// ou RUNNING si l'un des enfants est en cours d'ex�cution.
        /// </summary>
        /// <returns>Retourne l'�tat actuel du n�ud apr�s �valuation.</returns>
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
