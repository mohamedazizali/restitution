using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class Check : Node
{
    /// <summary>
    /// Transform du personnage effectuant la v�rification.
    /// </summary>
    private Transform _transform;

    /// <summary>
    /// Masque de calque utilis� pour d�tecter les ennemis. L'ennemi doit �tre sur le calque avec l'indice 6.
    /// </summary>
    private static int _enemyLayerMask = 1 << 6;

    /// <summary>
    /// Constructeur de la classe Check. Initialise le transform du personnage.
    /// </summary>
    /// <param name="transform">Transform du personnage.</param>
    public Check(Transform transform)
    {
        _transform = transform;
    }

    /// <summary>
    /// �value l'�tat du n�ud en v�rifiant si un ennemi est � port�e et dans le champ de vision du personnage.
    /// </summary>
    /// <returns>L'�tat du n�ud apr�s l'�valuation.</returns>
    public override NodeState Evaluate()
    {
        // R�cup�re la cible � partir des donn�es du comportement.
        object t = GetData("target");
        if (t == null)
        {
            // D�tecte les objets dans la sph�re autour du personnage en utilisant le masque de calque des ennemis.
            Collider[] colliders = Physics.OverlapSphere(_transform.position,
                GuardBT.fovRange, _enemyLayerMask);

            // Parcourt tous les colliders d�tect�s pour trouver celui qui est dans le champ de vision.
            foreach (Collider collider in colliders)
            {
                // Calcule la direction vers la cible en ignorant la diff�rence verticale.
                Vector3 directionToTarget = collider.transform.position - _transform.position;
                directionToTarget.y = 0f;

                // Calcule l'angle entre la direction vers l'avant du personnage et la direction vers la cible.
                float angleToTarget = Vector3.Angle(_transform.forward, directionToTarget);

                // Si l'angle est dans le champ de vision, d�finit la cible et retourne un succ�s.
                if (angleToTarget <= GuardBT.coneAngle / 2)
                {
                    parent.parent.SetData("target", collider.transform);
                    state = NodeState.SUCCESS;
                    return state;
                }
            }

            // Aucune cible valide trouv�e dans le champ de vision.
            state = NodeState.FAILURE;
            return state;
        }

        // Cible d�j� d�finie.
        state = NodeState.SUCCESS;
        return state;
    }
}
