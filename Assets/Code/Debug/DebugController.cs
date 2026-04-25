using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    [SerializeField] private CharacterFactory characterFactory;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            KillAllEnemies();
        }

    }

    private void KillAllEnemies()
    {
        var activeCharacters = characterFactory.ActiveCharacters;

        // копия списка, чтобы избежать проблем при удалении из пула
        for (int i = activeCharacters.Count - 1; i >= 0; i--)
        {
            Character character = activeCharacters[i];

            if (character == null)
                continue;

            if (character.CharacterType != CharacterType.DefaultEnemy)
                continue;

            if (character.HealthComponent == null)
                continue;

            character.HealthComponent.Kill();
        }

        Debug.Log("DEBUG: All enemies killed");
    }


}
