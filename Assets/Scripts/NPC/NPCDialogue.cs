using UnityEngine;

public enum InterationType
{
    Quest,
    Shop
}

[CreateAssetMenu(menuName = "NPC Dialogue")]
public class NPCDialogue : ScriptableObject
{
    [Header("Info")]
    public string Name;
    public Sprite Icon; // Icon of NPC.

    [Header("Interaction")]
    public bool HasInteraction;
    public InterationType InterationType;

    [Header("Dialogue")]
    public string Greeting;
    [TextArea]public string[] Dialogue;
}
