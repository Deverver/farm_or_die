using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using FarmGame.Mutations;

public class CrossbreedingSystem : MonoBehaviour
{
    [Header("Mutation Pools")]
    public MutationEffect[] slot1Pool; // HarvestDifficulty
    public MutationEffect[] slot2Pool; // Yield
    public MutationEffect[] slot3Pool; // Value

    [Header("Tuning")]
    [Range(0f, 0.5f)]
    public float infectionChancePerNeighbor = 0.15f;
    public float adjacencyRadius = 1.5f; // tilpas til din grid-spacing

    private static readonly MutationSlotType[] AllSlots =
    {
        MutationSlotType.HarvestDifficulty,
        MutationSlotType.Yield,
        MutationSlotType.Value
    };

    // Kaldt af DaySystem.EndDay() efter plantevækst
    public void ProcessCrossbreeding()
    {
        var allSlots = FindObjectsByType<PlantSlot>(FindObjectsSortMode.None);

        foreach (var plantSlot in allSlots)
        {
            if (!plantSlot.IsOccupied()) continue;
            Plant plant = plantSlot.GetPlant();
            List<Plant> neighbors = GetAdjacentPlants(plantSlot, allSlots);

            foreach (var slotType in AllSlots)
            {
                if (!plant.IsSlotEmpty(slotType)) continue;

                MutationEffect result = RollMutationForSlot(plant, slotType, neighbors);
                if (result != null)
                {
                    plant.SetSlot(slotType, result);
                    Debug.Log($"[Crossbreeding] {plant.data.plantName} fik '{result.mutationName}' " +
                              $"({slotType}) | {plant.GetMutationSummary()}");
                }
            }
        }
    }

    private MutationEffect RollMutationForSlot(
        Plant plant, MutationSlotType slotType, List<Plant> neighbors)
    {
        float ownChance = plant.data.mutationChanceBase;
        var infecting = neighbors.Where(n => !n.IsSlotEmpty(slotType)).ToList();

        float totalChance = Mathf.Clamp01(ownChance + infecting.Count * infectionChancePerNeighbor);
        if (Random.value > totalChance) return null;

        bool useOwnPool = infecting.Count == 0 || Random.value < (ownChance / totalChance);
        return useOwnPool ? PickFromPool(slotType) : PickFromNeighbors(infecting, slotType);
    }

    private MutationEffect PickFromPool(MutationSlotType slotType)
    {
        var pool = slotType switch
        {
            MutationSlotType.HarvestDifficulty => slot1Pool,
            MutationSlotType.Yield => slot2Pool,
            MutationSlotType.Value => slot3Pool,
            _ => null
        };
        if (pool == null || pool.Length == 0) return null;
        return WeightedRandom(pool, m => m.crossbreedWeight);
    }

    private MutationEffect PickFromNeighbors(List<Plant> neighbors, MutationSlotType slotType)
    {
        var candidates = neighbors.Select(n => n.GetSlot(slotType)).Where(m => m != null).ToList();
        return WeightedRandom(candidates, m => m.crossbreedWeight);
    }

    private static T WeightedRandom<T>(IList<T> items, System.Func<T, float> weight)
    {
        float total = items.Sum(weight), roll = Random.value * total, cursor = 0f;
        foreach (var item in items) { cursor += weight(item); if (roll <= cursor) return item; }
        return items[items.Count - 1];
    }

    private List<Plant> GetAdjacentPlants(PlantSlot source, PlantSlot[] allSlots)
    {
        var result = new List<Plant>();
        Vector2 origin = source.transform.position;
        foreach (var other in allSlots)
        {
            if (other == source || !other.IsOccupied()) continue;
            if (Vector2.Distance(origin, other.transform.position) < adjacencyRadius)
                result.Add(other.GetPlant());
        }
        return result;
    }
}
