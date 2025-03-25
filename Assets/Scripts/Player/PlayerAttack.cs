using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttack : MonoBehaviour
{
    private PlayerActions actions;
    private PlayerAnimations playerAnimations;
    private EnemyBrain enemyTarget;
    private Coroutine attackCoroutine;

    private void Awake()
    {
        actions = new PlayerActions();
        playerAnimations = GetComponent<PlayerAnimations>();
    }

    private void Start()
    {
        // Determine if our click atack action was performed,
        // if so, call Attack method within this context?
        actions.Attack.ClickAttack.performed += ctx => Attack();
    }

    private void Attack()
    {
        if (enemyTarget == null) return;

        // If we're in the middle of an attack animation, we will stop it
        // and create a new one.
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }

        // A coroutine is a method that is typically related to some sort
        // of time.
        attackCoroutine = StartCoroutine(IEAttack());
    }

    // For example, in this Coroutine, we wait .5 seconds
    // in the middle of the method.
    private IEnumerator IEAttack()
    {
        playerAnimations.SetAttackAnimation(true);
        yield return new WaitForSeconds(0.5f);
        playerAnimations.SetAttackAnimation(false);
    }

    private void EnemySelectedCallback(EnemyBrain enemySelected)
    {
        enemyTarget = enemySelected;
    }

    private void NoEnemySelectedCallback()
    {
        enemyTarget = null;
    }

    private void OnEnable()
    {
        actions.Enable();
        SelectionManager.OnEnemySelectedEvent += EnemySelectedCallback;
        SelectionManager.OnNoSelectionEvent += NoEnemySelectedCallback;
    }

    private void OnDisable()
    {
        actions.Disable();
        SelectionManager.OnEnemySelectedEvent -= EnemySelectedCallback;
        SelectionManager.OnNoSelectionEvent -= NoEnemySelectedCallback;
    }
}
