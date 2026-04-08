using UnityEngine;

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
}
