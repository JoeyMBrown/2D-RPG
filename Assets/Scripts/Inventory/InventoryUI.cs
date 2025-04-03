using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : Singleton<InventoryUI>
{
    [Header("Config")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private InventorySlot slotPrefab;
    [SerializeField] private Transform container;

    [Header("Description Panel")]
    [SerializeField] private GameObject descriptionPanel;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemNameTMP;
    [SerializeField] private TextMeshProUGUI itemDescriptionTMP;

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
        if (CurrentSlot == null) return;

        Inventory.Instance.UseItem(CurrentSlot.Index);
    }

    public void RemoveItem()
    {
        if (CurrentSlot == null) return;

        Inventory.Instance.RemoveItem(CurrentSlot.Index);
    }

    // For equip button in inventory screen.  when clicked,
    // we will call the inventory instance's equip item method
    // and pass in reference to the current selected slot.
    public void EquipItem()
    {
        if (CurrentSlot == null) return;

        Inventory.Instance.EquipItem(CurrentSlot.Index);
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

    public void ShowItemDescription(int index)
    {
        if (Inventory.Instance.InventoryItems[index] == null) return;

        descriptionPanel.SetActive(true);
        itemIcon.sprite = Inventory.Instance.InventoryItems[index].Icon;
        itemNameTMP.text = Inventory.Instance.InventoryItems[index].Name;
        itemDescriptionTMP.text = Inventory.Instance.InventoryItems[index].Description;
    }

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);

        // If we close the inventory, we also want to close the description panel.
        if (inventoryPanel.activeSelf == false)
        {
            descriptionPanel.SetActive(false);
            CurrentSlot = null;
        }
    }

    // When slot event is distpached, set the current slot
    // to the slot that was selected, and show the item
    // description for the selected item.
    private void SlotSelectedCallback(int slotIndex)
    {
        CurrentSlot = slotList[slotIndex];
        ShowItemDescription(slotIndex);
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
