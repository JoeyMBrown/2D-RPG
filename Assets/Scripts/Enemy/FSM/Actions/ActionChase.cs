using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ActionChase : FSMAction
{
    [Header("Config")]
    [SerializeField] private float chaseSpeed;
    private EnemyBrain enemyBrain;

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
    }
    public override void Act()
    {
        ChasePlayer();
    }
    
    private void ChasePlayer()
    {
        if (enemyBrain.Player == null) return;

        Vector3 dirToPlayer = enemyBrain.Player.position - transform.position;

        // If we're further than 1.3f away, move.
        if (dirToPlayer.magnitude >= 1.3f)
        {
            transform.Translate(dirToPlayer.normalized * (chaseSpeed * Time.deltaTime));
        }
    }
}
