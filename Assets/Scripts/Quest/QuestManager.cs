using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    [Header("Quests")]
    [SerializeField] private Quest[] quests;

    [Header("NPC Quest Panel")]
    [SerializeField] private QuestCardNPC questCardNpcPrefab;
    [SerializeField] private Transform npcPanelContainer;

    [Header("Player Quest Panel")]
    [SerializeField] private QuestCardPlayer questCardPlayerPrefab;
    [SerializeField] private Transform playerQuestContainer;

    private void Start()
    {
        LoadQuestsIntoNPCPanel();
    }

    public void AcceptQuest(Quest quest)
    {
        QuestCardPlayer cardPlayer = Instantiate(questCardPlayerPrefab, playerQuestContainer);
        cardPlayer.ConfigQuestUI(quest);
    }

    private Quest QuestExists(string questID)
    {
        foreach (Quest quest in quests)
        {
            if (quest.ID == questID)
            {
                return quest;
            }
        }

        return null;
    }

    public void AddProgress(string questID, int amount)
    {
        Quest quest = QuestExists(questID);

        if (quest == null) return;

        if (quest.QuestAccepted)
        {
            quest.AddProgress(amount);
        }
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

    // On startup we're going to reset all quests.
    private void OnEnable()
    {
        for (int i = 0; i < quests.Length; i ++)
        {
            quests[i].ResetQuest();
        }
    }
}
