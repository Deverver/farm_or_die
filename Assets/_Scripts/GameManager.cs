using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlantSlot[] allSlots;
    void Start()
    {
        allSlots = FindObjectsByType<PlantSlot>(FindObjectsSortMode.None);
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
        foreach (PlantSlot slot in allSlots)
        {
            slot.GrowPlant();
        }

        Debug.Log("Day ended");
    }
}