using TMPro;
using UnityEngine;

public class QuestCardNPC : QuestCard
{
    [SerializeField] private TextMeshProUGUI questRewardTMP;

    public override void ConfigQuestUI(Quest quest)
    {
        base.ConfigQuestUI(quest);
        questRewardTMP.text =
            $"-{quest.GoldReward} Gold\n" +
            $"-{quest.ExpReward} Exp\n" +
            $"-x{quest.ItemReward.Quantity} {quest.ItemReward.Item.Name}";
    }
}
