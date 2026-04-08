using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ── Singleton ─────────────────────────────────────────────────
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    // ── System References ─────────────────────────────────────────
    [Header("Systems")]
    public DaySystem daySystem;
    public WeatherSystem weatherSystem;
    public RadiationSystem radiationSystem;
    public MutationSystem mutationSystem;

    [Header("UI")]
    public UIManager uiManager;

    [Header("Player")]
    public PlayerStats playerStats;

    // ── Game State ────────────────────────────────────────────────
    public void GameOver()
    {
        Debug.Log("GAME OVER");
        // SceneManager.LoadScene("GameOver");
    }
}
