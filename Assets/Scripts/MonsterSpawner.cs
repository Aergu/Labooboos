using System.Collections;
using UnityEngine;



public class MonsterSpawner : MonoBehaviour
{
   [Header("Prefabs & Spawn Settings")]public GameObject[] monsterPrefabs;
   public float spawnInterval, spawnDistance;
   
   [Header("References")]
   public Camera arCamera;
   public TimeManager timeManager;
   public LocationManager locationManager;

   private void Start()
   {
      if (arCamera == null || monsterPrefabs == null ||
          monsterPrefabs.Length == 0 || locationManager == null)
      {
         Debug.LogError("MonsterSpawner: one or more references are missing!");
         enabled = false;
         return;
      }

      StartCoroutine(SpawnLoop());
   }

   private IEnumerator SpawnLoop()
   {
      while (true)
      {
         bool canSpawn = timeManager.IsNightTime()
                         && locationManager.IsNearSpawnPoint();

         if (canSpawn)
         {
            int index = UnityEngine.Random.Range(0, monsterPrefabs.Length);
            GameObject prefab = monsterPrefabs[index];
            
            Vector3 spawnPos = arCamera.transform.position +
                               arCamera.transform.forward * spawnDistance;
            
            Instantiate(prefab, spawnPos, Quaternion.identity);
         }
         yield return new WaitForSeconds(spawnInterval);
      }
   }
}
