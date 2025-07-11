using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestCardPlayer : QuestCard
{
    [Header("Config")]
    [SerializeField] private TextMeshProUGUI statusTMP;
    [SerializeField] private TextMeshProUGUI goldRewardTMP;
    [SerializeField] private TextMeshProUGUI expRewardTMP;

    [Header("Item")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemQuantityTMP;

    [Header("Quest Completed")]
    [SerializeField] private GameObject claimButton;
    [SerializeField] private GameObject rewardsPanel;

    private void Update()
    {
        statusTMP.text = $"Status\n{QuestToComplete.CurrentStatus}/{QuestToComplete.QuestGoal}";
    }
    public override void ConfigQuestUI(Quest quest)
    {
        base.ConfigQuestUI(quest);
        statusTMP.text = $"Status\n{quest.CurrentStatus}/{quest.QuestGoal}";
        goldRewardTMP.text = quest.GoldReward.ToString();
        expRewardTMP.text = quest.ExpReward.ToString();

        itemIcon.sprite = quest.ItemReward.Item.Icon;
        itemQuantityTMP.text = quest.ItemReward.Quantity.ToString();
    }

    private void QuestCompletedCheck()
    {
        if(QuestToComplete.QuestCompleted)
        {
            claimButton.SetActive(true);
            rewardsPanel.SetActive(false);
        }
    }

    // Divvy out rewards, and then disable this quest card.
    public void ClaimQuest()
    {
        GameManager.Instance.AddPlayerExp(QuestToComplete.ExpReward);
        Inventory.Instance.AddItem(QuestToComplete.ItemReward.Item, QuestToComplete.ItemReward.Quantity);
        CoinManager.Instance.AddCoins(QuestToComplete.GoldReward);
        gameObject.SetActive(false);
    }

    // Whenever the Quest Card Player is enabled, we can check if 
    // our quest is completed or not.  If it is, we can show the
    // claim button, and hide the reward panel.
    private void OnEnable()
    {
        QuestCompletedCheck();
    }
}
