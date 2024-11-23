using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject circlePrefab; // Prefab for circles
    public int numberOfCircles = 5; // Number of circles to spawn
    public List<GameObject> circles = new List<GameObject>(); // Store spawned circles

    void Start()
    {
        SpawnCircles();
    }

    public void SpawnCircles()
    {
        foreach (var circle in circles)
        {
            if (circle != null)
                Destroy(circle); // Clear existing circles
        }
        circles.Clear();

        for (int i = 0; i < numberOfCircles; i++)
        {
            float xPos = Random.Range(-7f, 7f);
            float yPos = Random.Range(-4f, 4f);
            Vector3 position = new Vector3(xPos, yPos, 0);

            GameObject circle = Instantiate(circlePrefab, position, Quaternion.identity);
            circles.Add(circle);
        }
    }
}
