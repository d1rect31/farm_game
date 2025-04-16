using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    [SerializeField] private Sprite[] growthStages; // Массив спрайтов для каждого этапа роста
    [SerializeField] private GameObject collectablePrefab; // Префаб для финального состояния
    [SerializeField] private float growthTime = 5f; // Время между этапами роста
    [SerializeField] private List<Harvestable.ItemEntry> itemsToHarvest = new(); // Список предметов и их количества

    private int currentStage = 0; // Текущее состояние роста
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (growthStages.Length > 0)
        {
            spriteRenderer.sprite = growthStages[currentStage]; // Установить начальный спрайт
        }
        StartCoroutine(Grow());
    }

    private IEnumerator Grow()
    {
        while (currentStage < growthStages.Length - 1)
        {
            yield return new WaitForSeconds(growthTime); // Ждать указанное время
            currentStage++;
            spriteRenderer.sprite = growthStages[currentStage]; // Обновить спрайт
        }

        // После достижения последнего этапа заменить на Collectable
        yield return new WaitForSeconds(growthTime);
        SpawnCollectable();
        Destroy(gameObject); // Удалить текущий объект
    }

    private void SpawnCollectable()
    {
        GameObject collectable = Instantiate(collectablePrefab, transform.position, Quaternion.identity);

        // Установить текстуру последнего состояния
        var collectableSpriteRenderer = collectable.GetComponent<SpriteRenderer>();
        if (collectableSpriteRenderer != null)
        {
            collectableSpriteRenderer.sprite = growthStages[growthStages.Length - 1];
        }

        // Присвоить массив предметов через компонент Harvestable
        var harvestable = collectable.GetComponent<Harvestable>();
        if (harvestable != null)
        {
            harvestable.ItemsToHarvest = itemsToHarvest; // Передать список предметов и их количества
        }
    }
}