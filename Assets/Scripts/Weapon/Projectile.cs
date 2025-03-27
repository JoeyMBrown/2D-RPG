using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float speed;

    public Vector3 Direction { get; set; }
    public float Damage { get; set; }

    // Move the projectile every frame in the given direction
    // at the given speed.
    private void Update()
    {
        transform.Translate(Direction * (speed * Time.deltaTime));
    }

    // An event that is called when collider enters 2D trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log(other.name) <-- Shows the game object we are collding with.

        // Get what we collided with
        // If it is an instance of Idamageable
        // Call it's TakeDamage method ad pass in the Damage amount
        other.GetComponent<IDamageable>()?.TakeDamage(Damage);
        // Destroy this gameObject when it collides with something.
        Destroy(gameObject);
    }
}
