using UnityEngine;
using DialogueEditor;

public class MouseController : MonoBehaviour
{
    private void OnEnable()
    {
        // Abonne les m�thodes aux �v�nements de d�but et de fin de conversation
        ConversationManager.OnConversationStarted += EnableMouse;
        ConversationManager.OnConversationEnded += DisableMouse;
    }

    private void OnDisable()
    {
        // D�sabonne les m�thodes des �v�nements de d�but et de fin de conversation
        ConversationManager.OnConversationStarted -= EnableMouse;
        ConversationManager.OnConversationEnded -= DisableMouse;
    }

    // Active le curseur de la souris lorsque la conversation commence
    public void EnableMouse()
    {
        Cursor.lockState = CursorLockMode.None; // D�verrouille le curseur de la souris
        Cursor.visible = true; // Rend le curseur visible
    }

    // D�sactive le curseur de la souris lorsque la conversation se termine
    public void DisableMouse()
    {
        Cursor.lockState = CursorLockMode.Locked; // Verrouille le curseur de la souris
        Cursor.visible = false; // Rend le curseur invisible
    }
}
