using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class Check : Node
{
    /// <summary>
    /// Transform du personnage effectuant la vérification.
    /// </summary>
    private Transform _transform;

    /// <summary>
    /// Masque de calque utilisé pour détecter les ennemis. L'ennemi doit être sur le calque avec l'indice 6.
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
    /// Évalue l'état du nœud en vérifiant si un ennemi est à portée et dans le champ de vision du personnage.
    /// </summary>
    /// <returns>L'état du nœud après l'évaluation.</returns>
    public override NodeState Evaluate()
    {
        // Récupère la cible à partir des données du comportement.
        object t = GetData("target");
        if (t == null)
        {
            // Détecte les objets dans la sphère autour du personnage en utilisant le masque de calque des ennemis.
            Collider[] colliders = Physics.OverlapSphere(_transform.position,
                GuardBT.fovRange, _enemyLayerMask);

            // Parcourt tous les colliders détectés pour trouver celui qui est dans le champ de vision.
            foreach (Collider collider in colliders)
            {
                // Calcule la direction vers la cible en ignorant la différence verticale.
                Vector3 directionToTarget = collider.transform.position - _transform.position;
                directionToTarget.y = 0f;

                // Calcule l'angle entre la direction vers l'avant du personnage et la direction vers la cible.
                float angleToTarget = Vector3.Angle(_transform.forward, directionToTarget);

                // Si l'angle est dans le champ de vision, définit la cible et retourne un succès.
                if (angleToTarget <= GuardBT.coneAngle / 2)
                {
                    parent.parent.SetData("target", collider.transform);
                    state = NodeState.SUCCESS;
                    return state;
                }
            }

            // Aucune cible valide trouvée dans le champ de vision.
            state = NodeState.FAILURE;
            return state;
        }

        // Cible déjà définie.
        state = NodeState.SUCCESS;
        return state;
    }
}
