using UnityEngine;

public class FurnaceBehaviour : MonoBehaviour
{
    private Collider sphereCollider;
    
    [Header("Player Tags")]
    [SerializeField] private string exorcistTag = "Exorcist";

    [Header("Furnace")]
    [SerializeField] GameEvent allItemsCollectedEvent;
    [SerializeField] GameEvent itemsBurnedEvent;
    [SerializeField] private int totalNumberOfItems;
    [SerializeField] private int itemsCollected = 0;

    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    public void checkCollectedItems()
    { 
        itemsCollected += 1;
        if (itemsCollected == totalNumberOfItems)
        {
            //Furnace becomes interactable
            allItemsCollectedEvent.Raise();
            sphereCollider.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(exorcistTag))
        {
            itemsBurnedEvent.Raise();
        }
    }
}
