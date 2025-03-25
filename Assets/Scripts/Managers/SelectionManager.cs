using System;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static event Action<EnemyBrain> OnEnemySelectedEvent;
    public static event Action OnNoSelectionEvent;

    [Header("Config")]
    [SerializeField] private LayerMask enemyMask;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        SelectEnemy();
    }

    private void SelectEnemy()
    {
        // If a click happened
        if (Input.GetMouseButtonDown(0))
        {
            // Basically here we're querying the click position, (not sure what vector zero is
            // or infinity, but also only detecting if we hit an enemy layer mask.
            RaycastHit2D hit = Physics2D.Raycast(
                mainCamera.ScreenToWorldPoint(Input.mousePosition),
                Vector2.zero,
                Mathf.Infinity,
                enemyMask
            );

            if (hit.collider != null)
            {
                EnemyBrain enemy = hit.collider.GetComponent<EnemyBrain>();

                // If we clicked an enemy
                // ELSE we clicked something not an enemy.
                if (enemy != null)
                {
                    // Fires an event when an enemy was clicked, passing in
                    // clicked enemy reference.
                    OnEnemySelectedEvent?.Invoke(enemy);
                }
            }
            else
            {
                // Fires when we click inside game view and a layermask of enemy
                // is not clicked.
                OnNoSelectionEvent?.Invoke();
            }
        }
    }

}
