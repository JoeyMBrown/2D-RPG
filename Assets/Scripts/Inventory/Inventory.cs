using BayatGames.SaveGameFree;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [Header("Config")]
    [SerializeField] private GameContent gameContent;
    [SerializeField] private int inventorySize;
    [SerializeField] private InventoryItem[] inventoryItems;

    [Header("Testing")]
    public InventoryItem testItem;

    public int InventorySize => inventorySize;

    public InventoryItem[] InventoryItems => inventoryItems;

    private readonly string INVENTORY_KEY_DATA = "MY_INVENTORY";

    private void Start()
    {
        inventoryItems = new InventoryItem[inventorySize];
        VerifyItemsForDraw();
        LoadInventory();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            AddItem(testItem, 1);
        }
    }

    public void UseItem(int index)
    {
        if (inventoryItems[index] == null) return;

        // If we were able to use our item, decrease quantity
        // by 1.
        if (inventoryItems[index].UseItem())
        {
            DecreaseItemQuantity(index);
            SaveInventory();
        }
    }

    // Remove reference of object, then redraw the
    // UI, save inventory.
    public void RemoveItem(int index)
    {
        if (inventoryItems[index] == null) return;

        // Call the individual item's RemoveItem method.
        inventoryItems[index].RemoveItem();
        inventoryItems[index] = null;
        InventoryUI.Instance.DrawItem(null, index);

        SaveInventory();
    }

    public void EquipItem(int index)
    {
        if (inventoryItems[index] == null) return;
        if (inventoryItems[index].ItemType != ItemType.Weapon) return;

        inventoryItems[index].EquipItem();
    }

    // This method looks for a free slot and adds the given item to that slot.
    private void AddItemFreeSlot(InventoryItem item, int quantity)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            // If there is an item in the current slot continue.
            if (inventoryItems[i] != null) continue;

            // If the slot is free, copy the passed in item and it's qty
            // to this inventory slot.
            inventoryItems[i] = item.CopyItem();
            inventoryItems[i].Quantity = quantity;

            // Update the UI to draw the inventory item within the UI
            // slot.
            InventoryUI.Instance.DrawItem(inventoryItems[i], i);
            return;
        }
    }

    public void AddItem(InventoryItem item, int quantity)
    {
        if (item == null || quantity <= 0) return;

        // Check if we have other slots containing this item.
        List<int> itemIndexes = CheckItemStockIndexes(item.ID);

        // If item is stackable, and we have other slots containing
        // this tiem.
        if (item.IsStackable && itemIndexes.Count > 0)
        {
            int maxStack = item.MaxStack;

            // Iterate through other slots containing this item.
            foreach (int index in itemIndexes)
            {
                // If they're not at max stack.
                if (inventoryItems[index].Quantity < maxStack)
                {
                    // Add our incoming quantity to them.
                    inventoryItems[index].Quantity += quantity;

                    // If they're now over the max stack, find by how much.
                    // Reset them to max stack, and recall this method with
                    // the left over quantity.
                    if (inventoryItems[index].Quantity > maxStack)
                    {
                        int dif = inventoryItems[index].Quantity - maxStack;
                        inventoryItems[index].Quantity = maxStack;
                        AddItem(item, dif);
                    }

                    // Draw the item within the slot at the given index.
                    InventoryUI.Instance.DrawItem(inventoryItems[index], index);

                    SaveInventory();
                    return;
                }
            }
        }

        // This is only ran if item is not stackable, OR if we don't have this
        // item in our inventory in an existing slot.
        int quantityToAdd = quantity > item.MaxStack ? item.MaxStack : quantity;

        // Add the item to a free slot, either in the form of maxStack qty,
        // or the qty specificed if it is lower than max stack.
        AddItemFreeSlot(item, quantityToAdd);

        // If over maxStack amnt tried to be add,
        // lets go ahead and add the final remained to an open slot.
        int remainingAmount = quantity - quantityToAdd;
        if (remainingAmount > 0)
        {
            AddItemFreeSlot(item, remainingAmount);
        }

        SaveInventory();
    }

    // Here we can decrease an item's quantity, and if
    // we detect that the quantity is 0 or less than 0
    // we can remove the item from our inventory
    // while also calling the draw method which will
    // visually remove it from our inventory.
    private void DecreaseItemQuantity(int index)
    {
        InventoryItem item = inventoryItems[index];

        item.Quantity--;

        if (item.Quantity <= 0)
        {
            item = null;
            InventoryUI.Instance.DrawItem(null, index);
        }
        else
        {
            // Update drawn UI to reflect to quantity.
            InventoryUI.Instance.DrawItem(item, index);
        }
    }

    public void ConsumeItem(string itemID)
    {
        // Get the inventory indexes of the specific item.
        List<int> indexes = CheckItemStockIndexes(itemID);

        // If Item exists within our inventory.
        if (indexes.Count > 0)
        {
            // Decrese from the largest index to the smallest.
            DecreaseItemQuantity(indexes[^1]);
        }
    }

    // This method will return all inventory slots containing
    // the same item as the item passed in.
    private List<int> CheckItemStockIndexes(string itemID)
    {
        List<int> itemIndexes = new List<int>();

        for (int i = 0; i < inventoryItems.Length; i++)
        {
            // If inventory slot is empty continue.
            if (inventoryItems[i] == null) continue;

            if (inventoryItems[i].ID == itemID)
            {
                itemIndexes.Add(i);
            }
        }

        return itemIndexes;
    }

    public int GetItemCurrentQuantityInInventory(string itemID)
    {
        List<int> indexes = CheckItemStockIndexes(itemID);

        int currentQuantity = 0;

        foreach (int index in indexes)
        {
            currentQuantity += inventoryItems[index].Quantity;
        }

        return currentQuantity;
    }

    // Hide slot information if (sprite / quantity text) for all
    // slots that do not contain an item
    private void VerifyItemsForDraw()
    {
        for (int i = 0; i < inventorySize; i++)
        {

            if (inventoryItems[i] == null)
            {
                InventoryUI.Instance.DrawItem(null, i);
            }
        }
    }

    private InventoryItem ItemExistsInGameContent (string itemID)
    {
        for (int i = 0; i < gameContent.GameItems.Length; i++)
        {
            if (gameContent.GameItems[i].ID == itemID)
            {
                return gameContent.GameItems[i];
            }
        }

        return null;
    }

    private void LoadInventory()
    {
        // check to see if there is existing data saved under
        // this key.
        if (SaveGame.Exists(INVENTORY_KEY_DATA))
        {
            // Save data stored with the provided key into this variable.
            InventoryData loadData = SaveGame.Load<InventoryData>(INVENTORY_KEY_DATA);

            for (int i = 0; i < inventorySize; i ++)
            {
                // Loop over saved data
                if (loadData.ItemContent[i] != null)
                {
                    // If current index is an item, verify that it exists as an item within our game.
                    InventoryItem itemFromContent = ItemExistsInGameContent(loadData.ItemContent[i]);
                    // If item exists, create an isntance of it inside of our inventory
                    // at the current position, and update the quantity.  Then
                    // We'll draw the item's image in quantity in the inventory tile.
                    if (itemFromContent != null)
                    {
                        inventoryItems[i] = itemFromContent.CopyItem();
                        inventoryItems[i].Quantity = loadData.ItemQuantity[i];
                        InventoryUI.Instance.DrawItem(inventoryItems[i], i);
                    }
                }
                else
                {
                    // We don't have anything in this index.
                    inventoryItems[i] = null;
                }
            }
        }
    }

    private void SaveInventory()
    {
        InventoryData saveData = new InventoryData();

        // Create the itemContent and itemQuantity array with a size of inventorySize
        saveData.ItemContent = new string[inventorySize];
        saveData.ItemQuantity = new int[inventorySize];

        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] == null)
            {
                saveData.ItemContent[i] = null;
                saveData.ItemQuantity[i] = 0;
            }
            else
            {
                saveData.ItemContent[i] = inventoryItems[i].ID;
                saveData.ItemQuantity[i] = (int)inventoryItems[i].Quantity;
            }
        }

        // Use package to store inventory IDs and Quantities with the specified key
        SaveGame.Save(INVENTORY_KEY_DATA, saveData);
    }
}
