using UnityEngine;

public class LootManager : Singleton<LootManager>
{
    [Header("Config")]
    [SerializeField] private GameObject lootPanel;
    [SerializeField] private LootButton lootButtonPrefab;
    [SerializeField] private Transform container;

    public void ShowLoot(EnemyLoot enemyLoot)
    {
        // Show the panel
        lootPanel.SetActive(true);
        
        // If the loot panel already has items in it we need to destroy
        // them so that we can rebuild it with the new items.
        if (LootPanelContainsItems())
        {
            // Destroy all of the existing loot buttons in the loot container.
            for (int i = 0; i < container.childCount; i ++)
            {
                Destroy(container.GetChild(i).gameObject);
            }
        }

        foreach (DropItem item in enemyLoot.Items)
        {
            // If the item has already been looted, continue.
            if (item.ItemPickedUp) continue;
            // Otherwise, build a loot button prefab inside of
            // the loot container.
            LootButton lootButton = Instantiate(lootButtonPrefab, container);

            // Handles actually updating the item, icon, quantity, etc
            // for the loot button we just created
            lootButton.ConfigLootButton(item);
        }
    }

    public void CloseLootPanel()
    {
        lootPanel.SetActive(false);
    }

    private bool LootPanelContainsItems()
    {
        // This checks if the container gameObject has any children
        // game object.  So in this case, if there are any loot
        // buttons in the container.
        return container.childCount > 0;
    }
}
