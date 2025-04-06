using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float moveSpeed;

    private readonly int moveX = Animator.StringToHash("MoveX");
    private readonly int moveY = Animator.StringToHash("MoveY");

    private Waypoint waypoint;
    private Animator animator;
    private Vector3 previousPos;
    private int currentPointIndex;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        waypoint = GetComponent<Waypoint>();
    }

    private void Update()
    {
        // Get our next position.
        Vector3 nextPos = waypoint.GetPosition(currentPointIndex);
        UpdateMoveValues(nextPos);
        transform.position = Vector3.MoveTowards(transform.position, nextPos, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, nextPos) <= 0.2)
        {
            previousPos = nextPos;
            // This works to account for going from alst position back to first.
            currentPointIndex = (currentPointIndex + 1) % waypoint.Points.Length;
        }
    }

    private void UpdateMoveValues(Vector3 nextPos)
    {
        Vector2 dir = Vector2.zero;

        // Here we're determining our move direction.  If our prev
        // x position is LESS than our next pos, we know we need to move
        // to the right, etc.
        if (previousPos.x < nextPos.x) dir = new Vector2(1f, 0f);
        if (previousPos.x > nextPos.x) dir = new Vector2(-1f, 0f);
        if (previousPos.y < nextPos.y) dir = new Vector2(0f, 1f);
        if (previousPos.y > nextPos.y) dir = new Vector2(0f, -1f);

        // Here we're updating our animator with the direction we need
        // to heads values to show the correct animation.
        animator.SetFloat(moveX, dir.x);
        animator.SetFloat(moveY, dir.y);
    }
}
