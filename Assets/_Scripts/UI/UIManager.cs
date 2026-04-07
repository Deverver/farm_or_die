using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI moneyText;


    public void UpdateDay(int day)
    {
        dayText.text = "Day: " + day;
    }

    public void UpdateEnergy(int energy)
    {
        energyText.text = "Energy: " + energy;
    }

    public void UpdateHealth(int health)
    {
        healthText.text = "Health: " + health;
    }

    public void UpdateMoney(int money)
    {
        moneyText.text = "Money: " + money;
    }
}