using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LootButton : MonoBehaviour
{
    public static event Action ItemPickedUpEvent;

    [Header("Config")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemQuantity;

    public DropItem ItemLoaded { get; private set; }

    // Handles updating this prefabs UI to display specifics
    // about the item that was dropped.
    public void ConfigLootButton(DropItem dropItem)
    {
        ItemLoaded = dropItem;
        itemIcon.sprite = dropItem.Item.Icon;
        itemName.text = dropItem.Item.Name;
        itemQuantity.text = $"x {dropItem.Quantity}";
    }

    // Check to see if we loaded an item to be dropped,
    // If we did, add it to player inv.
    public void CollectItem()
    {
        if (ItemLoaded == null) return;

        Inventory.Instance.AddItem(ItemLoaded.Item, ItemLoaded.Quantity);
        ItemLoaded.ItemPickedUp = true;
        Destroy(gameObject);
        ItemPickedUpEvent?.Invoke();
    }
}
