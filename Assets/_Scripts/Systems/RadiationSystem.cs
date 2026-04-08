using UnityEngine;

public class RadiationSystem : MonoBehaviour
{
    public float currentLevel = 0f;
    public float dailyIncrease = 2f;
    public float criticalThreshold = 85f;

    public void ApplyDailyRadiation(PlayerStats player)
    {
        currentLevel += dailyIncrease;
        currentLevel = Mathf.Clamp(currentLevel, 0f, 100f);

        if (currentLevel >= criticalThreshold)
            GameManager.Instance.GameOver();
        else if (currentLevel > 50f)
            player.TakeDamage(Mathf.RoundToInt((currentLevel - 50f) * 0.5f));

        GameManager.Instance.uiManager.UpdateRadiation(currentLevel);
    }

    public void ReduceRadiation(float amount)
    {
        currentLevel = Mathf.Max(0, currentLevel - amount);
    }
}
