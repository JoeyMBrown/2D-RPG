using UnityEngine;

public class DamageManager : MonoBehaviour
{
    // Singleton pattern being used here.
    public static DamageManager Instance;

    [Header("Config")]
    [SerializeField] private DamageText damageTextPrefab;

    private void Awake()
    {
        Instance = this;
    }

    // When this is called, we will instantiate a damageTextPrefab
    // at the parent's position.
    public void ShowDamageText(float damageAmount, Transform parent)
    {
        // Instantiate text object at parent position.
        DamageText text = Instantiate(damageTextPrefab, parent);
        // Move it to the right .5f
        text.transform.position += Vector3.right * 0.5f;
        // Set the text of the text to the damage being done.
        text.SetDamageText(damageAmount);
    }
}
