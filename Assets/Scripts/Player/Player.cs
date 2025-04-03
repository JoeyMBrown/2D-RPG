using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    public PlayerStats Stats => stats;

    [Header("Test")]
    public ItemHealthPotion HealthPotion;
    public ItemManaPotion ManaPotion;

    public PlayerMana PlayerMana { get; private set; }

    public PlayerHealth PlayerHealth { get; private set; }

    public PlayerAttack PlayerAttack { get; private set; }

    private PlayerAnimations animations;

    private void Awake()
    {
        PlayerMana = GetComponent<PlayerMana>();
        PlayerHealth = GetComponent<PlayerHealth>();
        PlayerAttack = GetComponent<PlayerAttack>();
        animations = GetComponent<PlayerAnimations>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (ManaPotion.UseItem())
            {
                Debug.Log("Using Mana Potion");
            } else
            {
                Debug.Log("Unable to use Mana Potion.");
            }
        }
    }

    // Here we can reset player stats, then call the method in our
    // animator to reset our player animation as well.
    public void ResetPlayer()
    {
        stats.ResetPlayerStats();
        animations.ResetPlayer();
        PlayerMana.ResetMana();
    }
}
