using UnityEngine;

public enum WeaponType
{
    Magic,
    Melee
}

// This places this within the "right click" list of the unity editor
// We can now right click inside of unity and select to create a new
// Weapon scriptable object.
[CreateAssetMenu(fileName = "Weapon_")]
public class Weapon : ScriptableObject
{
    [Header("Config")]
    public Sprite Icon;
    public WeaponType WeaponType;
    public float Damage;

    [Header("Projectile")]
    // The projectile the weapon uses.
    public Projectile ProjectilePrefab;
    public float RequiredMana;
}
