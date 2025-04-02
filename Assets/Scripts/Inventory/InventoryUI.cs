using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : Singleton<InventoryUI>
{
    [Header("Config")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private InventorySlot slotPrefab;
    [SerializeField] private Transform container;

    public InventorySlot CurrentSlot { get; private set; }

    private List<InventorySlot> slotList = new List<InventorySlot>();

    protected override void Awake()
    {
        // Call parent class awake method
        base.Awake();
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

    // Fired when "use" button in inventory screen is clicked,
    // we will try to use selected item.
    public void UseItem()
    {
        Inventory.Instance.UseItem(CurrentSlot.Index);
    }

    // Here we update a specific slot's item image and
    // item quantity using UpdateSlot, we then toggle
    // the image and quantity UI to show.  If the item
    // param is null, we will instead clear the information
    public void DrawItem(InventoryItem item, int index)
    {
        InventorySlot slot = slotList[index];

        // If the item were trying to show is null,
        // hide it and return.
        if (item == null)
        {
            slot.ShowSlotInformation(false);
            return;
        }

        slot.UpdateSlot(item);
        slot.ShowSlotInformation(true);
    }

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    // When slot event is distpached, set the current slot
    // to the slot that was selected.
    private void SlotSelectedCallback(int slotIndex)
    {
        CurrentSlot = slotList[slotIndex];
    }

    private void OnEnable()
    {
        InventorySlot.OnSlotSelectedEvent += SlotSelectedCallback;
    }

    private void OnDisable()
    {
        InventorySlot.OnSlotSelectedEvent -= SlotSelectedCallback;
    }
}
