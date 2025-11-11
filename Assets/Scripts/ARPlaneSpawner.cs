using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;

public class ARPlaneSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public ARPlaneManager planeManager;

    public void SpawnOnRandomPlane()
    {
        // Gather all planes manually from the TrackableCollection (I'm unsure if I did this right)
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

        // Pick a random plane and spawn the monster near its center
        ARPlane randomPlane = planes[Random.Range(0, planes.Count)];
        Vector3 randomPoint = randomPlane.center + new Vector3(
            Random.Range(-0.5f, 0.5f),
            0,
            Random.Range(-0.5f, 0.5f)
        );

        Instantiate(monsterPrefab, randomPoint, Quaternion.identity);
    }
}