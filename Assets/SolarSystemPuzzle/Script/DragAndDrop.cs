using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public float snapThresholdDistance = 0.5f; // Distance seuil pour considérer que l'objet est bien positionné
    private RectTransform rectTransform; // Référence au RectTransform de l'objet
    private Vector2 initialPosition; // Position initiale de l'objet avant le déplacement

    public PuzzleManager puzzleManager; // Référence au gestionnaire de puzzle
    public GameObject snapPoint; // Point de fixation auquel l'objet doit s'accrocher
    private bool isSnapped = false; // Indique si l'objet est bien positionné

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>(); // Initialise le RectTransform
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        initialPosition = rectTransform.position; // Enregistre la position initiale au début du glissement
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition; // Déplace l'objet avec la position de la souris
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SnapToTarget(); // Essaie d'accrocher l'objet au point de fixation lorsque le glissement se termine
    }

    private void SnapToTarget()
    {
        float distance = Vector2.Distance(rectTransform.position, snapPoint.transform.position); // Calcule la distance entre l'objet et le point de fixation

        if (distance < snapThresholdDistance) // Vérifie si l'objet est suffisamment proche du point de fixation
        {
            rectTransform.position = snapPoint.transform.position; // Positionne l'objet au point de fixation
            isSnapped = true; // Indique que l'objet est bien positionné
            puzzleManager.CheckPuzzleCompletion(); // Vérifie si le puzzle est complété
        }
        else
        {
            rectTransform.position = initialPosition; // Restaure la position initiale si l'objet n'est pas bien positionné
            isSnapped = false; // Indique que l'objet n'est pas bien positionné
            puzzleManager.ReduceLives(); // Réduit le nombre de vies si l'objet n'est pas bien positionné
        }
    }

    public bool IsSnapped()
    {
        return isSnapped; // Retourne si l'objet est bien positionné
    }
}
