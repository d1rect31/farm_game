using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    [SerializeField] private Sprite[] growthStages;
    [SerializeField] private GameObject collectablePrefab;
    [SerializeField] private float growthTime = 5f;
    [SerializeField] private List<Harvestable.ItemEntry> itemsToHarvest = new();

    private int currentStage = 0; 
    private SpriteRenderer spriteRenderer;

    private Plantable plantingSlot;

    public void SetPlantingSlot(Plantable slot)
    {
        plantingSlot = slot;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (growthStages.Length > 0)
        {
            spriteRenderer.sprite = growthStages[currentStage];
        }
        StartCoroutine(Grow());
    }

    private IEnumerator Grow()
    {
        while (currentStage < growthStages.Length - 1)
        {
            yield return new WaitForSeconds(growthTime);
            currentStage++;
            spriteRenderer.sprite = growthStages[currentStage];
        }

        // Replace the crop with the collectable object
        SpawnObject();
        Destroy(gameObject);
    }

    private void SpawnObject()
    {
        GameObject collectable = Instantiate(collectablePrefab, transform.position, Quaternion.identity);

        // Setting the sprite of the collectable depending on the last growth stage
        var collectableSpriteRenderer = collectable.GetComponent<SpriteRenderer>();
        if (collectableSpriteRenderer != null)
        {
            collectableSpriteRenderer.sprite = growthStages[growthStages.Length - 1];
        }

        // Assinging the items to harvest
        var harvestable = collectable.GetComponent<Harvestable>();
        if (harvestable != null)
        {
            harvestable.ItemsToHarvest = itemsToHarvest; // Передать список предметов и их количества
        }

        // Free the planting slot
        if (plantingSlot != null)
        {
            plantingSlot.GetType().GetField("isOccupied", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(plantingSlot, false);
        }
    }
}
