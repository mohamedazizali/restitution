using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D customCursorTexture; // Assign the custom cursor texture in the Inspector
    public Vector2 cursorHotspot = Vector2.zero; // The point in the cursor texture that will act as the click point
    public GameObject miniGameObject; // Reference to the mini-game GameObject

    private bool isCustomCursorActive = false;

    private void Start()
    {
        // Initially, hide the custom cursor
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void Update()
    {
        if (miniGameObject != null)
        {
            if (miniGameObject.activeInHierarchy && !isCustomCursorActive)
            {
                // If the mini-game GameObject is active and custom cursor is not active, set the custom cursor
                Cursor.SetCursor(customCursorTexture, cursorHotspot, CursorMode.Auto);
                isCustomCursorActive = true;
            }
            else if (!miniGameObject.activeInHierarchy && isCustomCursorActive)
            {
                // If the mini-game GameObject is not active and custom cursor is active, reset to the default cursor
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                isCustomCursorActive = false;
            }
        }
    }
}
