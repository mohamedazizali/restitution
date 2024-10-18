using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public float snapThresholdDistance = 0.5f; // Distance seuil pour consid�rer que l'objet est bien positionn�
    private RectTransform rectTransform; // R�f�rence au RectTransform de l'objet
    private Vector2 initialPosition; // Position initiale de l'objet avant le d�placement

    public PuzzleManager puzzleManager; // R�f�rence au gestionnaire de puzzle
    public GameObject snapPoint; // Point de fixation auquel l'objet doit s'accrocher
    private bool isSnapped = false; // Indique si l'objet est bien positionn�

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>(); // Initialise le RectTransform
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        initialPosition = rectTransform.position; // Enregistre la position initiale au d�but du glissement
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition; // D�place l'objet avec la position de la souris
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SnapToTarget(); // Essaie d'accrocher l'objet au point de fixation lorsque le glissement se termine
    }

    private void SnapToTarget()
    {
        float distance = Vector2.Distance(rectTransform.position, snapPoint.transform.position); // Calcule la distance entre l'objet et le point de fixation

        if (distance < snapThresholdDistance) // V�rifie si l'objet est suffisamment proche du point de fixation
        {
            rectTransform.position = snapPoint.transform.position; // Positionne l'objet au point de fixation
            isSnapped = true; // Indique que l'objet est bien positionn�
            puzzleManager.CheckPuzzleCompletion(); // V�rifie si le puzzle est compl�t�
        }
        else
        {
            rectTransform.position = initialPosition; // Restaure la position initiale si l'objet n'est pas bien positionn�
            isSnapped = false; // Indique que l'objet n'est pas bien positionn�
            puzzleManager.ReduceLives(); // R�duit le nombre de vies si l'objet n'est pas bien positionn�
        }
    }

    public bool IsSnapped()
    {
        return isSnapped; // Retourne si l'objet est bien positionn�
    }
}
