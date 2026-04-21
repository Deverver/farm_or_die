    using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class UIManager : MonoBehaviour
{
    public UIDocument ui;
    public UIDocument statUI;
    private VisualElement inventory;
    private Label dayText;
    private Label energyText;
    private Label healthText;
    private Label moneyText;
    private Label radiationText;


    public void UpdateRadiation(float currentRadiation)
    {
        radiationText.text = "Radiation: " + Mathf.RoundToInt(currentRadiation) + "%";
    }

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

    void Start()
    {
        inventory = ui.rootVisualElement.Q<VisualElement>("inventory");
        dayText = statUI.rootVisualElement.Q<Label>("DayText");
        energyText = statUI.rootVisualElement.Q<Label>("EnergyText");
        healthText = statUI.rootVisualElement.Q<Label>("HealthText");
        moneyText = statUI.rootVisualElement.Q<Label>("MoneyText");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.style.display = inventory.style.display == DisplayStyle.None ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}