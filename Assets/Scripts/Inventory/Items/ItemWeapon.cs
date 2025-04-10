using UnityEngine;

// The "/" allows this to be a sub menu selection option of "Weapon"
// inside of unity, with a starting file name of ItemWeapon.
// It will contain all the properties of it's parent, InventoryItem
// with then the added properties of this class as well.
[CreateAssetMenu(menuName = "Items/Weapon", fileName = "ItemWeapon")]
public class ItemWeapon : InventoryItem
{
    [Header("Weapon")]
    public Weapon Weapon;

    // Call equip weapon and pass in the Weapon property
    public override void EquipItem()
    {
        WeaponManager.Instance.EquipWeapon(Weapon);
    }
     
    public override bool UseItem()
    {
        return false;
    }
}
