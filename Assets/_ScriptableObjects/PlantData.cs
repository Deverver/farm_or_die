using UnityEngine;

/// <summary>
/// Binder en mutations-nøgle (fx "fire", "ice") til den korrekte sprite
/// for denne specifikke plantefamilie ved maxGrowthStage.
/// Udfyldes i Inspector for hvert PlantData-asset.
/// </summary>
[System.Serializable]
public struct MutationSpriteEntry
{
    [Tooltip("Matcher MutationEffect.mutationSpriteKey (fx 'fire', 'ice').")]
    public string key;

    [Tooltip("Sprite der vises ved maxGrowthStage når denne mutationstype er aktiv.")]
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

    [Header("Mutation Sprites")]
    [Tooltip("Map mutations-nøgler til den rigtige familie-sprite ved maxGrowthStage.\n" +
             "Eks.: key='fire' → LeafPlant_3_fire  |  key='ice' → LeafPlant_3_ice")]
    public MutationSpriteEntry[] mutationSprites;

    /// <summary>
    /// Returnerer mutation-sprite for den givne nøgle, eller null hvis ingen match.
    /// </summary>
    public Sprite GetMutationSprite(string key)
    {
        if (mutationSprites == null || string.IsNullOrEmpty(key)) return null;
        foreach (var entry in mutationSprites)
        {
            if (string.Equals(entry.key, key, System.StringComparison.OrdinalIgnoreCase))
                return entry.sprite;
        }
        return null;
    }
}
