using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Fishing : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private ItemData Fish;

    private bool inRange = false;

    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.F))
        {
            GetFish();
        }
    }

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    protected void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            inRange = false;
        }
    }


    public void GetFish()
    {
        if (playerStats.UseEnergy(10))
        {
            inventory.AddItem(Fish, 1);
            Debug.Log("You got a fish!");
            getFishEvent.Invoke();
        }
    }

    [field: SerializeField]
    public UnityEvent getFishEvent { get; private set; }
}