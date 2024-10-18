using StarterAssets;
using System.Collections;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject ThirdCam; // Référence à la caméra à la troisième personne
    public GameObject FirstCam; // Référence à la caméra à la première personne
    public int CamMode; // Mode de la caméra (0 pour la caméra à la troisième personne, 1 pour la caméra à la première personne)
    [SerializeField]
    private ThirdPersonController controller; // Référence au contrôleur de la troisième personne
    public GameObject InventoryTab1, InventoryTab2, CursorCam; // Références aux onglets de l'inventaire et à la caméra du curseur
    public GameObject CtoExit; // Référence à l'objet à activer/désactiver en fonction du mode de caméra
    public GameObject phone; // Référence à l'objet téléphone

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Verrouille le curseur au centre de l'écran
        Cursor.visible = false; // Rend le curseur invisible
    }

    private void Update()
    {
        // Vérifie si le bouton "Camera" est pressé pour changer de mode de caméra
        if (Input.GetButtonDown("Camera"))
        {
            ToggleCameraMode(); // Change le mode de caméra
        }

        // Vérifie si le bouton "Inventory" est pressé pour ouvrir/fermer l'inventaire
        if (Input.GetButtonDown("Inventory"))
        {
            ToggleInventory(); // Ouvre ou ferme l'inventaire
        }
    }

    public void ToggleCameraMode()
    {
        CamMode = (CamMode == 0) ? 1 : 0; // Bascule entre les modes de caméra (0 et 1)

        if (CamMode == 0)
        {
            CursorCam.SetActive(false); // Désactive la caméra du curseur en mode caméra à la troisième personne
        }
        else
        {
            CursorCam.SetActive(true); // Active la caméra du curseur en mode caméra à la première personne
        }

        StartCoroutine(CamChange()); // Démarre la coroutine pour changer de caméra
    }

    void ToggleInventory()
    {
        // Bascule la visibilité des onglets de l'inventaire
        InventoryTab1.SetActive(!InventoryTab1.activeSelf);
        InventoryTab2.SetActive(!InventoryTab2.activeSelf);
        InventoryManager.Instance.ListItems(); // Met à jour la liste des objets dans l'inventaire
        ToggleCursorVisibilityAndLockState(); // Bascule la visibilité et l'état de verrouillage du curseur
    }

    void ToggleCursorVisibilityAndLockState()
    {
        Cursor.visible = !Cursor.visible; // Bascule la visibilité du curseur

        if (Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.None; // Déverrouille le curseur si visible
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; // Verrouille le curseur si invisible
        }
    }

    IEnumerator CamChange()
    {
        yield return new WaitForSeconds(0.01f); // Attend un court instant avant de changer la caméra
        if (CamMode == 0)
        {
            ThirdCam.SetActive(true); // Active la caméra à la troisième personne
            FirstCam.SetActive(false); // Désactive la caméra à la première personne
            controller.enabled = true; // Active le contrôleur de la troisième personne
            CtoExit.SetActive(false); // Désactive l'objet pour quitter
            phone.SetActive(true); // Active l'objet téléphone
        }
        else
        {
            ThirdCam.SetActive(false); // Désactive la caméra à la troisième personne
            FirstCam.SetActive(true); // Active la caméra à la première personne
            controller.enabled = false; // Désactive le contrôleur de la troisième personne
        }
    }
}
