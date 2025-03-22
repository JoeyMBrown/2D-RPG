using UnityEngine;

public class ActionPatrol : FSMAction
{
    [Header("Config")]
    [SerializeField] private float speed;

    private Waypoint waypoint;
    private int pointIndex;
    private Vector3 nextPosition;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }

    public override void Act()
    {
        FollowPath();
    }

    // Move entity from current position, towardss current point position,
    // at the given speed. (Time.deltaTime accounts fro framerate differences
    // to ensure we move at the same speed regardless of FPS).
    private void FollowPath()
    {
        transform.position = Vector3.MoveTowards(transform.position, GetCurrentPosition(), speed * Time.deltaTime);

        // When we reach current position, update next position to the
        // next waypoint's position.
        if (Vector3.Distance(transform.position, GetCurrentPosition()) <= 0.1f)
        {
            UpdateNextPosition();
        }
    }

    // Get the position of the next waypoint in the array,
    // or the first waypoint if we've reached the end of
    // the array.
    private void UpdateNextPosition()
    {
        pointIndex++;

        if (pointIndex > waypoint.Points.Length - 1)
        {
            pointIndex = 0;
        }
    }

    private Vector3 GetCurrentPosition()
    {
        return waypoint.GetPosition(pointIndex);
    }
}
