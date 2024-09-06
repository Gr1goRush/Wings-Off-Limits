using DG.Tweening;
using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Projectile : MonoBehaviour
{
    public static event Action onEnemyProjectileHit;
    public static event Action onRepairProjectileHit;
    public static event Action onSpeedProjecileHit;

    public ProjectileType Type;

    private float speed = 3f;

    private bool Used;

    [SerializeField, HideInInspector] private AudioSource _source;

    private void OnValidate()
    {
        _source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (SpeedBuffSystem.DoubleSpeed) DoubleSpeed();
    }

    private void OnEnable()
    {
        onSpeedProjecileHit += DoubleSpeed;
    }

    private void OnDisable()
    {
        onSpeedProjecileHit -= DoubleSpeed;
    }

    private void Update()
    {
        if (PauseManager.Paused || Used) return;

        transform.DOBlendableMoveBy(Vector2.down * Time.deltaTime * speed, Time.deltaTime);

        if (transform.position.x < -10)
        {
            DOTween.Clear(transform);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == PlaneController.instance.gameObject)
        {
            if (Used) return;
            Used = true;
            _source.Play();
            ExecuteEffect();

            transform.DOScale(Vector3.zero, 0.5f).onComplete = () => Destroy(gameObject);
        }
    }

    public virtual void ExecuteEffect()
    {
        switch (Type)
        {
            case ProjectileType.Enemy:
                onEnemyProjectileHit?.Invoke();
                break;

            case ProjectileType.Repair:
                onRepairProjectileHit?.Invoke(); 
                break;

            case ProjectileType.Speed:
                onSpeedProjecileHit?.Invoke();
                break;

            case ProjectileType.Money:
                MoneyHelper.AddMoney(1);
                break;
        }
    }

    public void DoubleSpeed()
    {
        speed = 6f;
    }
}

public enum ProjectileType
{
    Enemy,
    Repair,
    Speed,
    Money
}
