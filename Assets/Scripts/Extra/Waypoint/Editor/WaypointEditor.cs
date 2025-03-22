using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Waypoint))]
public class WaypointEditor : Editor
{
    private Waypoint WaypointTarget => target as Waypoint;

    private void OnSceneGUI()
    {
        // There are no points in inspector
        if (WaypointTarget.Points.Length <= 0) return;

        // We're essentially going to be adding handles to these
        // points so we can drag them around the scene.
        Handles.color = Color.red;

        for (int i = 0; i < WaypointTarget.Points.Length; i++)
        {
            EditorGUI.BeginChangeCheck();

            // This places the waypoint on top of the entity.
            Vector3 currentPoint = WaypointTarget.EntityPosition + WaypointTarget.Points[i];
            // Builds a "Handle" at currentPoint, with a size of 0.5f, the snap is 0.5f, with
            // a sphere handle type.
            Vector3 newPosition = Handles.FreeMoveHandle(currentPoint, 0.5f, Vector3.one * 0.5f, Handles.SphereHandleCap);

            // Create a GUIStyle named text. Set font style, size, and text color.
            // define pos, attach it to the Handle as a label, at the entity position
            // plus the waypoint position, plus the text position (slight offset of
            // waypoint), text will be waypoint number + 1, with the text styles.
            GUIStyle text = new GUIStyle();
            text.fontStyle = FontStyle.Bold;
            text.fontSize = 16;
            text.normal.textColor = Color.black;
            Vector3 textPos = new Vector3(0.2f, -0.2f);
            Handles.Label(WaypointTarget.EntityPosition + WaypointTarget.Points[i] + textPos, $"{i + 1}", text);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Free Move");
                WaypointTarget.Points[i] = newPosition - WaypointTarget.EntityPosition;
            }
        }
    }
}
