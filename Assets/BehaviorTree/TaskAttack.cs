using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

/// <summary>
/// Classe TaskAttack : Représente un nœud de l'arbre de comportement qui gère l'attaque d'une cible.
/// Hérite de la classe abstraite Node.
/// </summary>
public class TaskAttack : Node
{
    /// <summary>
    /// Constructeur de la classe TaskAttack.
    /// Initialise le nœud d'attaque avec les paramètres nécessaires.
    /// </summary>
    /// <param name="transform">Référence à l'objet Transform de l'attaquant.</param>
    public TaskAttack(Transform transform)
    {

    }

    /// <summary>
    /// Méthode Evaluate : Exécute la logique d'attaque sur la cible.
    /// Récupère la cible à partir des données stockées dans l'arbre de comportement, puis passe l'état du nœud à RUNNING.
    /// </summary>
    /// <returns>Retourne l'état du nœud après évaluation.</returns>
    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        Debug.Log("attacking");
        state = NodeState.RUNNING;
        return state;
    }
}
