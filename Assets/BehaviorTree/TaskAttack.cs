using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

/// <summary>
/// Classe TaskAttack : Repr�sente un n�ud de l'arbre de comportement qui g�re l'attaque d'une cible.
/// H�rite de la classe abstraite Node.
/// </summary>
public class TaskAttack : Node
{
    /// <summary>
    /// Constructeur de la classe TaskAttack.
    /// Initialise le n�ud d'attaque avec les param�tres n�cessaires.
    /// </summary>
    /// <param name="transform">R�f�rence � l'objet Transform de l'attaquant.</param>
    public TaskAttack(Transform transform)
    {

    }

    /// <summary>
    /// M�thode Evaluate : Ex�cute la logique d'attaque sur la cible.
    /// R�cup�re la cible � partir des donn�es stock�es dans l'arbre de comportement, puis passe l'�tat du n�ud � RUNNING.
    /// </summary>
    /// <returns>Retourne l'�tat du n�ud apr�s �valuation.</returns>
    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        Debug.Log("attacking");
        state = NodeState.RUNNING;
        return state;
    }
}
