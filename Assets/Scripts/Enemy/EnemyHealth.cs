using UnityEngine;

// Note, this enemy will take damage - so we're extending IDamageable
public class EnemyHealth : MonoBehaviour, IDamageable
{
    [Header("Config")]
    [SerializeField] private float health;
    private EnemyBrain enemyBrain;
    private EnemySelector enemySelector;

    public float CurrentHealth { get; set; }

    private Animator animator;

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
        animator = GetComponent<Animator>();
        enemySelector = GetComponent<EnemySelector>();
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
            // Trigger Dead variable trigger when below or = 0 HP
            animator.SetTrigger("Dead");
            // Disable our enemyBrain script so enemy stops moving etc.
            enemyBrain.enabled = false;
            // Deselect the enmy because it has died (or emit callback, not necessary JUST deselect)
            enemySelector.NoSelectionCallback();
            // Updating the enemys layer to "Ignore Raycast".  This will
            // stop projectiles from colliding with it.
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }
        else
        {
            // If the enemy is no dead, then lets show the damage it took.
            DamageManager.Instance.ShowDamageText(amount, transform);
        }
    }
}
