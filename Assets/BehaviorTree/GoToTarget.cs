using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class GoToTarget : Node
{
    /// <summary>
    /// R�f�rence � l'animateur pour contr�ler les animations du personnage.
    /// </summary>
    private Animator _animator;

    /// <summary>
    /// R�f�rence � la transformation (position, rotation, �chelle) du personnage.
    /// </summary>
    private Transform _transform;

    /// <summary>
    /// Constructeur de la classe GoToTarget. Initialise les r�f�rences � la transformation et � l'animateur.
    /// </summary>
    /// <param name="transform">La transformation du personnage.</param>
    public GoToTarget(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    /// <summary>
    /// �value l'�tat du n�ud en se d�pla�ant vers la cible si elle est d�finie.
    /// </summary>
    /// <returns>L'�tat du n�ud apr�s l'�valuation.</returns>
    public override NodeState Evaluate()
    {
        // R�cup�re la position de la cible depuis les donn�es du comportement.
        Transform target = (Transform)GetData("target");

        // Si la distance entre la position actuelle et la cible est sup�rieure � un seuil minimal, se d�placer vers la cible.
        if (Vector3.Distance(_transform.position, target.position) > 0.01f)
        {
            // D�place le personnage vers la cible.
            _transform.position = Vector3.MoveTowards(_transform.position, target.position, GuardBT.speed * Time.deltaTime);

            // Oriente le personnage en direction de la cible.
            _transform.LookAt(target.position);

            // Ajuste la rotation sur l'axe X � 0 pour �viter une inclinaison ind�sirable.
            Vector3 euler = _transform.rotation.eulerAngles;
            euler.x = 0f;
            _transform.rotation = Quaternion.Euler(euler);

            // Active l'animation de marche.
            _animator.SetBool("Walking", true);
        }

        // D�finit l'�tat du n�ud comme �tant en cours d'ex�cution.
        state = NodeState.RUNNING;
        return state;
    }
}
