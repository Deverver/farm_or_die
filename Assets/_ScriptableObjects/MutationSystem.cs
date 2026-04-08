using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Farm/MutationEffect")]
public class MutationEffect : ScriptableObject
{
    public string mutationName;
    public MutationRarity rarity;
    public float harvestDifficultyMod;
    public float playerDamageOnHarvest;
    public float healAmountMod;
    public bool scorchedImmune;
    public bool hailImmune;
    public string description;
}

public class MutationSystem : MonoBehaviour
{
    public MutationEffect[] allMutations; // Assign i Inspector

    // Rarity vægte
    private const float CommonWeight = 60f;
    private const float UncommonWeight = 25f;
    private const float RareWeight = 12f;
    private const float LegendaryWeight = 3f;

    public void RollMutations(Plant plant, WeatherType weather)
    {
        float chance = plant.data.mutationChanceBase;
        if (weather == WeatherType.AcidRain) chance = 1f; // Garanteret

        if (Random.value < chance && plant.mutations.Count < plant.data.maxMutationSlots)
        {
            var mutation = PickMutation(plant);
            plant.mutations.Add(mutation);
            Debug.Log($"{plant.data.plantName} fik mutation: {mutation.mutationName}");
        }
    }

    public void RollAllMutations(PlantSlot[] allSlots, WeatherType weather)
    {
        foreach (var slot in allSlots)
        {
            if (!slot.IsOccupied()) continue;
            Plant plant = slot.GetCurrentPlant();
            if (plant != null) RollMutations(plant, weather);
        }
    }

    private MutationEffect PickMutation(Plant plant)
    {
        if (allMutations == null || allMutations.Length == 0) return null;

        // Byg vægtet liste – undgå dubletter spilleren allerede har
        var weighted = new List<(MutationEffect mutation, float weight)>();
        foreach (var m in allMutations)
        {
            if (plant.mutations.Contains(m)) continue; // spring over eksisterende

            float w = m.rarity switch
            {
                MutationRarity.Common => CommonWeight,
                MutationRarity.Uncommon => UncommonWeight,
                MutationRarity.Rare => RareWeight,
                MutationRarity.Legendary => LegendaryWeight,
                _ => CommonWeight
            };
            weighted.Add((m, w));
        }

        if (weighted.Count == 0) return null;

        float total = 0f;
        foreach (var (_, w) in weighted) total += w;

        float roll = Random.value * total;
        float cumulative = 0f;

        foreach (var (mutation, w) in weighted)
        {
            cumulative += w;
            if (roll <= cumulative) return mutation;
        }

        return weighted[^1].mutation; // fallback
    }
}
