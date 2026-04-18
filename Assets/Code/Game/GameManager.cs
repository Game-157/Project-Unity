using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private CharacterFactory characterFactory;
    [SerializeField] private LevelConfig levelConfig;

    private ScoreSystem scoreSystem;
    private WaveManager waveManager;

    private float gameSessionTime;
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
            Destroy(gameObject);
        }
    }

    private void Initialize()
    {
        scoreSystem = new ScoreSystem();
        isGameActive = false;

        waveManager = gameObject.AddComponent<WaveManager>();
        waveManager.Init(characterFactory, levelConfig);
    }

    public void StartGame()
    {
        if (isGameActive) return;

        Character player = characterFactory.GetCharacter(CharacterType.Hero);
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
        player.Initialize();

        player.HealthComponent.OnCharacterDeath += CharacterDeathHandler;

        gameSessionTime = 0;
        scoreSystem.StartGame();

        isGameActive = true;
    }

    private void Update()
    {
        if (!isGameActive) return;

        gameSessionTime += Time.deltaTime;

        // WaveManager has its own Update wrapper; no manual Tick call needed here.

        if (gameSessionTime >= gameData.SessionTimeSeconds)
        {
            GameVictory();
        }
    }

    public void CharacterDeathHandler(Character deathCharacter)
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