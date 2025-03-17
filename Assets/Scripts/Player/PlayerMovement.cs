using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Adds a section header to unity editor
    [Header("Config")]
    // Using SerializeField attribute will show this property
    // inside of the unity editor.  This allows this property
    // to be seen inside of Unity editor even know it is
    // private.
    [SerializeField] private float speed;
    
    // This references the script we auto-generated at
    // /actions/PlayerActions
    private PlayerActions actions;

    // Gets the hash of these values, this allows us to only have
    // to change this line if we need to change this in the future.
    private readonly int moveX = Animator.StringToHash("MoveX");
    private readonly int moveY = Animator.StringToHash("MoveY");
    private readonly int moving = Animator.StringToHash("Moving");

    private Rigidbody2D rb2D;

    private Vector2 moveDirection;

    private Animator animator;

    private void Awake()
    {
        actions = new PlayerActions();
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Checking for movement input.
        ReadMovement();
    }

    // We use fixedUpdate when working with physics and rigid bodies.
    private void FixedUpdate()
    {
        Move();
    }

    // Move the player based on their current position and the current input direction
    // We then factor in speed.  Fixed delta time is allowing us to multiply our speed
    // by a value that is not dependent on frame rate.  It is instead based on a fix 
    // rate.
    private void Move()
    {
        rb2D.MovePosition(rb2D.position + moveDirection * (speed * Time.fixedDeltaTime));
    }

    private void ReadMovement()
    {
        // This translates directly to the unity editor.  We're accessing the
        // actions.Movement.Move property.  We're reading it and it is a
        // vector2.  The normalized call prevents an issue when moving
        // diagnoally.  It ensures the returned value is never greater
        // than 1.  Without this, moving diagnoally would be faster
        // than right, or up alone.
        moveDirection = actions.Movement.Move.ReadValue<Vector2>().normalized;

        if (moveDirection == Vector2.zero)
        {
            animator.SetBool(moving, false);
            return;
        }

        animator.SetBool(moving, true);

        // Update moveX and moveY for animation reference.
        animator.SetFloat(moveX, moveDirection.x);
        animator.SetFloat(moveY, moveDirection.y);
    }

    // Called when our game objects gets enabled.
    private void OnEnable()
    {
        actions.Enable();
    }

    // Called when our game objects gets disabled.
    private void OnDisable()
    {
        actions.Disable();
    }

}
