using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFactory : MonoBehaviour
{
    [SerializeField] private Character HeroCharacterPrefab;
    [SerializeField] private Character EnemyCharacterPrefab;

    private Dictionary<CharacterType, Queue<Character>> disabledCharacters = new Dictionary<CharacterType, Queue<Character>>();

    private List<Character> activeCharacters = new List<Character>();

    public Character Hero
    {
        get; private set;
    }

    public List<Character> ActiveCharacters => activeCharacters;

    public Character GetCharacter(CharacterType type)
    {
        Character character = null;

        if (!disabledCharacters.ContainsKey(type))
        {
            disabledCharacters.Add(type, new Queue<Character>());
        }

        if (disabledCharacters[type].Count > 0)
        {
            character = disabledCharacters[type].Dequeue();
        }

        if (character == null)
        {
            character = InstantiateCharacter(type);
        }

        activeCharacters.Add(character);
        return character;
    }

    
    public void ReturnCharacter(Character character)
    {
        Queue<Character> characters = disabledCharacters[character.CharacterType];
        characters.Enqueue(character);

        activeCharacters.Remove(character);
    }
    

    private Character InstantiateCharacter(CharacterType type)
    {
        Character character = null;
        switch (type)
        {
            case CharacterType.Hero:
                character = GameObject.Instantiate(HeroCharacterPrefab, null);
                Hero = character;
                break;
            case CharacterType.DefaultEnemy:
                character = GameObject.Instantiate(EnemyCharacterPrefab, null);
                break;
            default:
                Debug.LogError("I dont now is type" + type);
                break;
        }
        return character;
    }
}
