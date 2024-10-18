using System.Collections.Generic;
using BehaviorTree;

public class GuardBT : Tree
{
    /// <summary>
    /// Tableau de points de passage (waypoints) que la garde doit suivre lors de sa patrouille.
    /// </summary>
    public UnityEngine.Transform[] waypoints;

    /// <summary>
    /// Vitesse � laquelle la garde se d�place.
    /// </summary>
    public static float speed = 2f;

    /// <summary>
    /// Port�e de vision de la garde, repr�sentant la distance maximale � laquelle elle peut d�tecter un ennemi.
    /// </summary>
    public static float fovRange = 6f;

    /// <summary>
    /// Port�e d'attaque de la garde, repr�sentant la distance maximale � laquelle elle peut attaquer un ennemi.
    /// </summary>
    public static float attackRange = 2f;

    /// <summary>
    /// Angle du c�ne de vision de la garde, d�finissant l'angle de d�tection des ennemis devant elle.
    /// </summary>
    public static float coneAngle = 90f;

    /// <summary>
    /// Configuration de l'arbre de comportement pour la garde.
    /// Ce n�ud racine s�lectionne une t�che � ex�cuter en fonction de la situation actuelle.
    /// </summary>
    /// <returns>Le n�ud racine de l'arbre de comportement.</returns>
    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            // S�quence pour v�rifier si un ennemi est � port�e d'attaque, puis attaquer.
            //new Sequence(new List<Node>
            //{
            //    new CheckEnemyInAttackRange(transform),
            //    new TaskAttack(transform),
            //}),

            // S�quence pour v�rifier si un ennemi est visible, puis se d�placer vers lui.
            //new Sequence(new List<Node>
            //{
            //    new Check(transform),
            //    new GoToTarget(transform),
            //}),

            // T�che de patrouille entre les points de passage.
            new TaskPatrol(transform, waypoints),
        });
        return root;
    }
}
