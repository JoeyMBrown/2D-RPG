using UnityEngine;

public class LootManager : Singleton<LootManager>
{
    [Header("Config")]
    [SerializeField] private GameObject lootPanel;
    [SerializeField] private LootButton lootButtonPrefab;
    [SerializeField] private Transform container;

    private void OnEnable()
    {
        LootButton.ItemPickedUpEvent += AutoClosePanel;
    }

    private void OnDisable()
    {
        LootButton.ItemPickedUpEvent -= AutoClosePanel;
    }

    public void UpdateLootPanelItems(EnemyLoot enemyLoot)
    {
        // If the loot panel already has items in it we need to destroy
        // them so that we can rebuild it with the new items.
        if (LootPanelContainsItems())
        {
            // Destroy all of the existing loot buttons in the loot container.
            for (int i = 0; i < container.childCount; i++)
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

        ShowLoot();
    }

    /*
     * This method exists to auto close loot panel
     * after last item has been pickedup.
     */
    public void AutoClosePanel()
    {
        // Here we're doing childCount - 1 because destroy of
        // game object introduces a potential race condition
        // where we're checking childcount before it's updated.
        if (lootPanel.activeInHierarchy && container.childCount - 1 <= 0) CloseLootPanel();
    }

    public void ShowLoot()
    {
        if (!LootPanelContainsItems()) return;

        // Show the panel
        lootPanel.SetActive(true);
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
