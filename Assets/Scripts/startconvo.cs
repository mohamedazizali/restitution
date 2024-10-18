using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.Video;
using UnityEngine.UI;

public class startconvo : MonoBehaviour
{
    [SerializeField] private NPCConversation myConv;
    [SerializeField] private NPCConversation myConv2;
    [SerializeField] private NPCConversation myConv3;
    [SerializeField] private NPCConversation MissionCompletedDialogue;
    [SerializeField] private NPCConversation MissionFailedDialogue;
    [SerializeField] public bool Quest;
    public QuestManager QuestManager;
    public bool quest1completed;
    public bool quest2completed;
    public QuestUIManager Quests;
    private bool inRange = false;
    [SerializeField] private InventoryManager inventory;

    [Header("Quest SO")]
    public Quest backpack1, backpack2, Polaroid1, Polaroid2, Solar1;
    public GameObject mainMenuCanvas;
    public GameObject puzzleCanvas;
    private bool isMainMenuActive = true; // Suit l'état actuel
    public PuzzleManager PuzzleManager;

    [Header("Cutscene")]
    public bool FirstConver = true;
    public GameObject PlayerCamera, CutsceneCamera;
    private VideoPlayer videoPlayer;
    public GameObject CutsceneCanvas; // Référence au canvas de la cinématique.
    public Button skipButton; // Référence au bouton de saut de la cinématique.
    [SerializeField] private MouseController _mousecontroller;

    public bool quest2 = true;
    /// <summary>
    /// Appelé lorsque le joueur entre dans la zone de déclenchement.
    /// </summary>
    /// <param name="other">Le collider du joueur.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            Debug.Log("enter");
        }
    }

    private void Start()
    {
        videoPlayer = CutsceneCamera.GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += EndReached;
    }

    /// <summary>
    /// Active la quête lorsque cette méthode est appelée.
    /// </summary>
    public void setActiveQest()
    {
        Quest = true;
    }

    /// <summary>
    /// Appelé lorsque le joueur quitte la zone de déclenchement.
    /// </summary>
    /// <param name="other">Le collider du joueur.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    private void Update()
    {
        quest2 = inventory.HasAtLeastThreePictures();
        // Exemple de condition pour basculer le canvas (à remplacer par votre propre condition)
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleCanvas();
        }

        if (inRange && Input.GetKeyUp(KeyCode.E))
        {
            if (FirstConver)
            {
                ToggleCutScene();
            }
            else
            {
                if (QuestManager != null)
                {
                    quest1completed = QuestManager.quest1Completed; // Obtient la valeur de la quête 1
                    quest2completed = QuestManager.quest2Completed; // Obtient la valeur de la quête 2
                }
                else
                {
                    Debug.LogWarning("Référence à QuestManager non définie.");
                }

                // Gestion des dialogues en fonction des quêtes et de l'état du puzzle
                if (!quest1completed && !quest2completed )
                {
                    ConversationManager.Instance.StartConversation(myConv);
                }
                else if (quest1completed && !quest2completed )
                {
                    ConversationManager.Instance.StartConversation(myConv2);
                }
                else if (quest1completed && quest2completed || quest2 )
                {
                    ConversationManager.Instance.StartConversation(myConv3);
                }
                else
                {
                    ConversationManager.Instance.StartConversation(myConv3);
                }
            }

            // Gestion de l'inventaire si la quête 1 est terminée
            if (quest1completed)
            {
                if (InventoryManager.Instance.HasItem("backpack"))
                {
                    InventoryManager.Instance.RemoveWithName("backpack");
                    InventoryManager.Instance.ListItems();
                }
            }
        }
    }

    /// <summary>
    /// Bascule entre le menu principal et le canvas du puzzle.
    /// </summary>
    public void ToggleCanvas()
    {
        isMainMenuActive = !isMainMenuActive; // Bascule l'état
        mainMenuCanvas.SetActive(!mainMenuCanvas.activeSelf);
        puzzleCanvas.SetActive(!puzzleCanvas.activeSelf);
    }

    /// <summary>
    /// Appelé lorsque le puzzle est complété.
    /// </summary>
    public void OnPuzzleCompleted()
    {
        ConversationManager.Instance.StartConversation(MissionCompletedDialogue);
    }

    /// <summary>
    /// Active la cinématique et désactive la caméra du joueur.
    /// </summary>
    public void ToggleCutScene()
    {
        FirstConver = false;
        CutsceneCanvas.SetActive(true);
        PlayerCamera.SetActive(false);
        CutsceneCamera.SetActive(true);
        ToggleCanvas();

        skipButton.onClick.AddListener(SkipCutScene);
        _mousecontroller.EnableMouse();

        if (videoPlayer != null)
        {
            videoPlayer.Play();
        }
    }

    /// <summary>
    /// Permet de passer la cinématique en cours.
    /// </summary>
    public void SkipCutScene()
    {
        if (videoPlayer != null)
        {
            // Option 1 : Mettre la vidéo en pause
            videoPlayer.Pause();

            // Option 2 : Aller à la fin de la vidéo pour la passer
            videoPlayer.time = videoPlayer.length;
        }

        // Bascule vers la caméra du joueur
        CutsceneCamera.SetActive(false);
        PlayerCamera.SetActive(true);
        CutsceneCanvas.SetActive(false);

        skipButton.onClick.RemoveListener(SkipCutScene); // Supprime le listener pour éviter les duplications

        ToggleCanvas();
        // Optionnel : Désactive le bouton de saut
        skipButton.gameObject.SetActive(false);
        _mousecontroller.DisableMouse();

    }

    /// <summary>
    /// Appelé lorsque la vidéo de la cinématique se termine.
    /// </summary>
    /// <param name="vp">Le lecteur vidéo qui a terminé la lecture.</param>
    private void EndReached(VideoPlayer vp)
    {
        PlayerCamera.SetActive(true);
        CutsceneCamera.SetActive(false);
        ToggleCanvas();
        CutsceneCanvas.SetActive(false);
    }

    /// <summary>
    /// Appelé lorsque le puzzle échoue.
    /// </summary>
    public void OnPuzzleFailed()
    {
        ConversationManager.Instance.StartConversation(MissionFailedDialogue);
    }

    /// <summary>
    /// Méthode pour compléter la quête 2.
    /// </summary>
    public void completequest2()
    {
        quest2completed = true;
    }
}
