using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public UIManager uiManager;

    public int maxEnergy = 100;
    public int currentEnergy;

    public int maxHealth = 100;
    public int currentHealth;

    public int currentMoney = 0;

    void Start()
    {
        currentEnergy = maxEnergy;
        currentHealth = maxHealth;

        uiManager.UpdateEnergy(currentEnergy);
        uiManager.UpdateHealth(currentHealth);
        uiManager.UpdateMoney(currentMoney);
    }

    public void RegenerateEnergy(int amount)
    {
        currentEnergy += amount;
        if (currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;

        }
        uiManager.UpdateEnergy(currentEnergy);
        Debug.Log("Energy: " + currentEnergy);
    }



    public bool UseEnergy(int amount)
    {
        if (currentEnergy >= amount)
        {
            currentEnergy -= amount;
            uiManager.UpdateEnergy(currentEnergy);

            Debug.Log("Energy: " + currentEnergy);
            return true;
        }
        else
        {
            Debug.Log("Not enough energy!");
            return false;
        }
    }
}