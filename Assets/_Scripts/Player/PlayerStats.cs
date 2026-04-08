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

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        uiManager.UpdateHealth(currentHealth);
        if (currentHealth <= 0) GameManager.Instance.GameOver();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        uiManager.UpdateHealth(currentHealth);
    }

    public void EarnMoney(int amount)
    {
        currentMoney += amount;
        uiManager.UpdateMoney(currentMoney);
    }

    public bool SpendMoney(int amount)
    {
        if (currentMoney < amount) return false;
        currentMoney -= amount;
        uiManager.UpdateMoney(currentMoney);
        return true;
    }


}