using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [Header("Config")]
    // Each NPC will store their own dialogue SO
    [SerializeField] private NPCDialogue dialogueToShow;
    // This is the button to press to interact,
    // it will be shown as UI text next to NPC
    [SerializeField] private GameObject interactionBox;

    public NPCDialogue DialogueToShow => dialogueToShow;

    // Whenever something enters our 2D collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If it has a tag of "Player" we can set the selected NPC
        // as this instance of NPC
        if (collision.CompareTag("Player"))
        {
            DialogueManager.Instance.NPCSelected = this;
            interactionBox.SetActive(true);
        }
    }

    // Detect player leaving our collider
    private void OnTriggerExit2D(Collider2D collision)
    {
        // If it has a tag of "Player" we can remove the NPCSelected value,
        // close the panel, and hide the interaction box.
        if (collision.CompareTag("Player"))
        {
            DialogueManager.Instance.NPCSelected = null;
            DialogueManager.Instance.CloseDialoguePanel();
            interactionBox.SetActive(false);
        }
    }
}
