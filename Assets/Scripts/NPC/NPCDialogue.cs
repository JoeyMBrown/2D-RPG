using UnityEngine;

public enum InteractionType
{
    Quest,
    Shop,
    Crafting
}

[CreateAssetMenu(menuName = "NPC Dialogue")]
public class NPCDialogue : ScriptableObject
{
    [Header("Info")]
    public string Name;
    public Sprite Icon; // Icon of NPC.

    [Header("Interaction")]
    public bool HasInteraction;
    public InteractionType InteractionType;

    [Header("Dialogue")]
    public string Greeting;
    [TextArea]public string[] Dialogue;
}
