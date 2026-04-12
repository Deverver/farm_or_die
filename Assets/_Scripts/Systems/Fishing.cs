using UnityEngine;
using UnityEngine.Events;

public class Fishing : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void OnTriggerEnter(Collider fishingSpot)
    {
        if (fishingSpot.CompareTag("Player"))
        {
            GetFish();
        }
    }

    public void GetFish()
    {
        Debug.Log("You got a fish!");
        // brug addItem i Inventory til at give spilleren en fisk
        getFishEvent.Invoke();
    }

    [field: SerializeField]

    public UnityEvent getFishEvent { get; private set; }
}
