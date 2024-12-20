using UnityEngine;

public class CollectItem : MonoBehaviour
{
    [Header("Objective Items")]
    [SerializeField] GameEvent itemCollectedEvent;
    [SerializeField] private string itemTag = "Item";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(itemTag))
        {
            itemCollectedEvent.Raise();
            Destroy(other.gameObject);
        }
    }
}
