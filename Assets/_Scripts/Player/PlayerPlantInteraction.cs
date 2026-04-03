using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private PlantSlot currentSlot;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentSlot != null)
            {
                if (!currentSlot.IsOccupied())
                {
                    currentSlot.Plant();
                }
                else
                {
                    currentSlot.Harvest();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlantSlot slot = other.GetComponent<PlantSlot>();
        if (slot != null)
        {
            currentSlot = slot;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlantSlot slot = other.GetComponent<PlantSlot>();
        if (slot != null)
        {
            currentSlot = null;
        }
    }
}