using UnityEngine;

public enum ItemType
{
    Weapon,
    Potion,
    Scroll,
    Ingredient,
    Treasure
}

[CreateAssetMenu(menuName = "Items/Item")]
public class InventoryItem : ScriptableObject
{
    [Header("Config")]
    public string ID;
    public string Name;
    public Sprite Icon;
    [TextArea] public string Description;

    [Header("Info")]
    public ItemType ItemType;
    public bool IsConsumable;
    public bool IsStackable;
    public int MaxStack;

    [HideInInspector] public int Quantity;

    public InventoryItem CopyItem()
    {
        InventoryItem instance = Instantiate(this);
        return instance;
    }

    // With these virtual method definitions, we can allow
    // childs of this class to implement these methods
    // how they see fit, virtual keyword allows for
    // override.
    public virtual bool UseItem()
    {
        return true;
    }

    public virtual void EquipItem()
    {

    }

    public virtual void RemoveItem()
    {

    }
}
