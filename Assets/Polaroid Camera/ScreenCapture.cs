using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenCapture : MonoBehaviour
{
    [Header("Photo Taker")]
    [SerializeField] private Image photoDisplayArea; // Zone d'affichage pour la photo captur�e
    [SerializeField] private GameObject photoFrame, polaroid; // R�f�rences au cadre photo et au polaroid
    float maxPictures = 0f; // Nombre maximal de photos pouvant �tre prises

    [Header("Flash Effect")]
    [SerializeField] private GameObject cameraFlash; // Effet de flash de la cam�ra
    [SerializeField] private float flashTime; // Dur�e du flash de la cam�ra

    [Header("Photo Fader Effect")]
    [SerializeField] private Animator fadingAnimation; // Animation de fondu pour la photo
    public CameraChange camchange; // R�f�rence au script CameraChange pour v�rifier le mode de cam�ra
    private Texture2D screenCapture; // Texture pour stocker la capture d'�cran
    private bool viewingPhoto; // Indique si la photo est actuellement affich�e
    [SerializeField] private bool screenshotOfNPC; // Indique si la capture d'�cran contient un NPC

    [SerializeField] private InventoryManager inventoryManager2; // R�f�rence au gestionnaire d'inventaire
    [SerializeField] private LayerMask npcLayerMask; // Masque de couche pour d�tecter les NPCs
    [SerializeField] private float wincon = 0f; // Condition de victoire
    [SerializeField] bool questCompleted = false; // Indique si la qu�te est compl�t�e
    [SerializeField] private AudioSource camsound; // Source audio pour les sons de la cam�ra
    [SerializeField] private AudioClip startSound; // Son jou� au d�but de la capture de photo

    void Start()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false); // Cr�e une texture pour la capture d'�cran
        camsound = gameObject.GetComponent<AudioSource>(); // Obtient le composant AudioSource
    }

    private void Update()
    {
        // V�rifie si le bouton gauche de la souris est press�, le mode cam�ra est correct et le nombre maximal de photos n'est pas atteint
        if (Input.GetMouseButtonDown(0) && camchange.CamMode == 1 && maxPictures < 6)
        {
            if (!viewingPhoto)
            {
                StartCoroutine(CapturePhoto()); // D�marre la coroutine pour capturer une photo
            }
            else
            {
                RemovePhoto(); // Supprime la photo affich�e
            }
            if (maxPictures == 5)
            {
                photoFrame.SetActive(false); // D�sactive le cadre photo lorsque le nombre maximal de photos est atteint
            }
        }
        wincon = inventoryManager2.CalculateTotalValue(); // Calcule la valeur totale des objets dans l'inventaire
        if (wincon > 2) { questCompleted = true; } // Marque la qu�te comme compl�t�e si la condition est remplie
    }

    IEnumerator CapturePhoto()
    {
        if (startSound != null && camsound != null)
        {
            camsound.PlayOneShot(startSound); // Joue le son de d�but de capture de photo
        }
        viewingPhoto = true; // Indique que la photo est en cours d'affichage
        yield return new WaitForEndOfFrame(); // Attend la fin du cadre actuel

        // Cr�e une texture pour la nouvelle capture d'�cran
        Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);
        Texture2D newScreenshotTexture = new Texture2D((int)regionToRead.width, (int)regionToRead.height, TextureFormat.RGB24, false);
        newScreenshotTexture.ReadPixels(regionToRead, 0, 0, false);
        newScreenshotTexture.Apply();

        float value = 0;
        screenshotOfNPC = IsNPCInScreenshot(); // V�rifie si la capture d'�cran contient un NPC
        if (screenshotOfNPC) { value = 1; }
        maxPictures++; // Incr�mente le compteur de photos

        // Cr�e un Sprite unique pour cette capture d'�cran
        Sprite screenshotSprite = Sprite.Create(newScreenshotTexture, new Rect(0.0f, 0.0f, newScreenshotTexture.width, newScreenshotTexture.height), new Vector2(0.5f, 0.5f), 100.0f);

        ShowPhoto(); // Affiche la photo
        inventoryManager2.ListItems(); // Met � jour la liste des objets dans l'inventaire
        inventoryManager2.AddScreenshotItem(screenshotSprite, "Picture", value); // Ajoute la capture d'�cran comme un objet dans l'inventaire
    }

    public void ShowPhoto()
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
        photoDisplayArea.sprite = photoSprite; // Affiche la photo dans la zone d'affichage
        photoFrame.SetActive(true); // Active le cadre photo
        StartCoroutine(CameraFlashEfect()); // D�marre l'effet de flash de la cam�ra
        fadingAnimation.Play("PhotoFade"); // Joue l'animation de fondu
    }

    bool IsNPCInScreenshot()
    {
        // Lance un rayon depuis le centre de l'�cran
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, npcLayerMask))
        {
            // V�rifie si l'objet touch� par le rayon est tagu� comme NPC
            if (hit.collider.CompareTag("NPC"))
            {
                return true; // Un NPC est pr�sent dans la capture d'�cran
            }
        }

        return false; // Aucun NPC dans la capture d'�cran
    }

    IEnumerator CameraFlashEfect()
    {
        cameraFlash.SetActive(true); // Active l'effet de flash
        yield return new WaitForSeconds(flashTime); // Attend la dur�e du flash
        cameraFlash.SetActive(false); // D�sactive l'effet de flash
    }

    void RemovePhoto()
    {
        viewingPhoto = false; // Indique que la photo n'est plus affich�e
        photoFrame.SetActive(false); // D�sactive le cadre photo
    }
}
