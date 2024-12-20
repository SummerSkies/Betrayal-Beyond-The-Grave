using UnityEngine;

public class ObjectiveItemBehaviour : MonoBehaviour
{
    [SerializeField] GameEvent onCollectEvent;

    private void OnDestroy()
    {
        onCollectEvent.Raise();
    }
}