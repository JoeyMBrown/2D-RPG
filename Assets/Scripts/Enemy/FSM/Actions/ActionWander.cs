using UnityEngine;

public class ActionWander : FSMAction
{
    [Header("Config")]
    [SerializeField] private float speed;
    // How often we move to a new location.
    [SerializeField] private float wanderTime;
    // Represents how far the enemy is able to move.
    [SerializeField] private Vector2 moveRange;

    private Vector3 movePosition;
    // Inits at 0.
    private float timer;

    private void Start()
    {
        GetNewDestination();
    }

    public override void Act()
    {
        // Everytime timer is <= 0 we will get a new direction.
        timer -= Time.deltaTime;

        // Here movePosition is lets say 11, 10.  and our current is
        // 10, 11.  We subtract x and y.  1, -1.
        Vector3 moveDirection = (movePosition - transform.position).normalized;
        // this is the direction we need to move in.  The speed * time.deltatime
        // is how far we move THIS frame.
        Vector3 movement = moveDirection * (speed * Time.deltaTime);
        // if our enemy's position and our movePosition is >= 0.5, we move.
        // We check here how far away we still are using our current pos
        // and the target pos, then we decide if we need to continue
        // moving in that direction or not.
        if (Vector3.Distance(transform.position, movePosition) >= 0.5f)
        {
            transform.Translate(movement);
        }
    }

    private void GetNewDestination()
    {
        // Random number between -x, and x
        float randomX = Random.Range(-moveRange.x, moveRange.x);
        float randomY = Random.Range(-moveRange.y, moveRange.y);

        // movePosition is current position + new random, that way we're
        // not always setting movePosition to x,y.  Instead it's currPos
        // + new x and + new y.
        // EX: if I'm a 10, 11.  and randomX and randomY are 1, -1.
        // movePosition will be 11, 10.  As these are the new coords
        // I need to move to.
        movePosition = transform.position + new Vector3 (randomX, randomY);
    }
}
