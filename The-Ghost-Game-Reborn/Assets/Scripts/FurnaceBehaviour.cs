using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceBehaviour : MonoBehaviour
{
    [Header("Player Tags")]
    [SerializeField] private string exorcistTag = "Exorcist";
    [SerializeField] private string posessedTag = "Posessed";

    [Header("Furnace")]
    [SerializeField] GameEvent allItemsCollectedEvent;
    [SerializeField] private int totalNumberOfItems;
    [SerializeField] private int itemsCollected = 0;
    [SerializeField] private bool isActive = false;

    public void checkCollectedItems()
    { 
        itemsCollected += 1;
        if (itemsCollected == totalNumberOfItems)
        {
            //Furnace becomes interactable
            allItemsCollectedEvent.Raise();
            isActive = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(exorcistTag))
        {
            //burn items
        }
    }
}
