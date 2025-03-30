using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    public float CurrentMana => stats.Mana;
  
    // Initiate CurrentMana to our actual mana on game starts
    private void Start()
    {
        ResetMana();
    }

    public void UseMana(float amount)
    {
        // Update CurrentMana whenever we update our stats.
        // Will check for mana whenever we're attacking, not here.
        stats.Mana = Mathf.Max(stats.Mana -= amount, 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            UseMana(1f);
        }
    }

    public void RestoreMana(float amount)
    {
        stats.Mana += amount;

        // Set to either current mana, or maximum, whichever
        // is smaller.
        stats.Mana = Mathf.Min(stats.Mana, stats.MaxMana);
    }

    public bool IsMissingMana()
    {
        return stats.Mana >= 0 && stats.Mana < stats.MaxMana;
    }

    public void ResetMana ()
    {
        stats.Mana = stats.MaxMana;
    }
}
