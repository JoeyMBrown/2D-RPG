using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    public void UseMana(float amount)
    {
        if (stats.Mana >= amount)
        {
            // Ensures we never go negative.
            stats.Mana = Mathf.Max(stats.Mana -= amount, 0);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            UseMana(1f);
        }
    }
}
