using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{
    // We can fire this event anytime the player interacts with an NPC
    // that has an interaction type.
    public static event Action<InteractionType> OnExtraInteractionEvent;

    [Header("Config")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Image npcIcon;
    [SerializeField] private TextMeshProUGUI npcNameTMP;
    [SerializeField] private TextMeshProUGUI npcDialogueTMP;

    public NPCInteraction NPCSelected { get; set; }

    private bool dialogueStarted;
    private PlayerActions actions;
    private Queue<string> dialogueQueue = new Queue<string>();

    protected override void Awake()
    {
        base.Awake();
        actions = new PlayerActions();
    }

    private void Start()
    {
        // This is mapped to our Player Actions mapping
        // Interact here is "E", continue is "Enter"
        actions.Dialogue.Interact.performed += ctx => ShowDialogue();
        actions.Dialogue.Continue.performed += ctx => ContinueDialogue();
    }

    // Hide the dialogue panel, and reset the dialogue started
    // value.  Lastly, clear the queued sentences.
    public void CloseDialoguePanel()
    {
        dialoguePanel.SetActive(false);
        dialogueStarted = false;
        dialogueQueue.Clear();
    }

    // TODO: Revisit how this is working.
    private void LoadDialogueFromNPC()
    {
        // Check if this NPC has dialogue to show.
        if (NPCSelected.DialogueToShow.Dialogue.Length <= 0) return;

        foreach (string sentence in NPCSelected.DialogueToShow.Dialogue)
        {
            dialogueQueue.Enqueue(sentence);
        }
    }

    // Fires when pressing "E", this indicates we're starting
    // a convo with our NPC.
    private void ShowDialogue()
    {
        if (NPCSelected == null) return;
        if (dialogueStarted) return;

        dialoguePanel.SetActive(true);
        LoadDialogueFromNPC();

        npcIcon.sprite = NPCSelected.DialogueToShow.Icon;
        npcNameTMP.text = NPCSelected.DialogueToShow.Name;
        npcDialogueTMP.text = NPCSelected.DialogueToShow.Greeting;
        dialogueStarted = true;
    }

    // This will fire after "continuing" the convo with our NPC.
    private void ContinueDialogue()
    {
        // If we no longer have an NPCselected, clear dialogue queue.
        if (NPCSelected == null)
        {
            dialogueQueue.Clear();
            return;
        }

        // We're out of dialog, close panel.
        if (dialogueQueue.Count <= 0)
        {
            CloseDialoguePanel();
            dialogueStarted = false;
            if (NPCSelected.DialogueToShow.HasInteraction)
            {
                // If this npc has an interaction type, fire this event and pass in the
                // type.
                OnExtraInteractionEvent?.Invoke(NPCSelected.DialogueToShow.InteractionType);
            }
            return;
        }

        // Get next sentence
        npcDialogueTMP.text = dialogueQueue.Dequeue();
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }
}
