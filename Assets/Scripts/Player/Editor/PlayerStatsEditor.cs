using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerStats))]
public class PlayerStatsEditor : Editor
{
    private PlayerStats StatsTarget => target as PlayerStats;

    // Defines a button, that if clicked will run the code
    // witin the "IF" statement.
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Reset Player Stats"))
        {
            StatsTarget.ResetPlayerStats();
        }
    }
}
