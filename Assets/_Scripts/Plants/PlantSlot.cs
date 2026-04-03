using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlantSlot : MonoBehaviour
{

    public GameObject PlantPrefab;
    private GameObject currentPlant;

    public bool IsOccupied()
    {
        return currentPlant != null;
    }

    public void Plant()
    {
        if (IsOccupied()) return;

        currentPlant = Instantiate(PlantPrefab, transform.position, Quaternion.identity);
    }

    public void Harvest()
    {
        if (!IsOccupied()) return;

        Plant plantScript = currentPlant.GetComponent<Plant>();

        if (plantScript != null && plantScript.IsReadyToHarvest())
        {
            Destroy(currentPlant);
            currentPlant = null;

            Debug.Log("Plant harvested");
        }
        else
        {
            Debug.Log("Plant is not ready yet!");
        }
    }

    public void GrowPlant()
    {
        if (currentPlant == null) return;
        
        Plant plantScript = currentPlant.GetComponent<Plant>();
        if (plantScript != null)
        {
            plantScript.Grow();
            Debug.Log("Growing plant in slot: " + gameObject.name);
        }
    }
}
