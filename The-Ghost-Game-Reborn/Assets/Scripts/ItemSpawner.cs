using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectiveItems;
    [SerializeField] private List<Vector3> spawnLocations;

    public static ItemSpawner Instance { get; private set; }

    private void Awake()
    {
        //Only ever have one version of this game object at a time
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        collectSpawnLocations();
        spawnObjectiveItems();
    }

    void spawnObjectiveItems()
    {
        //Make a list copy of all posible spawn locations
        List<Vector3> avaliableLocations = spawnLocations;

        for (int i = 0; i < objectiveItems.Count; i++)
        {
            //Choose a random location
            int locationIndex = Random.Range(0, avaliableLocations.Count);

            //Spawn item at random location
            Instantiate(objectiveItems[i], avaliableLocations[locationIndex], objectiveItems[i].transform.rotation);

            //Remove this location from the list copy so it can't be used for more than one object
            avaliableLocations.RemoveAt(locationIndex);
        }
    }

    void collectSpawnLocations()
    {
        //For each child of this object, save its location as a spawn point, then destroy it
        foreach (Transform spawnPointMarker in transform)
        {
            spawnLocations.Add(spawnPointMarker.position);
            Destroy(spawnPointMarker.gameObject);
        }
    }
}
