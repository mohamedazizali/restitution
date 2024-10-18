using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

/// <summary>
/// Classe TaskPatrol : G�re le comportement de patrouille d'un agent en suivant une s�rie de points de passage.
/// H�rite de la classe Node, utilis�e dans l'impl�mentation de l'arbre de comportement.
/// </summary>
public class TaskPatrol : Node
{
    // Transform de l'agent qui patrouille.
    private Transform _transform;

    // Tableau de points de passage que l'agent suivra.
    private Transform[] _waypoints;

    // Composant Animator de l'agent, utilis� pour g�rer les animations.
    private Animator _animator;

    // Indice du point de passage actuel vers lequel l'agent se dirige.
    private int _currentWayPointIndex = 0;

    // Temps d'attente d�fini lorsque l'agent atteint un point de passage.
    private float _waitTime = 0f;

    // Compteur pour mesurer le temps pass� � attendre.
    private float _waitCounter = 0f;

    // Bool�en indiquant si l'agent est en attente (immobile) ou non.
    private bool _waiting = false;

    /// <summary>
    /// Constructeur de la classe TaskPatrol.
    /// Initialise le transform de l'agent et les points de passage.
    /// </summary>
    /// <param name="transform">Le transform de l'agent.</param>
    /// <param name="waypoints">Tableau de points de passage � suivre.</param>
    public TaskPatrol(Transform transform, Transform[] waypoints)
    {
        _transform = transform;
        _waypoints = waypoints;
        _animator = transform.GetComponent<Animator>();
    }

    /// <summary>
    /// �value l'�tat actuel de la t�che de patrouille.
    /// G�re le d�placement de l'agent entre les points de passage et le comportement d'attente � chaque point.
    /// </summary>
    /// <returns>L'�tat du n�ud (RUNNING) apr�s �valuation.</returns>
    public override NodeState Evaluate()
    {
        // Si l'agent est en attente, on incr�mente le compteur d'attente.
        if (_waiting)
        {
            _waitCounter += Time.deltaTime;
            // Si le temps d'attente est �coul�, l'agent reprend sa marche.
            if (_waitCounter >= _waitTime)
                _waiting = false;

            _animator.SetBool("Walking", true);
        }
        else
        {
            // R�cup�re le point de passage actuel.
            Transform wp = _waypoints[_currentWayPointIndex];

            // Si l'agent est tr�s proche du point de passage, il s'arr�te et attend.
            if (Vector3.Distance(_transform.position, wp.position) < 0.01f)
            {
                _transform.position = wp.position;
                _waitCounter = 0f;
                _waiting = true;
                // Passage au prochain point de passage, ou retour au d�but s'il n'y en a plus.
                _currentWayPointIndex = (_currentWayPointIndex + 1) % _waypoints.Length;
                _animator.SetBool("Walking", false);
            }
            else
            {
                // D�place l'agent vers le point de passage.
                _transform.position = Vector3.MoveTowards(_transform.position, wp.position, GuardBT.speed * Time.deltaTime);
                _transform.LookAt(wp.position);

                // Fixe la rotation sur l'axe X � 0 pour �viter des inclinaisons non souhait�es.
                Vector3 euler = _transform.rotation.eulerAngles;
                euler.x = 0f;
                _transform.rotation = Quaternion.Euler(euler);
            }
        }

        // Retourne l'�tat RUNNING pour indiquer que la t�che est en cours.
        state = NodeState.RUNNING;
        return state;
    }
}
