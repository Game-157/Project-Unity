using UnityEngine;

public class CharacterSpawnController
{
    private readonly CharacterFactory characterFactory;

    private int currentMaxEnemies;
    private int absoluteMaxEnemies;

    private float increaseInterval;
    private float increaseTimer;
    private int increaseStep;

    public CharacterSpawnController(
        CharacterFactory factory,
        int startMaxEnemies,
        int absoluteMaxEnemies,
        float increaseInterval,
        int increaseStep
    )
    {
        characterFactory = factory;

        this.currentMaxEnemies = startMaxEnemies;
        this.absoluteMaxEnemies = absoluteMaxEnemies;
        this.increaseInterval = increaseInterval;
        this.increaseStep = increaseStep;

        increaseTimer = increaseInterval;
    }

    public void Update(float deltaTime)
    {
        increaseTimer -= deltaTime;

        if (increaseTimer <= 0)
        {
            currentMaxEnemies = Mathf.Min(
                currentMaxEnemies + increaseStep,
                absoluteMaxEnemies
            );

            increaseTimer = increaseInterval;
        }
    }

    public bool CanSpawnEnemy()
    {
        int enemyCount = 0;

        var activeCharacters = characterFactory.ActiveCharacters;
        for (int i = 0; i < activeCharacters.Count; i++)
        {
            if (activeCharacters[i].CharacterType == CharacterType.DefaultEnemy)
            {
                enemyCount++;
            }
        }

        return enemyCount < currentMaxEnemies;
    }
}
