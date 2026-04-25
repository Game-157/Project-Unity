using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private CharacterFactory characterFactory;
    [SerializeField] private LevelConfig levelConfig;

    [SerializeField] private GameObject startButton;

    [SerializeField] private UI_Manager uiManager;

    private ScoreSystem scoreSystem;
    private WaveManager waveManager;

    private float gameSessionTime;
    private bool isGameActive;

    public static GameManager Instance { get; private set; }

    public CharacterFactory CharacterFactory => characterFactory;

    [SerializeField] private BaseBuilding baseBuilding;

    public BaseBuilding BaseBuilding => baseBuilding;

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

        startButton.SetActive(false);

        Character player = characterFactory.GetCharacter(CharacterType.Hero);
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
        player.Initialize();

        player.HealthComponent.OnCharacterDeath += CharacterDeathHandler;

        Camera.main.GetComponent<CameraFollow>().SetTarget(player.transform);

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
        uiManager.ShowWinMenu();
    }

    private void GameOver()
    {
        isGameActive = false;
        scoreSystem.EndGame();
        Debug.Log("Defeat!");
        //characterFactory.KillAllEnemies();
        uiManager.ShowLoseMenu();
    }
}