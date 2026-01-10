using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private CharacterFactory characterFactory;

    [SerializeField] private CharacterSpawnController spawnController;


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

        spawnController = new CharacterSpawnController(
        characterFactory,
        startMaxEnemies: 3,      
        absoluteMaxEnemies: 10,  
        increaseInterval: 10f,   
        increaseStep: 1          
    );

    }

    public void StartGame()
    {
        if (isGameActive)
        {
            return;
        }

        Character player = characterFactory.GetCharacter(CharacterType.Hero);
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
        player.Initialize();
        player.HealthComponent.OnCharacterDeath += CharacterDeathHandler;

        gameSessionTime = 0;
        timeBetweenEnemySpawn = gameData.TimeBetweenEnemySpawn;
        scoreSystem.StartGame();

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
            if (spawnController.CanSpawnEnemy())
            {
                SpawnEnemy();
            }
            timeBetweenEnemySpawn = gameData.TimeBetweenEnemySpawn;
        }

        if (gameSessionTime >= gameData.SessionTimeSeconds)
        {
            GameVictory();
        }

        spawnController.Update(Time.deltaTime);

    }
    
    private void CharacterDeathHandler(Character deathCharacter)
    {
        switch (deathCharacter.CharacterType)
        {
            case CharacterType.Hero:
                GameOver();
                break;
            case CharacterType.DefaultEnemy:
                scoreSystem.AddScore(deathCharacter.CharacterData.ScoreCost);
                break;
        }
        deathCharacter.gameObject.SetActive(false);
        characterFactory.ReturnCharacter(deathCharacter);
    }
    private void SpawnEnemy()
    {
        Character enemy = characterFactory.GetCharacter(CharacterType.DefaultEnemy);
        Character hero = characterFactory.Hero;

        enemy.transform.position = new Vector3(
            hero.transform.position.x + GetOffset(),
            0,
            hero.transform.position.z + GetOffset()
        );

        enemy.gameObject.SetActive(true);
        enemy.Initialize();
        enemy.HealthComponent.OnCharacterDeath += CharacterDeathHandler;

        
        if (enemy is EnemyCharacter enemyCharacter)
        {
            enemyCharacter.SetTarget(hero);
        }

        float GetOffset()
        {
            bool isPlus = Random.Range(0, 100) % 2 == 0;
            float offset = Random.Range(gameData.MinSpawnOffset, gameData.MaxSpawnOffset);
            return isPlus ? offset : -offset;
        }
    }


    private void GameVictory()
    {
        scoreSystem.EndGame();
        Debug.Log("Victory!");
        isGameActive = false;
    }

    private void GameOver()
    {
        scoreSystem.EndGame();
        Debug.Log("Defeat!");
        isGameActive = false;
    
    }
}
