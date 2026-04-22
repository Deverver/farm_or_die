using UnityEngine;
using System;

public class DaySystem : MonoBehaviour
{
    
    public int CurrentDay { get; private set; } = 1;

    // ── Cached References ─────────────────────────────────────────
    private PlantSlot[] allSlots;
    private GameManager gm;

    void Start()
    {
        gm = GameManager.Instance;
        allSlots = FindObjectsByType<PlantSlot>(FindObjectsSortMode.None);

        gm.uiManager.UpdateDay(CurrentDay);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            EndDay();
    }

    // ── Day Cycle ─────────────────────────────────────────────────
    /// <summary>
    /// 1. Plants grow
    /// 2. System checks if crossbreeding occurred and processes it
    /// 3. Mutation rolls
    /// 4. Environmental effects (radiation, weather damage)
    /// 5. Timed events (WeekEvent/MonthEvent)
    /// 6. Player resources reset
    /// 7. Day count up + new weather rolls
    /// </summary>
    public void EndDay()
    {
        HandlePlantGrowth();
        GameManager.Instance.crossbreedingSystem?.ProcessCrossbreeding();
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
        gm.mutationSystem?.RollAllMutations(allSlots, gm.weatherSystem.CurrentWeather);
    }


    private void HandleEnvironment()
    {
        // WIP (RadiationSystem)
    }

    private void HandlePlayerReset()
    {
        gm.playerStats.RegenerateEnergy(gm.playerStats.maxEnergy);
    }

    // ── Start-of-Next-Day Steps ───────────────────────────────────
    private void HandleNewDaySetup()
    {
        // WIP (WeatherSystem)
    }

    // ── Timed Events ───────────────────────────────────────
    private void CheckForTimedEvents()
    {
        if (CurrentDay % 7 == 0) WeeklyEvent();
        if (CurrentDay % 30 == 0) MonthlyEvent();
    }

    private void WeeklyEvent()
    {
        // WIP Event(Traveling Merchant);
        // WIP Event (Player getting taxed by The Farmer);
        Debug.Log("Ugentlig event – rejsende købmand?");
    }

    private void MonthlyEvent()
    {
        // WIP Event(MonthOverview);
        // WIP Event (Player getting taxed by The Farmer);
        Debug.Log("Månedlig event");
    }
}
