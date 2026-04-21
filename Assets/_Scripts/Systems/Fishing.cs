using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Fishing : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private ItemData Fish;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            inventory.AddItem(Fish, 1);
            playerStats.UseEnergy(10);
            GetFish();
        }
    }


    public void GetFish()
    {
        Debug.Log("You got a fish!");
        
        getFishEvent.Invoke();
    }

    [field: SerializeField]

    public UnityEvent getFishEvent { get; private set; }
}
