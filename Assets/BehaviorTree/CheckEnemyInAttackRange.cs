using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckEnemyInAttackRange : Node
{
    /// <summary>
    /// Masque de calque pour d�tecter les ennemis. L'ennemi doit �tre sur le calque avec l'indice 6.
    /// </summary>
    private static int _enemyLayerMask = 1 << 6;

    /// <summary>
    /// Transform du personnage qui ex�cute cette t�che.
    /// </summary>
    private Transform _transform;

    /// <summary>
    /// Composant Animator pour contr�ler les animations du personnage.
    /// </summary>
    private Animator _animator;

    /// <summary>
    /// Angle de champ de vision ajustable dans l'�diteur.
    /// </summary>
    public float fieldOfViewAngle = 45f;

    /// <summary>
    /// Constructeur de la classe CheckEnemyInAttackRange. Initialise le transform et l'animator.
    /// </summary>
    /// <param name="transform">Transform du personnage.</param>
    public CheckEnemyInAttackRange(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    /// <summary>
    /// �value l'�tat du n�ud en v�rifiant si un ennemi est dans la port�e d'attaque et dans le champ de vision.
    /// </summary>
    /// <returns>L'�tat du n�ud apr�s l'�valuation.</returns>
    public override NodeState Evaluate()
    {
        // R�cup�re la cible � partir des donn�es du comportement.
        object t = GetData("target");
        if (t == null)
        {
            state = NodeState.FAILURE;
            return state;
        }
        Transform target = (Transform)t;

        // Calcule la direction vers la cible en ignorant la diff�rence verticale.
        Vector3 directionToTarget = target.position - _transform.position;
        directionToTarget.y = 0f;

        // Calcule l'angle entre la direction vers l'avant du personnage et la direction vers la cible.
        float angleToTarget = Vector3.Angle(_transform.forward, directionToTarget);

        // D�termine le produit vectoriel pour v�rifier si la cible est � gauche ou � droite du personnage.
        Vector3 crossProduct = Vector3.Cross(_transform.forward, directionToTarget);

        // Si la composante y du produit vectoriel est n�gative, la cible est � gauche, sinon elle est � droite.
        bool targetToLeft = crossProduct.y < 0;

        // Si la cible est derri�re le personnage, inverse le drapeau de la cible � gauche.
        if (angleToTarget > 45f)
            targetToLeft = !targetToLeft;

        // Si l'angle est dans le champ de vision, que la cible est dans la port�e d'attaque et devant le personnage, l'�valuation r�ussit.
        if (angleToTarget <= fieldOfViewAngle / 2 && Vector3.Distance(_transform.position, target.position) <= GuardBT.attackRange && !targetToLeft)
        {
            _animator.SetBool("Walking", false);
            state = NodeState.SUCCESS;
            return state;
        }

        // L'�valuation �choue si les conditions ne sont pas remplies.
        state = NodeState.FAILURE;
        return state;
    }

    /// <summary>
    /// Dessine le champ de vision dans l'�diteur pour visualiser la port�e d'attaque.
    /// </summary>
    private void OnDrawGizmos()
    {
        // V�rifie que le transform du personnage n'est pas nul.
        if (_transform == null)
            return;

        Gizmos.color = Color.yellow;

        // Calcule la direction du c�ne bas� sur le vecteur avant du personnage.
        Vector3 coneDirection = _transform.forward;

        // Calcule la position du c�ne bas� sur la position du personnage.
        Vector3 conePosition = _transform.position;

        // Calcule les deux points qui repr�sentent les bords du champ de vision.
        float halfFOVAngleRad = fieldOfViewAngle * Mathf.Deg2Rad / 2;
        float coneLength = 5f; // Ajuste la longueur selon les besoins

        Vector3 leftFOVEdge = conePosition + Quaternion.Euler(0, -halfFOVAngleRad * Mathf.Rad2Deg, 0) * coneDirection * coneLength;
        Vector3 rightFOVEdge = conePosition + Quaternion.Euler(0, halfFOVAngleRad * Mathf.Rad2Deg, 0) * coneDirection * coneLength;

        // Dessine les lignes pour repr�senter le champ de vision.
        Gizmos.DrawLine(conePosition, leftFOVEdge);
        Gizmos.DrawLine(conePosition, rightFOVEdge);
    }
}
