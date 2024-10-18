using System.Collections.Generic;
using BehaviorTree;

public class GuardBT : Tree
{
    /// <summary>
    /// Tableau de points de passage (waypoints) que la garde doit suivre lors de sa patrouille.
    /// </summary>
    public UnityEngine.Transform[] waypoints;

    /// <summary>
    /// Vitesse à laquelle la garde se déplace.
    /// </summary>
    public static float speed = 2f;

    /// <summary>
    /// Portée de vision de la garde, représentant la distance maximale à laquelle elle peut détecter un ennemi.
    /// </summary>
    public static float fovRange = 6f;

    /// <summary>
    /// Portée d'attaque de la garde, représentant la distance maximale à laquelle elle peut attaquer un ennemi.
    /// </summary>
    public static float attackRange = 2f;

    /// <summary>
    /// Angle du cône de vision de la garde, définissant l'angle de détection des ennemis devant elle.
    /// </summary>
    public static float coneAngle = 90f;

    /// <summary>
    /// Configuration de l'arbre de comportement pour la garde.
    /// Ce nœud racine sélectionne une tâche à exécuter en fonction de la situation actuelle.
    /// </summary>
    /// <returns>Le nœud racine de l'arbre de comportement.</returns>
    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            // Séquence pour vérifier si un ennemi est à portée d'attaque, puis attaquer.
            //new Sequence(new List<Node>
            //{
            //    new CheckEnemyInAttackRange(transform),
            //    new TaskAttack(transform),
            //}),

            // Séquence pour vérifier si un ennemi est visible, puis se déplacer vers lui.
            //new Sequence(new List<Node>
            //{
            //    new Check(transform),
            //    new GoToTarget(transform),
            //}),

            // Tâche de patrouille entre les points de passage.
            new TaskPatrol(transform, waypoints),
        });
        return root;
    }
}
