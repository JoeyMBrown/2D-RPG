using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float expDrop;

    public float ExpDrop => expDrop;
}
