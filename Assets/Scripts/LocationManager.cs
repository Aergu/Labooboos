using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SpawnLocation
{
    public double latitude;
    public double longitude;
}

public class LocationManager : MonoBehaviour
{
    public List<SpawnLocation> spawnPoints = new List<SpawnLocation>();
    public float activeRadius = 30f; // meters

    private double currentLat;
    private double currentLon;

    void Start()
    {
        StartCoroutine(StartLocationService());
    }

    public bool IsNightTime(int nightStartHour = 19, int nightEndHour = 6)
    {
        int currentHour = System.DateTime.Now.Hour;

        if (nightStartHour > nightEndHour)
            return currentHour >= nightStartHour || currentHour < nightEndHour;
        else
            return currentHour >= nightStartHour && currentHour < nightEndHour;
    }

    public bool CanSpawn()
    {
        return IsNearSpawnPoint() && IsNightTime();
    }

    IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.LogWarning("Location not enabled by user");
            yield break;
        }

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogWarning("Unable to determine device location");
            yield break;
        }

        // Keep updating location every few seconds
        StartCoroutine(UpdateLocation());
    }

    IEnumerator UpdateLocation()
    {
        while (true)
        {
            if (Input.location.status == LocationServiceStatus.Running)
            {
                currentLat = Input.location.lastData.latitude;
                currentLon = Input.location.lastData.longitude;
            }
            yield return new WaitForSeconds(5f);
        }
    }

    // ✅ Method MonsterSpawner calls
    public bool IsNearSpawnPoint()
    {
        foreach (var point in spawnPoints)
        {
            if (Distance(currentLat, currentLon, point.latitude, point.longitude) < activeRadius)
                return true;
        }
        return false;
    }

    private float Distance(double lat1, double lon1, double lat2, double lon2)
    {
        const float R = 6371000f; // Earth's radius (meters)
        float dLat = Mathf.Deg2Rad * (float)(lat2 - lat1);
        float dLon = Mathf.Deg2Rad * (float)(lon2 - lon1);

        float a = Mathf.Sin(dLat / 2) * Mathf.Sin(dLat / 2) +
                  Mathf.Cos(Mathf.Deg2Rad * (float)lat1) * Mathf.Cos(Mathf.Deg2Rad * (float)lat2) *
                  Mathf.Sin(dLon / 2) * Mathf.Sin(dLon / 2);
        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        return R * c;
    }
}
