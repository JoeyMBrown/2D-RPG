using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float speed;

    public Vector3 Direction { get; set; }

    // Move the projectile every frame in the given direction
    // at the given speed.
    private void Update()
    {
        transform.Translate(Direction * (speed * Time.deltaTime));
    }

    // An event that is called when collider enters 2D trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log(other.name) <-- Shows the game object we are collding with.
        // Destroy this gameObject when it collides with something.
        
        Destroy(gameObject);
    }
}
