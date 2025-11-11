using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public TimeManager timeManager;
    public LocationManager locationManager;
    public ARPlaneSpawner planeSpawner;
    public float spawnInterval = 10f;

    private float timer;

    void Update()
    {
        if (!timeManager.IsNightTime()) return;
        if (!locationManager.IsNearSpawnPoint()) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            planeSpawner.SpawnOnRandomPlane();
        }
    }
}