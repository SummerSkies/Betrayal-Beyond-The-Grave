using System.Collections.Generic;
using UnityEngine;

public class CharacterLoader : MonoBehaviour
{
    [SerializeField] private List<GameObject> playableCharacters;
    [SerializeField] private int characterIndex;

    public GameObject chosenCharacter { get; private set; }

    private void Awake()
    {
        //Default character until character selection is implemented
        characterIndex = 4;
    }

    void Start()
    {
        LoadCharacter(characterIndex);
    }

    void LoadCharacter(int index)
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, 0, transform.position.z);

        if (chosenCharacter != null)
        {
            Destroy(chosenCharacter);
        }

        chosenCharacter = Instantiate(playableCharacters[index], spawnPosition, transform.rotation);
        chosenCharacter.transform.parent = transform;
    }
}

