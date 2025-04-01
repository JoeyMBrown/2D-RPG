using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : Singleton<InventoryUI>
{
    [Header("Config")]
    [SerializeField] private InventorySlot slotPrefab;
    [SerializeField] private Transform container;

    private List<InventorySlot> slotList = new List<InventorySlot>();

    private void Start()
    {
        InitInventory();
    }
    private void InitInventory()
    {
        // Here we will create our slots that will live inside of the container
        // UI, we're using a prefab and are building InventorySize amount's of that
        // prefab within our inventory container, and assigning it an explicity index.
        for (int i = 0; i < Inventory.Instance.InventorySize; i++)
        {
            InventorySlot slot = Instantiate(slotPrefab, container);
            slot.Index = i;

            slotList.Add(slot);
        }
    }

    // Here we update a specific slot's item image and
    // item quantity using UpdateSlot, we then toggle
    // the image and quantity UI to show.
    public void DrawItem(InventoryItem item, int index)
    {
        InventorySlot slot = slotList[index];
        slot.UpdateSlot(item);
        slot.ShowSlotInformation(true);
    }
}
