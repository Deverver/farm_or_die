using UnityEngine;

/// <summary>
/// Overskriver mutations-spriten for en specifik MutationEffect på denne plantefamilie.
/// Sættes i Inspector på hvert PlantData-asset — fx LeafPlant_3_fire til Fire-mutationen.
/// Hvis intet override er sat, bruges MutationEffect.mutatedSprite som fallback.
/// </summary>
[System.Serializable]
public struct MutationSpriteOverride
{
    [Tooltip("Den MutationEffect dette override gælder for.")]
    public MutationEffect mutation;

    [Tooltip("Plante-specifik sprite ved maxGrowthStage (fx LeafPlant_3_fire).")]
    public Sprite sprite;
}

[CreateAssetMenu(menuName = "Farm/PlantData")]
public class PlantData : ScriptableObject
{
    public string plantName;
    public PlantFamily family;        // Enum: Leafy, Root, Fruit, Flower
    public int maxGrowthStage = 4;    // 0=Seed → 4=Mature
    public int baseYield = 3;
    public float baseHarvestDifficulty = 0.1f; // 0-1, påvirker damage til spiller
    public int maxMutationSlots = 3;
    public float mutationChanceBase = 0.05f;
    public float healAmountPerProduce = 10f;
    public Sprite[] growthSprites;
    public int seedCost = 5;          // Pris i shop
    public int produceValue = 8;      // Salgspris pr. produce

    [Header("Mutation Sprite Overrides")]
    [Tooltip("Plante-specifikke sprites per mutation.\n" +
             "Hvis tomt bruges MutationEffect.mutatedSprite som fallback.\n" +
             "Eks.: Fire-mutation → LeafPlant_3_fire")]
    public MutationSpriteOverride[] mutationOverrides;

    /// <summary>
    /// Returnerer den korrekte mutations-sprite for denne plantefamilie.
    /// Tjekker plantens egne overrides først — falder tilbage på mutationens default sprite.
    /// </summary>
    public Sprite GetMutationSprite(MutationEffect mutation)
    {
        if (mutation == null) return null;

        if (mutationOverrides != null)
        {
            foreach (var entry in mutationOverrides)
            {
                if (entry.mutation == mutation && entry.sprite != null)
                    return entry.sprite;
            }
        }

        // Fallback: brug spriten direkte fra MutationEffect-asset
        return mutation.mutatedSprite;
    }
}

