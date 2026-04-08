using UnityEngine;

[CreateAssetMenu(menuName = "Farm/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public ItemType itemType; // Seed, Fertilizer, Food, Fish, Artifact

    // Frø
    public PlantData seedFor;

    // Mad
    public float healAmount;
    public float energyRestore;

    // Gødning
    public float mutationChanceMod; // positiv = øget, negativ = reduceret

    // Salg
    public int sellValue;
}
