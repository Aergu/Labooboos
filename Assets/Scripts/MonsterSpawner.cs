using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterSpawner : MonoBehaviour
{
    public TimeManager timeManager;
    public LocationManager locationManager;
    public Camera arCamera;
    public ARPlaneSpawner planeSpawner;

    public float spawnDistance, checkInterval;
    public float spawnInterval = 10f;

    [Header("Monster Prefabs")] 
    public GameObject[] monsterPrefabs;
    private GameObject spawnedMonster;

    private float timer;

    private void Start()
    {
        if (timeManager == null || arCamera == null || monsterPrefabs.Length == 0)
        {
            Debug.LogError("MonsterSpawner references not set!");
            enabled = false;
            return;
        }

        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            bool canSpawn = timeManager.IsNightTime() && locationManager.IsNearSpawnPoint();

            if (canSpawn && spawnedMonster == null)
            {
                int index = Random.Range(0, monsterPrefabs.Length);
                GameObject chosenPrefab = monsterPrefabs[index];

                Vector3 spawnPos = arCamera.transform.position +
                                   arCamera.transform.forward * spawnDistance;
                spawnedMonster = Instantiate(chosenPrefab, spawnPos, Quaternion.identity);
            }
            else if (!canSpawn && spawnedMonster != null)
            {
                Destroy(spawnedMonster);
                spawnedMonster = null;
            }

            yield return new WaitForSeconds(checkInterval);
        }
    }
}
