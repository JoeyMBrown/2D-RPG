using UnityEngine;

[CreateAssetMenu(fileName = "ItemManaPotion", menuName = "Items/Mana Potion")]
public class ItemManaPotion : InventoryItem
{
    [Header("Config")]
    public float ManaValue;

    public override bool UseItem()
    {
        // if player is missing mana, we can use this item to restore
        // mana amount, else we can't.
        if (GameManager.instance.Player.PlayerMana.IsMissingMana())
        {
            GameManager.instance.Player.PlayerMana.RestoreMana(ManaValue);
            return true;
        }

        return false;
    }
}
