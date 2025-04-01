using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [Header("Config")]
    [SerializeField] private int inventorySize;
    [SerializeField] private InventoryItem[] inventoryItems;

    [Header("Testing")]
    public InventoryItem testItem;

    public int InventorySize => inventorySize;

    private void Start()
    {
        inventoryItems = new InventoryItem[inventorySize];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            inventoryItems[0] = testItem.CopyItem();
            inventoryItems[0].Quantity = 10;
            InventoryUI.Instance.DrawItem(inventoryItems[0], 0);
        }
    }
}
