using UnityEngine;

public class DaySystem : MonoBehaviour
{
    // ── State ─────────────────────────────────────────────────────
    public int CurrentDay { get; private set; } = 1;

    // ── Cached References ─────────────────────────────────────────
    private PlantSlot[] allSlots;
    private GameManager gm;

    // ── Unity Lifecycle ───────────────────────────────────────────
    void Start()
    {
        gm = GameManager.Instance;
        allSlots = FindObjectsByType<PlantSlot>(FindObjectsSortMode.None);

        gm.uiManager.UpdateDay(CurrentDay);
    }

    void Update()
    {
        // Temp: Space som End Day – erstattes af UI-knap
        if (Input.GetKeyDown(KeyCode.Space))
            EndDay();
    }

    // ── Day Cycle ─────────────────────────────────────────────────

    /// <summary>
    /// Rækkefølge jf. game design spec:
    /// 1. Planter gror
    /// 2. Mutation rolls
    /// 3. Miljøeffekter (radiation, vejr-skade)
    /// 4. Tidsbestemte events
    /// 5. Spiller resources nulstilles
    /// 6. Dag tæller op + nyt vejr rulles
    /// </summary>
    public void EndDay()
    {
        HandlePlantGrowth();
        HandleMutations();
        HandleEnvironment();
        CheckForTimedEvents();
        HandlePlayerReset();

        CurrentDay++;
        gm.uiManager.UpdateDay(CurrentDay);

        HandleNewDaySetup();

        Debug.Log($"=== Dag {CurrentDay} starter ===");
    }

    // ── End-of-Day Steps ──────────────────────────────────────────
    private void HandlePlantGrowth()
    {
        foreach (PlantSlot slot in allSlots)
            slot.GrowPlant();
    }

    private void HandleMutations()
    {
        // Aktiveres i Fase 6 (MutationSystem)
        // MutationSystem.Instance?.RollAllMutations(allSlots, gm.weatherSystem.CurrentWeather);
    }

    private void HandleEnvironment()
    {
        // Aktiveres i Fase 7 (RadiationSystem)
        // gm.radiationSystem?.ApplyDailyRadiation(gm.playerStats);
    }

    private void HandlePlayerReset()
    {
        gm.playerStats.RegenerateEnergy(gm.playerStats.maxEnergy);
    }

    // ── Start-of-Next-Day Steps ───────────────────────────────────
    private void HandleNewDaySetup()
    {
        // Aktiveres i Fase 5 (WeatherSystem)
        // gm.weatherSystem?.RollWeather();
        // gm.uiManager.UpdateWeather(gm.weatherSystem.CurrentWeather);
    }

    // ── Tidsbestemte Events ───────────────────────────────────────
    private void CheckForTimedEvents()
    {
        if (CurrentDay % 7 == 0) WeeklyEvent();
        if (CurrentDay % 30 == 0) MonthlyEvent();
    }

    private void WeeklyEvent()
    {
        Debug.Log("Ugentlig event – rejsende købmand?");
        // TravelingMerchant.Instance?.Spawn();
    }

    private void MonthlyEvent()
    {
        Debug.Log("Månedlig event");
    }
}
