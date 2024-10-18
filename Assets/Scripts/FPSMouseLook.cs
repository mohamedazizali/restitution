using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMouseLook : MonoBehaviour
{
    // Sensibilit� de la souris
    public float mouseSensitivity = 100f;

    // R�f�rence au corps du joueur (pour la rotation horizontale)
    public Transform playerBody;

    // Rotation actuelle sur l'axe X
    float xRotation = 0f;

    void Start()
    {
        // Verrouille le curseur de la souris au centre de l'�cran
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // R�cup�re les mouvements de la souris sur les axes X et Y
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Met � jour la rotation sur l'axe X (haut/bas) en fonction du mouvement de la souris
        xRotation -= mouseY;

        // Clampe la rotation sur l'axe X pour �viter une rotation excessive (de -90 � 90 degr�s)
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Applique la rotation sur l'axe X � l'objet auquel ce script est attach�
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Applique la rotation sur l'axe Y (gauche/droite) au corps du joueur
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
