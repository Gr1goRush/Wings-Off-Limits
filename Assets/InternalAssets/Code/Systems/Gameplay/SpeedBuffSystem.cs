using System;
using UnityEngine;

public class SpeedBuffSystem : MonoBehaviour
{
    public static event Action OnSpeedBuffEnded;

    public static bool DoubleSpeed;
    public float speedBuffRemained;

    private void Update()
    {
        if (speedBuffRemained <= 0f || PauseManager.Paused) return;

        speedBuffRemained -= Time.deltaTime;
        DoubleSpeed = true;

        if (speedBuffRemained <= 0f)
        {
            DoubleSpeed = false;
            OnSpeedBuffEnded?.Invoke();
        }
    }

    private void OnEnable()
    {
        Projectile.onSpeedProjecileHit += AddTime;
    }

    private void OnDisable()
    {
        Projectile.onSpeedProjecileHit -= AddTime;
    }

    private void AddTime()
    {
        speedBuffRemained += 15f;
    }
}
