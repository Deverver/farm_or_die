using System.Diagnostics;
using System.Xml.Serialization;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlantSlot[] allSlots;

    public UIManager uiManager;
    public int currentDay = 1;

    void Start()
    {
        allSlots = FindObjectsByType<PlantSlot>(FindObjectsSortMode.None);
        uiManager.UpdateDay(currentDay);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndDay();
        }
    }


    public void EndDay()
    {
        HandlePLantGrowth();
        HandlePlayerEnergy();

        
        Debug.Log("Day ended");
        currentDay++; // This is the turning point for a new day
        uiManager.UpdateDay(currentDay);
        HandleDayEvents();
    }



    public void HandlePLantGrowth()
    {
        foreach (PlantSlot slot in allSlots)
        {
            slot.GrowPlant();
        }
    }

    public void HandlePlayerEnergy()
    {
        FindAnyObjectByType<PlayerStats>().RegenerateEnergy(80);
    }


    public void HandleDayEvents()
    {
        CheckForTimedEvents();
    }

    public void WeeklyEvent()
    {
    }

    public void MonthlyEvent()
    {
    }

    public void CheckForTimedEvents() 
    { 
        if (currentDay % 7 == 0)
        {
            WeeklyEvent();
        }
        if (currentDay % 30 == 0)
        {
            MonthlyEvent();
        }
    }




}