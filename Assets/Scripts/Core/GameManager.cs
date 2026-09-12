using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private bool debugMode = false;

    private GameState gameState = GameState.Menu;
    private float gameSpeed = 1f;
    private bool isPaused = false;

    public event Action<GameState> OnGameStateChanged;
    public event Action OnGamePaused;
    public event Action OnGameResumed;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = (int)Constants.TARGET_FPS;
    }

    private void Start()
    {
        if (debugMode)
            Debug.Log($"[GameManager] Initialized - {Constants.GAME_TITLE} v{Constants.GAME_VERSION}");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void SetGameState(GameState newState)
    {
        if (gameState == newState) return;

        gameState = newState;
        OnGameStateChanged?.Invoke(gameState);

        if (debugMode)
            Debug.Log($"[GameManager] State changed to: {gameState}");
    }

    public GameState GetGameState() => gameState;

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : gameSpeed;

        if (isPaused)
            OnGamePaused?.Invoke();
        else
            OnGameResumed?.Invoke();

        if (debugMode)
            Debug.Log($"[GameManager] Game paused: {isPaused}");
    }

    public bool IsPaused() => isPaused;

    public void SetGameSpeed(float speed)
    {
        gameSpeed = Mathf.Clamp(speed, 0.1f, 3f);
        if (!isPaused)
            Time.timeScale = gameSpeed;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        if (debugMode)
            Debug.Log("[GameManager] Quitting game...");

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}

public enum GameState
{
    Splash,
    PlayerCreation,
    Menu,
    Loading,
    GameWorld,
    Garage,
    Challenges,
    Paused
}
