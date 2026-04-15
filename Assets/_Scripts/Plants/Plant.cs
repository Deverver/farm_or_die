using System.Collections.Generic;
using UnityEngine;
using FarmGame.Mutations;

[RequireComponent(typeof(SpriteRenderer))]
public class Plant : MonoBehaviour
{
    // ── Core data ──────────────────────────────────────────────────
    public PlantData data;
    public int growthStage = 0;
    public int currentHealth = 100;

    // ── Mutation slots ─────────────────────────────────────────────
    [Header("Mutation Slots")]
    [Tooltip("Slot 1 – HarvestDifficulty")]
    public MutationEffect slot1;

    [Tooltip("Slot 2 – Yield")]
    public MutationEffect slot2;

    [Tooltip("Slot 3 – Value")]
    public MutationEffect slot3;

    private SpriteRenderer _sr;

    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    // Bevaret fra eksisterende kode – kaldt af PlantSlot
    public void Initialize(PlantData plantData)
    {
        data = plantData;
        RefreshSprite();
        Debug.Log($"Plante initialiseret: {data.plantName}");
    }

    // ── Growth ─────────────────────────────────────────────────────
    public void Grow()
    {
        if (data == null || growthStage >= data.maxGrowthStage) return;
        growthStage++;
        Debug.Log($"Plant grew to stage: {growthStage}");
        RefreshSprite();
    }

    public bool IsReadyToHarvest() => data != null && growthStage >= data.maxGrowthStage;

    // ── Visual pipeline ────────────────────────────────────────────

    /// <summary>
    /// Fuld visuel refresh.
    /// Ved maxGrowthStage med aktiv mutation: vises familie-specifik mutations-sprite
    /// med Color.white (spriten har allerede sine egne farver).
    /// Ellers: greyscale vækst-sprite tintes med familiens farve.
    /// Kald ved: Initialize, Grow, SetSlot.
    /// </summary>
    public void RefreshSprite()
    {
        if (_sr == null) _sr = GetComponent<SpriteRenderer>();
        if (data == null) return;

        // Ved maxGrowthStage: tjek for mutations-sprite (har egne farver → ingen tint)
        if (growthStage >= data.maxGrowthStage)
        {
            Sprite mutSprite = ResolveMutationSprite();
            if (mutSprite != null)
            {
                _sr.sprite = mutSprite;
                _sr.color  = Color.white; // mutation-spriten er allerede farvelagt
                return;
            }
        }

        // Normale greyscale vækst-sprites: læg familie-tint på
        if (data.growthSprites != null && growthStage < data.growthSprites.Length)
            _sr.sprite = data.growthSprites[growthStage];

        _sr.color = GetFamilyTint();
    }

    /// <summary>
    /// Gennemgår slot1→slot2→slot3 og returnerer den første
    /// mutations-sprite der kan slås op i PlantData, eller null.
    /// </summary>
    private Sprite ResolveMutationSprite()
    {
        MutationEffect[] slots = { slot1, slot2, slot3 };
        foreach (var slot in slots)
        {
            if (slot == null) continue;
            Sprite s = data.GetMutationSprite(slot);
            if (s != null) return s;
        }
        return null;
    }

    /// <summary>
    /// Returnerer den greyscale-kompenserende farvetint for plantens familie.
    /// Bruges kun på normale vækst-sprites; mutations-sprites er allerede farvelagt.
    /// </summary>
    private Color GetFamilyTint()
    {
        if (data == null) return Color.white;
        return data.family switch
        {
            PlantFamily.Leafy  => new Color(0.78f, 1.00f, 0.78f), // blød grøn
            PlantFamily.Root   => new Color(0.88f, 0.74f, 0.55f), // varm brun
            PlantFamily.Fruit  => new Color(1.00f, 0.86f, 0.58f), // gylden/varm
            PlantFamily.Flower => new Color(0.88f, 0.74f, 1.00f), // lilla
            _                  => Color.white
        };
    }

    // ── Mutation slot API (bruges af CrossbreedingSystem) ──────────
    public MutationEffect GetSlot(MutationSlotType type) => type switch
    {
        MutationSlotType.HarvestDifficulty => slot1,
        MutationSlotType.Yield => slot2,
        MutationSlotType.Value => slot3,
        _ => null
    };

    public bool IsSlotEmpty(MutationSlotType type) => GetSlot(type) == null;

    public void SetSlot(MutationSlotType type, MutationEffect mutation)
    {
        switch (type)
        {
            case MutationSlotType.HarvestDifficulty: slot1 = mutation; break;
            case MutationSlotType.Yield: slot2 = mutation; break;
            case MutationSlotType.Value: slot3 = mutation; break;
        }
        RefreshSprite();
    }

    // ── Stat getters ───────────────────────────────────────────────
    public float GetHarvestDifficulty()
    {
        if (data == null) return 0f;
        float d = data.baseHarvestDifficulty;
        d += growthStage * 0.05f;
        if (slot1 != null) d += slot1.modifier;
        return Mathf.Clamp01(d);
    }

    public int GetFinalYield()
    {
        int y = data?.baseYield ?? 0;
        if (slot2 != null) y += Mathf.RoundToInt(slot2.modifier);
        return Mathf.Max(0, y);
    }

    public int GetFinalSellValue()
    {
        float v = data?.produceValue ?? 0;
        if (slot3 != null) v *= slot3.modifier;
        return Mathf.Max(0, Mathf.RoundToInt(v));
    }

    public string GetMutationSummary()
    {
        string s1 = slot1 != null ? slot1.mutationName : "–";
        string s2 = slot2 != null ? slot2.mutationName : "–";
        string s3 = slot3 != null ? slot3.mutationName : "–";
        return $"[S1:{s1}] [S2:{s2}] [S3:{s3}]";
    }
}
