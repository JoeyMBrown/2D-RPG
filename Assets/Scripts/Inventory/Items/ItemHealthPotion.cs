using UnityEngine;

[CreateAssetMenu(fileName = "ItemHealthPotion", menuName = "Items/Health Potion")]
public class ItemHealthPotion : InventoryItem
{
    [Header("Config")]
    public float HealthValue;

    // Check if the player is injured, if they are - use the item
    // then return true so we can destroy 1 health potion
    public override bool UseItem()
    {
        if (GameManager.instance.Player.PlayerHealth.IsInjured())
        {
            GameManager.instance.Player.PlayerHealth.RestoreHealth(HealthValue);
            Debug.Log(HealthValue.ToString());
            return true;
        }

        return false;
    }

    public override void RemoveItem()
    {
        base.RemoveItem();
    }
}
