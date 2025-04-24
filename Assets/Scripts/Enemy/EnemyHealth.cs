using System;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

// Note, this enemy will take damage - so we're extending IDamageable
public class EnemyHealth : MonoBehaviour, IDamageable
{
    public static event Action OnEnemyDeadEvent;

    [Header("Config")]
    [SerializeField] private float health;

    [Header("Health Bar")]
    [SerializeField] private GameObject enemyHealthBar;
    [SerializeField] private Image healthBar;
    private EnemyBrain enemyBrain;
    private EnemySelector enemySelector;
    private EnemyLoot enemyLoot;

    public float CurrentHealth { get; set; }

    private Animator animator;
    private Rigidbody2D rb2D;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
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

    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        //Debug.Log($"{CurrentHealth} {health}");
        healthBar.fillAmount = Mathf.Lerp(
        healthBar.fillAmount,
            CurrentHealth / health,
            10f * Time.deltaTime
        );
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        DamageManager.Instance.ShowDamageText(amount, transform);

        if (CurrentHealth <= 0) 
        {
            DisableEnemy();
            // Very specific implementation of progressing a quest.
            // I think dispatching an enemy death event would
            // make more sense.
            QuestManager.Instance.AddProgress("Kill2Enemy", 1);
            QuestManager.Instance.AddProgress("Kill5Enemy", 1);
        }
    }

    private void DisableEnemy()
    {
        // Trigger Dead variable trigger when below or = 0 HP
        animator.SetTrigger("Dead");
        // Disable our enemyBrain script so enemy stops moving etc.
        enemyBrain.enabled = false;
        // Deselect the enemy because it has died (or emit callback, not necessary JUST deselect)
        enemySelector.NoSelectionCallback();
        // Updating the enemys layer to "Ignore Raycast".  This will
        // stop projectiles from colliding with it.
        // UPDATE: This was deprecated in favor of modifying our
        // enemies rigid body type.  This allows us to still detect
        // enemy clicked for looting, while avoiding projectile collisions.
        // gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        rb2D.bodyType = RigidbodyType2D.Static;
        // Fire enemy dead event.
        OnEnemyDeadEvent?.Invoke();

        // Use gameManager singleton to grant player exp based on enemy exp drop.
        GameManager.Instance.AddPlayerExp(enemyLoot.ExpDrop);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CurrentHealth <= 0) return;
        // If it has a tag of "Player" we set the hp bar as active.
        if (collision.CompareTag("Player"))
        {
            enemyHealthBar.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If it has a tag of "Player" we can hide health bar.
        if (collision.CompareTag("Player"))
        {
            enemyHealthBar.SetActive(false);
        }
    }
}
