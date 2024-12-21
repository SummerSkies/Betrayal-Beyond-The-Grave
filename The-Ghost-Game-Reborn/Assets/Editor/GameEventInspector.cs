using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEvent))]
public class GameEventInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameEvent gameEvent = (GameEvent)target;

        if (GUILayout.Button("Raise"))
        {
            gameEvent.Raise();
        }
    }
}
