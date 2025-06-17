using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject containerPrefab;
    public int numberOfContainers = 10;

    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;

    public int maxItemsPerContainer = 3;

    void Start()
    {
        for (int i = 0; i < numberOfContainers; i++)
        {
            Vector2 spawnPos = new Vector2(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y)
            );

            GameObject chest = Instantiate(containerPrefab, spawnPos, Quaternion.identity);

            List<Item> items = ItemGenerator.GenerateRandomItems(maxItemsPerContainer);
            foreach (Item item in items)
            {
                //ChestInventory.AddItem(item);
                Debug.Log($"Предмет: {item.GetFullName()}");
                Debug.Log(item.GetDescription());
            }
        }
    }
}
