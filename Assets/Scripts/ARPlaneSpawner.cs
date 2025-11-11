using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;

public class ARPlaneSpawner : MonoBehaviour
{
    [Tooltip("Monster prefab")]
    public GameObject[] monsterPrefabs;
    public ARPlaneManager planeManager;

    public void SpawnOnRandomPlane()
    {
        // Collect all tracked planes
        List<ARPlane> planes = new List<ARPlane>();
        foreach (var plane in planeManager.trackables)
        {
            if (plane.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
                planes.Add(plane);
        }

        if (planes.Count == 0)
        {
            Debug.Log("No tracked planes available yet.");
            return;
        }

        // Choose a random plane
        ARPlane randomPlane = planes[Random.Range(0, planes.Count)];
        Vector3 randomPoint = randomPlane.center + new Vector3(
            Random.Range(-0.5f, 0.5f),
            0,
            Random.Range(-0.5f, 0.5f)
        );

        // Monster prefabs
        if (monsterPrefabs.Length > 0)
        {
            GameObject randomMonster = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];
            Instantiate(randomMonster, randomPoint, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No monster prefabs assigned!");
        }
    }
}