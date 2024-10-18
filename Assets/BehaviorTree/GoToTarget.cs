using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class GoToTarget : Node
{
    /// <summary>
    /// Référence à l'animateur pour contrôler les animations du personnage.
    /// </summary>
    private Animator _animator;

    /// <summary>
    /// Référence à la transformation (position, rotation, échelle) du personnage.
    /// </summary>
    private Transform _transform;

    /// <summary>
    /// Constructeur de la classe GoToTarget. Initialise les références à la transformation et à l'animateur.
    /// </summary>
    /// <param name="transform">La transformation du personnage.</param>
    public GoToTarget(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    /// <summary>
    /// Évalue l'état du nœud en se déplaçant vers la cible si elle est définie.
    /// </summary>
    /// <returns>L'état du nœud après l'évaluation.</returns>
    public override NodeState Evaluate()
    {
        // Récupère la position de la cible depuis les données du comportement.
        Transform target = (Transform)GetData("target");

        // Si la distance entre la position actuelle et la cible est supérieure à un seuil minimal, se déplacer vers la cible.
        if (Vector3.Distance(_transform.position, target.position) > 0.01f)
        {
            // Déplace le personnage vers la cible.
            _transform.position = Vector3.MoveTowards(_transform.position, target.position, GuardBT.speed * Time.deltaTime);

            // Oriente le personnage en direction de la cible.
            _transform.LookAt(target.position);

            // Ajuste la rotation sur l'axe X à 0 pour éviter une inclinaison indésirable.
            Vector3 euler = _transform.rotation.eulerAngles;
            euler.x = 0f;
            _transform.rotation = Quaternion.Euler(euler);

            // Active l'animation de marche.
            _animator.SetBool("Walking", true);
        }

        // Définit l'état du nœud comme étant en cours d'exécution.
        state = NodeState.RUNNING;
        return state;
    }
}
