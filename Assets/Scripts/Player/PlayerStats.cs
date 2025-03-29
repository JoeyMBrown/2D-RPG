using UnityEngine;

public enum AttributeType
{
    Strength,
    Dexterity,
    Intelligence
}

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player Stats")]

public class PlayerStats : ScriptableObject
{
    [Header("Config")]
    public int Level;

    [Header("Health")]
    public float Health;
    public float MaxHealth;

    [Header("Mana")]
    public float Mana;
    public float MaxMana;

    [Header("Exp")]
    public float CurrentExp;
    // Exists to represent the exp needed to hit current level + 1.
    public float NextLevelExp;
    // Exists solely to represent the exp needed to hit level 2.
    public float InitialNextLevelExp;
    // Acts as a percentage - this x nextLevelExp will decide the
    // next nextLevelExp amount.
    [Range(1f, 100f)] public float ExpMultiplier;

    [Header("Attack")]
    public float BaseDamage;
    public float CriticalChance;
    public float CriticalDamage;

    [Header("Attributes")]
    public int Strength;
    public int Dexterity;
    public int Intelligence;
    public int AttributePoints;

    [HideInInspector] public float TotalExp;
    public float TotalDamage;

    public void ResetPlayerStats()
    {
        MaxHealth = 10;
        MaxMana = 20;
        Health = MaxHealth;
        Mana = MaxMana;
        Level = 1;
        CurrentExp = 0;
        NextLevelExp = InitialNextLevelExp;
        TotalExp = 0;
        BaseDamage = 2;
        CriticalChance = 10;
        CriticalDamage = 50;
        Strength = 0;
        Dexterity = 0;
        Intelligence = 0;
        AttributePoints = 0;
    }
}
