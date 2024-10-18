using UnityEngine;
using DialogueEditor;

public class MouseController : MonoBehaviour
{
    private void OnEnable()
    {
        // Abonne les méthodes aux événements de début et de fin de conversation
        ConversationManager.OnConversationStarted += EnableMouse;
        ConversationManager.OnConversationEnded += DisableMouse;
    }

    private void OnDisable()
    {
        // Désabonne les méthodes des événements de début et de fin de conversation
        ConversationManager.OnConversationStarted -= EnableMouse;
        ConversationManager.OnConversationEnded -= DisableMouse;
    }

    // Active le curseur de la souris lorsque la conversation commence
    public void EnableMouse()
    {
        Cursor.lockState = CursorLockMode.None; // Déverrouille le curseur de la souris
        Cursor.visible = true; // Rend le curseur visible
    }

    // Désactive le curseur de la souris lorsque la conversation se termine
    public void DisableMouse()
    {
        Cursor.lockState = CursorLockMode.Locked; // Verrouille le curseur de la souris
        Cursor.visible = false; // Rend le curseur invisible
    }
}
