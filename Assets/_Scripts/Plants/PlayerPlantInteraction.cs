using UnityEngine;

public class PlayerPlantInteraction : MonoBehaviour
{
    private PlantSlot currentSlot;
    private PlayerStats playerStats;

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentSlot != null)
            {
                if (!currentSlot.IsOccupied()) // Slots is empty
                {
                    if (playerStats.UseEnergy(10))
                    {
                        currentSlot.Plant();
                    }
                }
                else
                {
                    if (currentSlot.isReadyToHarvest()) // slot is not empty, check plant
                    {
                        if (playerStats.UseEnergy(10))
                        {
                            currentSlot.Harvest();
                        }
                    }
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        PlantSlot slot = other.GetComponent<PlantSlot>();
        if (slot != null)
        {
            currentSlot = slot;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlantSlot slot = other.GetComponent<PlantSlot>();
        if (slot != null)
        {
            currentSlot = null;
        }
    }
}