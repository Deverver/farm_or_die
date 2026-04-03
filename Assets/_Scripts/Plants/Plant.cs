using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Plant : MonoBehaviour
{
    public int growthStage = 0;
    public int maxGrowthStage = 3;

    public Sprite[] growthSprites;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateSprite();
    }

    public void Grow()
    {
        if (growthStage < maxGrowthStage)
        {
            growthStage++;

            Debug.Log("Plant grew to stage: " + growthStage);

            UpdateSprite();
        }

        if (IsReadyToHarvest())
        {
            Debug.Log("Plant is ready to harvest!");
        }
    }

    void UpdateSprite()
    {
        if (growthSprites != null && growthStage < growthSprites.Length)
        {
            spriteRenderer.sprite = growthSprites[growthStage];
        }
    }

    public bool IsReadyToHarvest()
    {
        return growthStage >= maxGrowthStage;
    }
}