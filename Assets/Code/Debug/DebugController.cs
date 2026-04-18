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

        if (Input.GetKeyDown(KeyCode.T))
        {
            var hero = GameManager.Instance.CharacterFactory.Hero;

            if (hero == null || hero.HealthComponent == null)
                return;

            
            var newHealth = new HealthBoosterTier1Decorator(hero.HealthComponent, 10);

            newHealth.Initialize(hero); // важно

            hero.SetHealthComponent(newHealth);

            Debug.Log("HP Boost applied (+20)");
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
