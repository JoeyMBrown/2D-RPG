using System;
using UnityEngine;

public class PlayerUpgrade : MonoBehaviour
{
    public static event Action OnPlayerUpgradeEvent;

    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    [Header("Settings")]
    [SerializeField] private UpgradeSettings[] settings;

    private void UpgradePlayer(int upgradeIndex)
    {
        stats.BaseDamage += settings[upgradeIndex].DamageUpgrade; 
        stats.TotalDamage += settings[upgradeIndex].DamageUpgrade; 
        stats.MaxHealth += settings[upgradeIndex].HealthUpgrade;
        stats.Health = stats.MaxHealth;
        stats.MaxMana += settings[upgradeIndex].ManaUpgrade;
        stats.Mana = stats.MaxMana;
        stats.CriticalChance += settings[upgradeIndex].CriticalChanceUpgrade;
        stats.CriticalDamage += settings[upgradeIndex].CriticalDamageUpgrade;
    }

    // When attribute selected event is fired, we first check for available
    // points, we then compare the given attribute selection with our 
    // AttributeType enum class to decide which stat to upgrade.
    // We then call our upgrade player method with the correct index for the
    // stat as defined in our settings array in the Unity editor.  This
    // will properly increment our stats based on our settings.
    // Finally, we decrement our points, and invoke the playerUpgradeEvent.
    private void AttributeCallback(AttributeType attributeType)
    {
        if (stats.AttributePoints == 0) return;

        switch (attributeType)
        {
            case AttributeType.Strength:
                UpgradePlayer(0);
                stats.Strength++;
                break;
            case AttributeType.Dexterity:
                UpgradePlayer(1);
                stats.Dexterity++;
                break;
            case AttributeType.Intelligence:
                UpgradePlayer(2);
                stats.Intelligence++;
                break;
        }

        stats.AttributePoints--;
        OnPlayerUpgradeEvent?.Invoke();
    }

    // Register our event listener here
    private void OnEnable()
    {
        AttributeButton.OnAttributeSelectedEvent += AttributeCallback;
    }

    private void OnDisable()
    {
        AttributeButton.OnAttributeSelectedEvent -= AttributeCallback;
    }
}

// These are essentially the underlying stats that get
// upgraded when an attribute is chosen.
[Serializable]
public class UpgradeSettings
{
    public string Name;

    [Header("Values")]
    public float DamageUpgrade;
    public float HealthUpgrade;
    public float ManaUpgrade;
    public float CriticalChanceUpgrade;
    public float CriticalDamageUpgrade;
}
