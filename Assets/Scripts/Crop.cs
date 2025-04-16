using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    [SerializeField] private Sprite[] growthStages; // ������ �������� ��� ������� ����� �����
    [SerializeField] private GameObject collectablePrefab; // ������ ��� ���������� ���������
    [SerializeField] private float growthTime = 5f; // ����� ����� ������� �����
    [SerializeField] private List<Harvestable.ItemEntry> itemsToHarvest = new(); // ������ ��������� � �� ����������

    private int currentStage = 0; // ������� ��������� �����
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (growthStages.Length > 0)
        {
            spriteRenderer.sprite = growthStages[currentStage]; // ���������� ��������� ������
        }
        StartCoroutine(Grow());
    }

    private IEnumerator Grow()
    {
        while (currentStage < growthStages.Length - 1)
        {
            yield return new WaitForSeconds(growthTime); // ����� ��������� �����
            currentStage++;
            spriteRenderer.sprite = growthStages[currentStage]; // �������� ������
        }

        // ����� ���������� ���������� ����� �������� �� Collectable
        yield return new WaitForSeconds(growthTime);
        SpawnCollectable();
        Destroy(gameObject); // ������� ������� ������
    }

    private void SpawnCollectable()
    {
        GameObject collectable = Instantiate(collectablePrefab, transform.position, Quaternion.identity);

        // ���������� �������� ���������� ���������
        var collectableSpriteRenderer = collectable.GetComponent<SpriteRenderer>();
        if (collectableSpriteRenderer != null)
        {
            collectableSpriteRenderer.sprite = growthStages[growthStages.Length - 1];
        }

        // ��������� ������ ��������� ����� ��������� Harvestable
        var harvestable = collectable.GetComponent<Harvestable>();
        if (harvestable != null)
        {
            harvestable.ItemsToHarvest = itemsToHarvest; // �������� ������ ��������� � �� ����������
        }
    }
}