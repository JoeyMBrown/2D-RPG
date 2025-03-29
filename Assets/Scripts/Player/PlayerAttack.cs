using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttack : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;
    [SerializeField] private Weapon initialWeapon;
    // Positions that projectiles can be spawned at.
    [SerializeField] private Transform[] attackPositions;

    [Header("Melee Config")]
    [SerializeField] private ParticleSystem slashFX;
    [SerializeField] private float minDistanceMeleeAttack;
    public Weapon CurrentWeapon { get; set; }

    private PlayerActions actions;
    private PlayerAnimations playerAnimations;
    // A reference to the current movement direction.
    private PlayerMovement playerMovement;
    private PlayerMana playerMana;
    private EnemyBrain enemyTarget;
    private Coroutine attackCoroutine;

    private Transform currentAttackPosition;
    // Used to rotate projectiles.
    private float currentAttackRotation;

    private void Awake()
    {
        actions = new PlayerActions();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimations = GetComponent<PlayerAnimations>();
        playerMana = GetComponent<PlayerMana>();
    }

    private void Start()
    {
        EquipWeapon(initialWeapon);
        // Determine if our click atack action was performed,
        // if so, call Attack method within this context?
        actions.Attack.ClickAttack.performed += ctx => Attack();
    }

    private void Update()
    {
        GetFirePosition();
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
        // If we don't have current attack position, return.
        if (currentAttackPosition == null) yield break;

        // Check our weapon type to determine how we should attack.
        if (CurrentWeapon.WeaponType == WeaponType.Magic)
        {
            // Make sure we've got enough mana to perform a Magic attack.
            if (playerMana.CurrentMana < CurrentWeapon.RequiredMana) yield break;
            MagicAttack();
        }
        else
        {
            MeleeAttack();
        }

        playerAnimations.SetAttackAnimation(true);
        yield return new WaitForSeconds(0.5f);
        playerAnimations.SetAttackAnimation(false);
    }

    private void MagicAttack()
    {
        // The rotation of our projectile.
        Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, currentAttackRotation));

        // Spawn our projectile
        Projectile projectile = Instantiate(CurrentWeapon.ProjectilePrefab, currentAttackPosition.position, rotation);

        // Set the movement direction of our projectile to be UP.
        // This is in relation to the projectile game object, it will always move up.
        // This works because we are rotating the game object when we spawn it in.
        // This means "UP" will always be away from the player.
        projectile.Direction = Vector3.up;

        // Set the projectile's damage to our calculated damage taking crit into account.
        projectile.Damage = GetAttackDamage();

        playerMana.UseMana(CurrentWeapon.RequiredMana);
    }

    private void MeleeAttack()
    {
        // Place the slashFX game object at our crrent attack position
        slashFX.transform.position = currentAttackPosition.position;
        // play the animation.
        slashFX.Play();

        // Distance between us (player) and enemy.
        float currentDistanceToEnemy = Vector3.Distance(enemyTarget.transform.position, transform.position);

        // If we're within range of the enemy, damage them.
        if (currentDistanceToEnemy <= minDistanceMeleeAttack)
        {
            enemyTarget.GetComponent<IDamageable>().TakeDamage(GetAttackDamage());
        }
    }

    public void EquipWeapon(Weapon newWeapon)
    {
        CurrentWeapon = newWeapon;

        // Total damage is referenced by our stat panel.
        stats.TotalDamage = stats.BaseDamage + CurrentWeapon.Damage;
    }

    private float GetAttackDamage()
    {
        float damage = stats.BaseDamage;
        damage += CurrentWeapon.Damage;

        float randomPercentage = Random.Range(0f, 100f);

        // If within critical chance range, apply crit damage.
        if (randomPercentage <= stats.CriticalChance)
        {
            damage += damage * (stats.CriticalDamage / 100f);
        }

        return damage;
    }

    private void GetFirePosition()
    {
        // This sets our current move direction.
        Vector2 moveDirection = playerMovement.MoveDirection;
        switch (moveDirection.x)
        {
            // If x > 0, we're facing right
            case > 0f:
                // Attack position is right position
                currentAttackPosition = attackPositions[1]; // This correlates to the unity editor and the indexes of each attack position setup there.
                // Rotate projectile -90
                currentAttackRotation = -90f;
                break;
            // If x < 0, we're facing left
            case < 0f:
                // Attack position is left position
                currentAttackPosition = attackPositions[3];
                // rotate projectile -270
                currentAttackRotation = -270f;
                break;
        }
        
        switch (moveDirection.y)
        {
            // If y > 0, we're facing up
            case > 0f:
                // Attack position is up position
                currentAttackPosition = attackPositions[0]; // This correlates to the unity editor and the indexes of each attack position setup there.
                // Rotate projectile 0
                currentAttackRotation = 0f;
                break;
            // If y < 0, we're facing down
            case < 0f:
                // Attack position is left position
                currentAttackPosition = attackPositions[2];
                // rotate projectile -180f
                currentAttackRotation = -180f;
                break;
        }
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
        // Subscribe to all these events.
        SelectionManager.OnEnemySelectedEvent += EnemySelectedCallback;
        SelectionManager.OnNoSelectionEvent += NoEnemySelectedCallback;
        EnemyHealth.OnEnemyDeadEvent += NoEnemySelectedCallback;
    }

    private void OnDisable()
    {
        actions.Disable();
        SelectionManager.OnEnemySelectedEvent -= EnemySelectedCallback;
        SelectionManager.OnNoSelectionEvent -= NoEnemySelectedCallback;
        EnemyHealth.OnEnemyDeadEvent -= NoEnemySelectedCallback;
    }
}
