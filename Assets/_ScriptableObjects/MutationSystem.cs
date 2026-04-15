using FarmGame.Mutations;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Farm/Mutations/MutationEffect", fileName = "NewMutation")]
public class MutationEffect : ScriptableObject
{
    [Header("Identity")]
    public string mutationName;
    [TextArea] public string description;
    public MutationSlotType slotType;
    public MutationRarity rarity;

    [Header("Stat Modifier")]
    [Tooltip("Slot 1: harvest difficulty delta (+0.3 = sværere, -0.3 = nemmere)\n" +
             "Slot 2: yield integer delta (+2 = to ekstra)\n" +
             "Slot 3: value multiplikator (2.0 = dobbelt pris)")]
    public float modifier;

    [Header("Visuals – Mutation Sprite")]
    [Tooltip("Nøgle der matcher en MutationSpriteEntry i PlantData (fx 'fire', 'ice').\n" +
             "Når planten er ved maxGrowthStage vises den tilsvarende familie-sprite. Tom = ingen sprite-effekt.")]
    public string mutationSpriteKey;

    [Header("Crossbreeding")]
    [Tooltip("Relativ spredningsvægt. Højere = spreder sig nemmere til naboer.")]
    [Min(0.01f)] public float crossbreedWeight = 1f;
}

public class MutationSystem : MonoBehaviour
{
    public MutationEffect[] allMutations;

    private const float CommonWeight = 60f;
    private const float UncommonWeight = 25f;
    private const float RareWeight = 12f;
    private const float LegendaryWeight = 3f;

    public void RollMutations(Plant plant, WeatherType weather)
    {
        float chance = plant.data.mutationChanceBase;
        if (weather == WeatherType.AcidRain) chance = 1f;

        if (Random.value > chance) return;

        // Forsøg at fylde et tomt slot
        var emptySlots = GetEmptySlots(plant);
        if (emptySlots.Count == 0) return;

        // Vælg et tilfældigt tomt slot
        var targetSlot = emptySlots[Random.Range(0, emptySlots.Count)];

        var mutation = PickMutationForSlot(plant, targetSlot);
        if (mutation == null) return;

        plant.SetSlot(targetSlot, mutation);
        Debug.Log($"{plant.data.plantName} fik mutation: {mutation.mutationName} i {targetSlot}");
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

    private List<MutationSlotType> GetEmptySlots(Plant plant)
    {
        var empty = new List<MutationSlotType>();
        if (plant.IsSlotEmpty(MutationSlotType.HarvestDifficulty)) empty.Add(MutationSlotType.HarvestDifficulty);
        if (plant.IsSlotEmpty(MutationSlotType.Yield)) empty.Add(MutationSlotType.Yield);
        if (plant.IsSlotEmpty(MutationSlotType.Value)) empty.Add(MutationSlotType.Value);
        return empty;
    }

    private MutationEffect PickMutationForSlot(Plant plant, MutationSlotType targetSlot)
    {
        if (allMutations == null || allMutations.Length == 0) return null;

        // Byg vægtet liste – kun mutationer der matcher slotType og ikke allerede sidder i slottet
        var weighted = new List<(MutationEffect mutation, float weight)>();
        foreach (var m in allMutations)
        {
            if (m.slotType != targetSlot) continue;
            if (plant.GetSlot(targetSlot) == m) continue; // allerede i dette slot

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

        return weighted[^1].mutation;
    }
}