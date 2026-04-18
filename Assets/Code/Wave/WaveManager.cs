using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private CharacterFactory factory;
    private LevelConfig levelConfig;

    private int currentWaveIndex;
    private int spawnedEnemies;

    private float spawnTimer;

    public void Init(CharacterFactory factory, LevelConfig levelConfig)
    {
        this.factory = factory;
        this.levelConfig = levelConfig;

        StartWave(0);
    }

    public void Tick(float deltaTime, Character hero)
    {
        if (hero == null)
            return;

        if (currentWaveIndex >= levelConfig.waves.Count)
            return;

        var wave = levelConfig.waves[currentWaveIndex];

        spawnTimer -= deltaTime;

        if (spawnTimer <= 0 && spawnedEnemies < wave.enemyCount)
        {
            SpawnEnemy(hero, wave);
            spawnedEnemies++;

            spawnTimer = wave.spawnDelay;
        }

        // переход к следующей волне
        if (spawnedEnemies >= wave.enemyCount && AllEnemiesDead())
        {
            StartWave(currentWaveIndex + 1);
        }
    }

    
    private void Update()
    {
        if (factory == null || levelConfig == null)
            return;

        var hero = factory.Hero;
        Tick(Time.deltaTime, hero);
    }

    private void StartWave(int index)
    {
        if (index >= levelConfig.waves.Count)
        {
            Debug.Log("LEVEL COMPLETE");
            return;
        }

        currentWaveIndex = index;
        spawnedEnemies = 0;

        spawnTimer = levelConfig.waves[index].spawnDelay;

        Debug.Log($"Start Wave: {currentWaveIndex + 1}");
    }

    private void SpawnEnemy(Character hero, WaveConfig wave)
    {
        Character enemy = factory.GetCharacter(wave.enemyType);

        Vector3 spawnPos = hero.transform.position + GetRandomOffset(wave);
        spawnPos.y = 0;

        enemy.transform.position = spawnPos;

        enemy.gameObject.SetActive(true);
        enemy.Initialize();

        if (enemy.HealthComponent != null && GameManager.Instance != null)
        {
            enemy.HealthComponent.OnCharacterDeath += GameManager.Instance.CharacterDeathHandler;
        }

        if (enemy is EnemyCharacter enemyCharacter)
        {
            enemyCharacter.SetTarget(hero);
        }
    }

    private Vector3 GetRandomOffset(WaveConfig wave)
    {
        float offsetX = Random.Range(wave.minOffset, wave.maxOffset) * (Random.value > 0.5f ? 1 : -1);
        float offsetZ = Random.Range(wave.minOffset, wave.maxOffset) * (Random.value > 0.5f ? 1 : -1);

        return new Vector3(offsetX, 0, offsetZ);
    }

    private bool AllEnemiesDead()
    {
        var active = factory.ActiveCharacters;

        for (int i = 0; i < active.Count; i++)
        {
            if (active[i].CharacterType == CharacterType.DefaultEnemy)
                return false;
        }

        return true;
    }
}
