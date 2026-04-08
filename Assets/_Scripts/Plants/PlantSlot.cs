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

    public void Plant(PlantData plantData)
    {
        if (IsOccupied()) return;
        if (plantData == null) { Debug.LogError("PlantData er null – tjek at ItemData.seedFor er sat!"); return; }

        currentPlant = Instantiate(PlantPrefab, transform.position, Quaternion.identity);
        currentPlant.GetComponent<Plant>().Initialize(plantData); // ← eksplicit kald
    }



    public bool isReadyToHarvest()
    {
        if (!IsOccupied()) return false;

        Plant plantScript = currentPlant.GetComponent<Plant>();

        if (plantScript != null && plantScript.IsReadyToHarvest())
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public bool Harvest()
    {
        Destroy(currentPlant);
        currentPlant = null;

        Debug.Log("Plant harvested");
        return true;
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

    public Plant GetCurrentPlant()
    {
        return currentPlant != null ? currentPlant.GetComponent<Plant>() : null;
    }

}
