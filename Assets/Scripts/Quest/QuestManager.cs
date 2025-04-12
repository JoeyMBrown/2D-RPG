using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Header("Quests")]
    [SerializeField] private Quest[] quests;

    [SerializeField] private QuestCardNPC questCardNpcPrefab;
    [SerializeField] private Transform npcPanelContainer;

    private void Start()
    {
        LoadQuestsIntoNPCPanel();
    }

    // Create an npcCard for each one of our quests in our quests array
    // Set quests to complete, name and description on QuestCard panel.
    private void LoadQuestsIntoNPCPanel()
    {
        for (int i = 0; i < quests.Length; i++)
        {
            QuestCard npcCard = Instantiate(questCardNpcPrefab, npcPanelContainer);

            npcCard.ConfigQuestUI(quests[i]);
        }
    }
}
