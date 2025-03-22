using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Vector3[] points;

    public Vector3[] Points => points;
    public Vector3 EntityPosition { get; set; }

    private bool gameStarted;

    private void Start()
    {
        EntityPosition = transform.position;
    }

    private void OnDrawGizmos()
    {
        // If we're not in play mode, and we modify our transform
        // modify the entity position so handles spawn on top
        // of the entity they're for.
        if (gameStarted == false && transform.hasChanged)
        {
            EntityPosition = transform.position;
        }
    }
}
