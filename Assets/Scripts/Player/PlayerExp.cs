using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            AddExp(300f);
        }
    }

    public void AddExp(float amount)
    {
        // TotalExp is referenced by our stats panel.
        stats.TotalExp += amount;
        stats.CurrentExp += amount;

        // While loop is actually really smart here - it handles
        // the case where EXP that should level the player more than
        // 1 time is awarded.  Otherwise player would have to go earn
        // EXP to trigger subsequent level ups.
        while (stats.CurrentExp >= stats.NextLevelExp)
        {
            stats.CurrentExp -= stats.NextLevelExp;
            LevelUp();
        }
    }

    // Increases player level, and sets the exp needed to reach the next level.
    private void LevelUp()
    {
        stats.Level++;

        stats.AttributePoints++;

        float previousExpRequired = stats.NextLevelExp;
        float additionalExpForNextLevel = previousExpRequired * (stats.ExpMultiplier / 100);

        stats.NextLevelExp = Mathf.Round(previousExpRequired + additionalExpForNextLevel);
    }
}
