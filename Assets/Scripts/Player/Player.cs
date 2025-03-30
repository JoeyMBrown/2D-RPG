using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    public PlayerStats Stats => stats;

    [Header("Test")]
    public ItemHealthPotion HealthPotion;

    public PlayerMana PlayerMana { get; private set; }

    public PlayerHealth PlayerHealth { get; private set; }

    private PlayerAnimations animations;

    private void Awake()
    {
        PlayerMana = GetComponent<PlayerMana>();
        PlayerHealth = GetComponent<PlayerHealth>();
        animations = GetComponent<PlayerAnimations>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (HealthPotion.UseItem())
            {
                Debug.Log("Using Health Potion");
            } else
            {
                Debug.Log("Unable to use Health Potion.");
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
