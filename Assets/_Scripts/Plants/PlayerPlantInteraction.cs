using UnityEngine;

public class PlayerPlantInteraction : MonoBehaviour
{
    private PlantSlot currentSlot;
    private PlayerStats playerStats;
    private Inventory inventory;

    [Header("Aktiv frø-type (sæt i Inspector eller via UI)")]
    [SerializeField] private ItemData selectedSeed;

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentSlot != null)
        {
            if (!currentSlot.IsOccupied())
                TryPlant();
            else if (currentSlot.isReadyToHarvest())
                TryHarvest();
        }
    }

    private void TryPlant()
    {
        if (selectedSeed == null)
        {
            Debug.LogWarning("Intet frø valgt!"); return;
        }
        if (!inventory.HasItem(selectedSeed))
        {
            Debug.LogWarning("Du har ikke dette frø i inventory!"); return;
        }
        if (playerStats.UseEnergy(10))
        {
            inventory.RemoveItem(selectedSeed);
            currentSlot.Plant(selectedSeed.seedFor);
        }
    }

    private void TryHarvest()
    {
        if (playerStats.UseEnergy(10))
            currentSlot.Harvest();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var slot = other.GetComponent<PlantSlot>();
        if (slot != null) currentSlot = slot;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var slot = other.GetComponent<PlantSlot>();
        if (slot != null) currentSlot = null;
    }

    // Kaldes fra UI når spilleren skifter aktivt frø
    public void SetSelectedSeed(ItemData seed) => selectedSeed = seed;
}
