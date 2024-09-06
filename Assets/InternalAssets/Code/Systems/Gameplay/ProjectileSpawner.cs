using System.Collections;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private Projectile[] projectilesPrefab;

    [SerializeField] private Transform[] projectileSpawnPoints;

    private Projectile randomProjectile => projectilesPrefab[Random.Range(0, projectilesPrefab.Length)];
    private Transform randomSpawnPoint => projectileSpawnPoints[Random.Range(0, projectileSpawnPoints.Length)];

    private Coroutine WorkRoutine;

    [Header("Config")]
    [SerializeField, Tooltip("Минимальное время в секундах, до появления нового проджектайла")] private float MinSpawnTime = 3;
    [SerializeField, Min(0.01f), Tooltip("Максимальное время в секундах, до появления нового проджектайла")] private float MaxSpawnTime = 5;

    private void OnValidate()
    {
        if (MinSpawnTime > MaxSpawnTime) MinSpawnTime = MaxSpawnTime;
    }

    public void Execute()
    {
        WorkRoutine = StartCoroutine(SpawnProjectilesRoutine());
    }

    public void Stop()
    {
        StopCoroutine(WorkRoutine);
    }

    private IEnumerator SpawnProjectilesRoutine()
    {
        while (true)
        {
            if (!PauseManager.Paused)
            {
                yield return new WaitForSeconds(Random.Range(MinSpawnTime, MaxSpawnTime));
                Instantiate(randomProjectile, randomSpawnPoint.position, Quaternion.identity);
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
