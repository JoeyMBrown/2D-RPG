using System;
using UnityEngine;

// Note, this enemy will take damage - so we're extending IDamageable
public class EnemyHealth : MonoBehaviour, IDamageable
{
    public static event Action OnEnemyDeadEvent;

    [Header("Config")]
    [SerializeField] private float health;
    private EnemyBrain enemyBrain;
    private EnemySelector enemySelector;
    private EnemyLoot enemyLoot;

    public float CurrentHealth { get; set; }

    private Animator animator;

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
        animator = GetComponent<Animator>();
        enemySelector = GetComponent<EnemySelector>();
        enemyLoot = GetComponent<EnemyLoot>();
    }

    // Intialize health on game start.
    private void Start()
    {
        CurrentHealth = health;
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;

        if (CurrentHealth <= 0) 
        {
            DisableEnemy();
        }
        else
        {
            // If the enemy is no dead, then lets show the damage it took.
            DamageManager.Instance.ShowDamageText(amount, transform);
        }
    }

    private void DisableEnemy()
    {
        // Trigger Dead variable trigger when below or = 0 HP
        animator.SetTrigger("Dead");
        // Disable our enemyBrain script so enemy stops moving etc.
        enemyBrain.enabled = false;
        // Deselect the enmy because it has died (or emit callback, not necessary JUST deselect)
        enemySelector.NoSelectionCallback();
        // Updating the enemys layer to "Ignore Raycast".  This will
        // stop projectiles from colliding with it.
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        // Fire enemy dead event.
        OnEnemyDeadEvent?.Invoke();

        // Use gameManager singleton to grant player exp based on enemy exp drop.
        GameManager.instance.AddPlayerExp(enemyLoot.ExpDrop);
    }
}
