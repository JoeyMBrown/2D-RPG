using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    public float CurrentMana { get; private set; }
  
    // Initiate CurrentMana to our actual mana on game starts
    private void Start()
    {
        ResetMana();
    }

    public void UseMana(float amount)
    {
        // Update CurrentMana whenever we update our stats.
        // Will check for mana whenever we're attacking, not here.
        stats.Mana = Mathf.Max(stats.Mana -= amount, 0);
        CurrentMana = stats.Mana;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            UseMana(1f);
        }
    }

    public void ResetMana ()
    {
        CurrentMana = stats.MaxMana;
    }
}
