using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    private PlayerAnimations playerAnimations;

    private void Awake()
    {
        playerAnimations = GetComponent<PlayerAnimations>();
    }

    private void Update()
    {
        if (stats.Health <= 0f)
        {
            PlayerDead();
        }
    }

    public void TakeDamage(float amount)
    {
        // Don't take damage if player doesn't have HP.
        if (!PlayerHasHealth()) return;

        stats.Health -= amount;

        // Here we call our damage mangager to show
        // the amount of damage being taken, and
        // pass in the player's transform location.
        DamageManager.Instance.ShowDamageText(amount, transform);

        if (stats.Health <= 0 )
        {
            PlayerDead();
        }
    }

    public bool PlayerHasHealth()
    {
        return stats.Health > 0f;
    }

    public void RestoreHealth(float amount)
    {
        stats.Health += amount;

        if (stats.Health > stats.MaxHealth) stats.Health = stats.MaxHealth;
    }

    // Returns whether or not the player is missing health, but not dead.
    public bool IsInjured()
    {
        return stats.Health > 0 && stats.Health < stats.MaxHealth;
    }

    private void PlayerDead()
    {
        playerAnimations.SetDeadAnimation();
    }
}
