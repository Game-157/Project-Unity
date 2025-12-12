using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private CharacterFactory characterFactory;

    private ScoreSystem scoreSystem;

    private float gameSessionTime;
    private float timeBetweenEnemySpawn;
    private bool isGameActive;

    public static GameManager Instance { get; private set; }

    public CharacterFactory CharacterFactory => characterFactory;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Initialize();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Initialize()
    {
        scoreSystem = new ScoreSystem();
        isGameActive = false;
    }

    public void StartGame()
    {
        if (isGameActive)
        {
            return;
        }

        Character player = characterFactory.GetCharacter(CharacterType.Hero);
        player.transform.position = Vector3.zero;
        player.Initialize();

        gameSessionTime = 0;
        timeBetweenEnemySpawn = gameData.TimeBetweenEnemySpawn;

        isGameActive = true;
    }

    public void Update()
    {
        if (!isGameActive)
        {
            return;
        }

        gameSessionTime += Time.deltaTime;
        timeBetweenEnemySpawn -= Time.deltaTime;

        if (timeBetweenEnemySpawn <= 0)
        {
            SpawnEnemy();
            timeBetweenEnemySpawn = gameData.TimeBetweenEnemySpawn;
        }

        if (gameSessionTime >= gameData.SessionTimeSeconds)
        {
            GameVictory();
        }
    }
    
    private void CharacterDeathHandler(Character deathCharacter)
    {
        switch (deathCharacter.CharacterType)
        {
            case CharacterType.Hero:
                Debug.Log("Game Over!");
                isGameActive = false;
                break;
            case CharacterType.DefaultEnemy:
                scoreSystem.AddScore(gameData.ScorePerEnemy);
                characterFactory.ReturnCharacter(deathCharacter);
                break;
        }
    }
    private void SpawnEnemy()
    {
        Character enemy = characterFactory.GetCharacter(CharacterType.DefaultEnemy);
        Vector3 heroPosition = characterFactory.Hero.transform.position;

        enemy.transform.position = new Vector3(heroPosition.x + GetOffset(), 0, heroPosition.z + GetOffset());
        enemy.Initialize();

        float GetOffset()
        {
            bool isPluse = Random.Range(0, 100) % 2 == 0;
            float offset = Random.Range(gameData.MinSpawnOffset, gameData.MaxSpawnOffset);
            return isPluse ? offset : (-1 * offset);
        }
    }

    private void GameVictory()
    {
        Debug.Log("Victory!");
        isGameActive = false;
    }

    private void GameOver()
    {
        Debug.Log("Defeat!");
        isGameActive = false;
    
    }
}
