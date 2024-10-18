using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenCapture : MonoBehaviour
{
    [Header("Photo Taker")]
    [SerializeField] private Image photoDisplayArea; // Zone d'affichage pour la photo capturée
    [SerializeField] private GameObject photoFrame, polaroid; // Références au cadre photo et au polaroid
    float maxPictures = 0f; // Nombre maximal de photos pouvant être prises

    [Header("Flash Effect")]
    [SerializeField] private GameObject cameraFlash; // Effet de flash de la caméra
    [SerializeField] private float flashTime; // Durée du flash de la caméra

    [Header("Photo Fader Effect")]
    [SerializeField] private Animator fadingAnimation; // Animation de fondu pour la photo
    public CameraChange camchange; // Référence au script CameraChange pour vérifier le mode de caméra
    private Texture2D screenCapture; // Texture pour stocker la capture d'écran
    private bool viewingPhoto; // Indique si la photo est actuellement affichée
    [SerializeField] private bool screenshotOfNPC; // Indique si la capture d'écran contient un NPC

    [SerializeField] private InventoryManager inventoryManager2; // Référence au gestionnaire d'inventaire
    [SerializeField] private LayerMask npcLayerMask; // Masque de couche pour détecter les NPCs
    [SerializeField] private float wincon = 0f; // Condition de victoire
    [SerializeField] bool questCompleted = false; // Indique si la quête est complétée
    [SerializeField] private AudioSource camsound; // Source audio pour les sons de la caméra
    [SerializeField] private AudioClip startSound; // Son joué au début de la capture de photo

    void Start()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false); // Crée une texture pour la capture d'écran
        camsound = gameObject.GetComponent<AudioSource>(); // Obtient le composant AudioSource
    }

    private void Update()
    {
        // Vérifie si le bouton gauche de la souris est pressé, le mode caméra est correct et le nombre maximal de photos n'est pas atteint
        if (Input.GetMouseButtonDown(0) && camchange.CamMode == 1 && maxPictures < 6)
        {
            if (!viewingPhoto)
            {
                StartCoroutine(CapturePhoto()); // Démarre la coroutine pour capturer une photo
            }
            else
            {
                RemovePhoto(); // Supprime la photo affichée
            }
            if (maxPictures == 5)
            {
                photoFrame.SetActive(false); // Désactive le cadre photo lorsque le nombre maximal de photos est atteint
            }
        }
        wincon = inventoryManager2.CalculateTotalValue(); // Calcule la valeur totale des objets dans l'inventaire
        if (wincon > 2) { questCompleted = true; } // Marque la quête comme complétée si la condition est remplie
    }

    IEnumerator CapturePhoto()
    {
        if (startSound != null && camsound != null)
        {
            camsound.PlayOneShot(startSound); // Joue le son de début de capture de photo
        }
        viewingPhoto = true; // Indique que la photo est en cours d'affichage
        yield return new WaitForEndOfFrame(); // Attend la fin du cadre actuel

        // Crée une texture pour la nouvelle capture d'écran
        Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);
        Texture2D newScreenshotTexture = new Texture2D((int)regionToRead.width, (int)regionToRead.height, TextureFormat.RGB24, false);
        newScreenshotTexture.ReadPixels(regionToRead, 0, 0, false);
        newScreenshotTexture.Apply();

        float value = 0;
        screenshotOfNPC = IsNPCInScreenshot(); // Vérifie si la capture d'écran contient un NPC
        if (screenshotOfNPC) { value = 1; }
        maxPictures++; // Incrémente le compteur de photos

        // Crée un Sprite unique pour cette capture d'écran
        Sprite screenshotSprite = Sprite.Create(newScreenshotTexture, new Rect(0.0f, 0.0f, newScreenshotTexture.width, newScreenshotTexture.height), new Vector2(0.5f, 0.5f), 100.0f);

        ShowPhoto(); // Affiche la photo
        inventoryManager2.ListItems(); // Met à jour la liste des objets dans l'inventaire
        inventoryManager2.AddScreenshotItem(screenshotSprite, "Picture", value); // Ajoute la capture d'écran comme un objet dans l'inventaire
    }

    public void ShowPhoto()
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
        photoDisplayArea.sprite = photoSprite; // Affiche la photo dans la zone d'affichage
        photoFrame.SetActive(true); // Active le cadre photo
        StartCoroutine(CameraFlashEfect()); // Démarre l'effet de flash de la caméra
        fadingAnimation.Play("PhotoFade"); // Joue l'animation de fondu
    }

    bool IsNPCInScreenshot()
    {
        // Lance un rayon depuis le centre de l'écran
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, npcLayerMask))
        {
            // Vérifie si l'objet touché par le rayon est tagué comme NPC
            if (hit.collider.CompareTag("NPC"))
            {
                return true; // Un NPC est présent dans la capture d'écran
            }
        }

        return false; // Aucun NPC dans la capture d'écran
    }

    IEnumerator CameraFlashEfect()
    {
        cameraFlash.SetActive(true); // Active l'effet de flash
        yield return new WaitForSeconds(flashTime); // Attend la durée du flash
        cameraFlash.SetActive(false); // Désactive l'effet de flash
    }

    void RemovePhoto()
    {
        viewingPhoto = false; // Indique que la photo n'est plus affichée
        photoFrame.SetActive(false); // Désactive le cadre photo
    }
}
