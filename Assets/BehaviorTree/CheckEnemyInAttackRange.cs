using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckEnemyInAttackRange : Node
{
    /// <summary>
    /// Masque de calque pour détecter les ennemis. L'ennemi doit être sur le calque avec l'indice 6.
    /// </summary>
    private static int _enemyLayerMask = 1 << 6;

    /// <summary>
    /// Transform du personnage qui exécute cette tâche.
    /// </summary>
    private Transform _transform;

    /// <summary>
    /// Composant Animator pour contrôler les animations du personnage.
    /// </summary>
    private Animator _animator;

    /// <summary>
    /// Angle de champ de vision ajustable dans l'éditeur.
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
    /// Évalue l'état du nœud en vérifiant si un ennemi est dans la portée d'attaque et dans le champ de vision.
    /// </summary>
    /// <returns>L'état du nœud après l'évaluation.</returns>
    public override NodeState Evaluate()
    {
        // Récupère la cible à partir des données du comportement.
        object t = GetData("target");
        if (t == null)
        {
            state = NodeState.FAILURE;
            return state;
        }
        Transform target = (Transform)t;

        // Calcule la direction vers la cible en ignorant la différence verticale.
        Vector3 directionToTarget = target.position - _transform.position;
        directionToTarget.y = 0f;

        // Calcule l'angle entre la direction vers l'avant du personnage et la direction vers la cible.
        float angleToTarget = Vector3.Angle(_transform.forward, directionToTarget);

        // Détermine le produit vectoriel pour vérifier si la cible est à gauche ou à droite du personnage.
        Vector3 crossProduct = Vector3.Cross(_transform.forward, directionToTarget);

        // Si la composante y du produit vectoriel est négative, la cible est à gauche, sinon elle est à droite.
        bool targetToLeft = crossProduct.y < 0;

        // Si la cible est derrière le personnage, inverse le drapeau de la cible à gauche.
        if (angleToTarget > 45f)
            targetToLeft = !targetToLeft;

        // Si l'angle est dans le champ de vision, que la cible est dans la portée d'attaque et devant le personnage, l'évaluation réussit.
        if (angleToTarget <= fieldOfViewAngle / 2 && Vector3.Distance(_transform.position, target.position) <= GuardBT.attackRange && !targetToLeft)
        {
            _animator.SetBool("Walking", false);
            state = NodeState.SUCCESS;
            return state;
        }

        // L'évaluation échoue si les conditions ne sont pas remplies.
        state = NodeState.FAILURE;
        return state;
    }

    /// <summary>
    /// Dessine le champ de vision dans l'éditeur pour visualiser la portée d'attaque.
    /// </summary>
    private void OnDrawGizmos()
    {
        // Vérifie que le transform du personnage n'est pas nul.
        if (_transform == null)
            return;

        Gizmos.color = Color.yellow;

        // Calcule la direction du cône basé sur le vecteur avant du personnage.
        Vector3 coneDirection = _transform.forward;

        // Calcule la position du cône basé sur la position du personnage.
        Vector3 conePosition = _transform.position;

        // Calcule les deux points qui représentent les bords du champ de vision.
        float halfFOVAngleRad = fieldOfViewAngle * Mathf.Deg2Rad / 2;
        float coneLength = 5f; // Ajuste la longueur selon les besoins

        Vector3 leftFOVEdge = conePosition + Quaternion.Euler(0, -halfFOVAngleRad * Mathf.Rad2Deg, 0) * coneDirection * coneLength;
        Vector3 rightFOVEdge = conePosition + Quaternion.Euler(0, halfFOVAngleRad * Mathf.Rad2Deg, 0) * coneDirection * coneLength;

        // Dessine les lignes pour représenter le champ de vision.
        Gizmos.DrawLine(conePosition, leftFOVEdge);
        Gizmos.DrawLine(conePosition, rightFOVEdge);
    }
}
