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
   
   [HideInInspector] public bool canSpawn = true;

   private void Start()
   {
    
   }

   private bool ValidateReferences()
   {
      if (arCamera == null || monsterPrefabs.Length == 0
                           || timeManager == null || locationManager == null)
      {
         Debug.LogError("MonsterSpawner: Missing references!");
         enabled = false;
         return false;         
      }
      return true;
   }

   private IEnumerator SpawnLoop()
   {
      while (canSpawn)
      {
         if (timeManager.IsNightTime() && locationManager.IsNearSpawnPoint())
         {
            int index = Random.Range(0, monsterPrefabs.Length);
            GameObject prefab = monsterPrefabs[index];

            Vector3 randomOffset = new Vector3(Random.Range(
               -1f, 1f), 0f, Random.Range(0.5f, 2f));
            
            Vector3 spawnPos = arCamera.transform.position +
                               arCamera.transform.forward * spawnDistance
                               + randomOffset;
            
            Instantiate(prefab, spawnPos, Quaternion.identity);
         }
         yield return new WaitForSeconds(spawnInterval);
      }
   }
}
