using StarterAssets;
using System.Collections;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject ThirdCam; // R�f�rence � la cam�ra � la troisi�me personne
    public GameObject FirstCam; // R�f�rence � la cam�ra � la premi�re personne
    public int CamMode; // Mode de la cam�ra (0 pour la cam�ra � la troisi�me personne, 1 pour la cam�ra � la premi�re personne)
    [SerializeField]
    private ThirdPersonController controller; // R�f�rence au contr�leur de la troisi�me personne
    public GameObject InventoryTab1, InventoryTab2, CursorCam; // R�f�rences aux onglets de l'inventaire et � la cam�ra du curseur
    public GameObject CtoExit; // R�f�rence � l'objet � activer/d�sactiver en fonction du mode de cam�ra
    public GameObject phone; // R�f�rence � l'objet t�l�phone

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Verrouille le curseur au centre de l'�cran
        Cursor.visible = false; // Rend le curseur invisible
    }

    private void Update()
    {
        // V�rifie si le bouton "Camera" est press� pour changer de mode de cam�ra
        if (Input.GetButtonDown("Camera"))
        {
            ToggleCameraMode(); // Change le mode de cam�ra
        }

        // V�rifie si le bouton "Inventory" est press� pour ouvrir/fermer l'inventaire
        if (Input.GetButtonDown("Inventory"))
        {
            ToggleInventory(); // Ouvre ou ferme l'inventaire
        }
    }

    public void ToggleCameraMode()
    {
        CamMode = (CamMode == 0) ? 1 : 0; // Bascule entre les modes de cam�ra (0 et 1)

        if (CamMode == 0)
        {
            CursorCam.SetActive(false); // D�sactive la cam�ra du curseur en mode cam�ra � la troisi�me personne
        }
        else
        {
            CursorCam.SetActive(true); // Active la cam�ra du curseur en mode cam�ra � la premi�re personne
        }

        StartCoroutine(CamChange()); // D�marre la coroutine pour changer de cam�ra
    }

    void ToggleInventory()
    {
        // Bascule la visibilit� des onglets de l'inventaire
        InventoryTab1.SetActive(!InventoryTab1.activeSelf);
        InventoryTab2.SetActive(!InventoryTab2.activeSelf);
        InventoryManager.Instance.ListItems(); // Met � jour la liste des objets dans l'inventaire
        ToggleCursorVisibilityAndLockState(); // Bascule la visibilit� et l'�tat de verrouillage du curseur
    }

    void ToggleCursorVisibilityAndLockState()
    {
        Cursor.visible = !Cursor.visible; // Bascule la visibilit� du curseur

        if (Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.None; // D�verrouille le curseur si visible
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; // Verrouille le curseur si invisible
        }
    }

    IEnumerator CamChange()
    {
        yield return new WaitForSeconds(0.01f); // Attend un court instant avant de changer la cam�ra
        if (CamMode == 0)
        {
            ThirdCam.SetActive(true); // Active la cam�ra � la troisi�me personne
            FirstCam.SetActive(false); // D�sactive la cam�ra � la premi�re personne
            controller.enabled = true; // Active le contr�leur de la troisi�me personne
            CtoExit.SetActive(false); // D�sactive l'objet pour quitter
            phone.SetActive(true); // Active l'objet t�l�phone
        }
        else
        {
            ThirdCam.SetActive(false); // D�sactive la cam�ra � la troisi�me personne
            FirstCam.SetActive(true); // Active la cam�ra � la premi�re personne
            controller.enabled = false; // D�sactive le contr�leur de la troisi�me personne
        }
    }
}
