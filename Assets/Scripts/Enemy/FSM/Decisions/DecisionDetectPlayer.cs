using UnityEngine;

public class DecisionDetectPlayer : FSMDecision
{
    [Header("Config")]
    [SerializeField] private float range;
    // This will be the player layer
    [SerializeField] private LayerMask playerMask;

    private EnemyBrain enemy;

    private void Awake()
    {
        enemy = GetComponent<EnemyBrain>();
    }

    public override bool Decide()
    {
        return DetectPlayer();
    }

    private bool DetectPlayer()
    {
        // We're trying to collide with enemy position,
        // within the given range, with the specified layer.
        Collider2D playerCollider = Physics2D.OverlapCircle(enemy.transform.position, range, playerMask);

        // We're detecting our player
        if (playerCollider != null)
        {
            enemy.Player = playerCollider.transform;
            return true;
        }

        // We're not detecting our player
        enemy.Player = null;
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        // This will draw a sphere around the enemy and
        // show their detect range.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
