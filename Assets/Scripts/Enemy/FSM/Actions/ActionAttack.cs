using UnityEngine;

public class ActionAttack : FSMAction
{
    [Header("Config")]
    [SerializeField] private float damage;
    [SerializeField] private float timeBetweenAttacks;

    private EnemyBrain enemyBrain;
    private float timer;

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
    }

    public override void Act()
    {
        AttackPlayer();
    }

    private void AttackPlayer()
    {
        
        if (enemyBrain.Player == null) return;

        // We subtract current time - timer
        timer -= Time.deltaTime;

        // When timer <= 0 we attack
        if (timer <= 0)
        {
            // This is calling TakeDamage method from playerHealth class.
            PlayerHealth health = enemyBrain.Player.GetComponent<PlayerHealth>();
            if (!health.PlayerHasHealth()) return;

            health.TakeDamage(damage);

            // We reset the timer to seconds between attacks
            timer = timeBetweenAttacks;
        }
    }
}
