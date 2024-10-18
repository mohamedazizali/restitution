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
    private bool isMainMenuActive = true; // Suit l'�tat actuel
    public PuzzleManager PuzzleManager;

    [Header("Cutscene")]
    public bool FirstConver = true;
    public GameObject PlayerCamera, CutsceneCamera;
    private VideoPlayer videoPlayer;
    public GameObject CutsceneCanvas; // R�f�rence au canvas de la cin�matique.
    public Button skipButton; // R�f�rence au bouton de saut de la cin�matique.
    [SerializeField] private MouseController _mousecontroller;

    public bool quest2 = true;
    /// <summary>
    /// Appel� lorsque le joueur entre dans la zone de d�clenchement.
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
    /// Active la qu�te lorsque cette m�thode est appel�e.
    /// </summary>
    public void setActiveQest()
    {
        Quest = true;
    }

    /// <summary>
    /// Appel� lorsque le joueur quitte la zone de d�clenchement.
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
        // Exemple de condition pour basculer le canvas (� remplacer par votre propre condition)
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
                    quest1completed = QuestManager.quest1Completed; // Obtient la valeur de la qu�te 1
                    quest2completed = QuestManager.quest2Completed; // Obtient la valeur de la qu�te 2
                }
                else
                {
                    Debug.LogWarning("R�f�rence � QuestManager non d�finie.");
                }

                // Gestion des dialogues en fonction des qu�tes et de l'�tat du puzzle
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

            // Gestion de l'inventaire si la qu�te 1 est termin�e
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
        isMainMenuActive = !isMainMenuActive; // Bascule l'�tat
        mainMenuCanvas.SetActive(!mainMenuCanvas.activeSelf);
        puzzleCanvas.SetActive(!puzzleCanvas.activeSelf);
    }

    /// <summary>
    /// Appel� lorsque le puzzle est compl�t�.
    /// </summary>
    public void OnPuzzleCompleted()
    {
        ConversationManager.Instance.StartConversation(MissionCompletedDialogue);
    }

    /// <summary>
    /// Active la cin�matique et d�sactive la cam�ra du joueur.
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
    /// Permet de passer la cin�matique en cours.
    /// </summary>
    public void SkipCutScene()
    {
        if (videoPlayer != null)
        {
            // Option 1 : Mettre la vid�o en pause
            videoPlayer.Pause();

            // Option 2 : Aller � la fin de la vid�o pour la passer
            videoPlayer.time = videoPlayer.length;
        }

        // Bascule vers la cam�ra du joueur
        CutsceneCamera.SetActive(false);
        PlayerCamera.SetActive(true);
        CutsceneCanvas.SetActive(false);

        skipButton.onClick.RemoveListener(SkipCutScene); // Supprime le listener pour �viter les duplications

        ToggleCanvas();
        // Optionnel : D�sactive le bouton de saut
        skipButton.gameObject.SetActive(false);
        _mousecontroller.DisableMouse();

    }

    /// <summary>
    /// Appel� lorsque la vid�o de la cin�matique se termine.
    /// </summary>
    /// <param name="vp">Le lecteur vid�o qui a termin� la lecture.</param>
    private void EndReached(VideoPlayer vp)
    {
        PlayerCamera.SetActive(true);
        CutsceneCamera.SetActive(false);
        ToggleCanvas();
        CutsceneCanvas.SetActive(false);
    }

    /// <summary>
    /// Appel� lorsque le puzzle �choue.
    /// </summary>
    public void OnPuzzleFailed()
    {
        ConversationManager.Instance.StartConversation(MissionFailedDialogue);
    }

    /// <summary>
    /// M�thode pour compl�ter la qu�te 2.
    /// </summary>
    public void completequest2()
    {
        quest2completed = true;
    }
}
