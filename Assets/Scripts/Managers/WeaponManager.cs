using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : Singleton<WeaponManager>
{
    [Header("Config")]
    [SerializeField] private Image weaponIcon;
    [SerializeField] private TextMeshProUGUI weaponManaTMP;

    // Update UI sprite, then activate it on the DOM
    // Then update the required mana to use the weapon
    // with the value stored on the weapon as a property.
    public void EquipWeapon(Weapon weapon)
    {
        weaponIcon.sprite = weapon.Icon;
        // This line uses the native size of the icon instead of
        // scaling it automatically.
        weaponIcon.SetNativeSize();
        weaponIcon.gameObject.SetActive(true);
        weaponManaTMP.text = weapon.RequiredMana.ToString();
        weaponManaTMP.gameObject.SetActive(true);
        // Call equip weapon in the player attack class to actually
        // equip the weapon and adjust damage stats.
        GameManager.Instance.Player.PlayerAttack.EquipWeapon(weapon);
    }
}
