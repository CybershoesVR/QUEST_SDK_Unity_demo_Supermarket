using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] GameObject bombPrefab;
    [SerializeField] Transform[] spawnPoints;

    private List<int> usedPoints = new List<int>();

    
    void Start()
    {
        SpawnNext(2);
    }

    public void SpawnNext(float delay=5)
    {
        Invoke("ExecuteSpawn", delay);
    }

    void ExecuteSpawn()
    {
        int index;

        if (usedPoints.Count >= spawnPoints.Length)
        {
            usedPoints = new List<int>();
        }

        do
        {
            index = Random.Range(0, spawnPoints.Length);

        } while (usedPoints.Contains(index));

        usedPoints.Add(index);

        Instantiate(bombPrefab, spawnPoints[index].position, Quaternion.Euler(-65.328f,0,0));
    }
}
