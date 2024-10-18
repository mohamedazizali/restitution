using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AN_HeroInteractive : MonoBehaviour
{
    [Tooltip("Are you have any key?")]
    public bool RedKey = false; // Indique si le joueur a la clé rouge
    public bool BlueKey = false; // Indique si le joueur a la clé bleue

    [Tooltip("Child empty object for plug following")]
    public Transform GoalPosition; // Référence à l'objet enfant pour suivre l'objectif
}
