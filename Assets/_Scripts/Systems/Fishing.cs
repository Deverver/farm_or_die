using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Fishing : MonoBehaviour
{

    private Inventory inventory;
    public ItemData Fish;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        inventory = GetComponent<Inventory>();
    }
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
