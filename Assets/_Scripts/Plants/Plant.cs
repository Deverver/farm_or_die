using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public PlantData data;
    public int growthStage = 0;
    public List<MutationEffect> mutations = new();
    public int currentHealth = 100;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        // Caches komponent tidligt – klar FØR PlantSlot kalder Initialize()
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Kaldes eksplicit af PlantSlot i stedet for at stole på Start()-timing
    public void Initialize(PlantData plantData)
    {
        data = plantData;
        UpdateSprite();
        Debug.Log($"Plante initialiseret: {data.plantName}");
    }

    public void Grow()
    {
        if (data == null || growthStage >= data.maxGrowthStage) return;
        growthStage++;
        Debug.Log($"Plant grew to stage: {growthStage}");
        UpdateSprite();
    }

    public bool IsReadyToHarvest() => data != null && growthStage >= data.maxGrowthStage;

    public float GetHarvestDifficulty()
    {
        if (data == null) return 0f;
        float d = data.baseHarvestDifficulty;
        d += growthStage * 0.05f;
        d += mutations.Count * 0.03f;
        return Mathf.Clamp01(d);
    }

    private void UpdateSprite()
    {
        if (data?.growthSprites == null || spriteRenderer == null) return;
        if (growthStage < data.growthSprites.Length)
            spriteRenderer.sprite = data.growthSprites[growthStage];
    }
}
