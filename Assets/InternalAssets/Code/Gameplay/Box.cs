using DG.Tweening;
using System;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Box : MonoBehaviour
{
    public static event Action<Box> OnBoxDestroyed;

    [SerializeField] private int _maxEndurance;
    [SerializeField] private bool _isRich;
    [SerializeField] private bool _isStrong;

    [SerializeField, Space(10f)] private Rigidbody2D _rb;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private ResourceSpriteAnimator _spriteAnimator;
    [SerializeField] private SpriteRenderer _rend;
    [Space(10f)]
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip[] _clips;

    private Vector3 chachedScale = Vector3.zero;
    private bool canTakeHit = true;
    private int endurance;
    private bool isInited;

    private Sprite _chachedSprite;

    public bool IsRich => _isRich;
    public bool IsStrong => _isStrong;

    private float velocity => Mathf.Abs(_rb.velocity.x) + Mathf.Abs(_rb.velocity.y);

    public string[] hitAnimsPath;
    public string CurrentAnimPath
    {
        get
        {
            return hitAnimsPath[Math.Clamp(endurance, 0, hitAnimsPath.Length)];
        }
        set
        {
            return;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        
        if (IsRich)
        {
            Handles.Label(transform.position, "GOLDEN");
        }
    }
#endif

    private void OnValidate()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _audio = GetComponent<AudioSource>();
        _spriteAnimator = GetComponent<ResourceSpriteAnimator>();
        _rend = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Projectile.onEnemyProjectileHit += TakeHit;
        Projectile.onRepairProjectileHit += Repair;
    }
    private void OnDisable()
    {
        Projectile.onEnemyProjectileHit -= TakeHit;
        Projectile.onRepairProjectileHit -= Repair;
    }

    public Box Init()
    {
        _chachedSprite = _rend.sprite;
        endurance = _maxEndurance;
        chachedScale = transform.localScale;
        _collider.enabled = false;
        Vector3 startPos = transform.position;
        transform.position += Vector3.down * 10 + Vector3.right * UnityEngine.Random.Range(-3f, 3f);
        transform.DOMove(startPos, 2f)
            .SetEase(Ease.OutSine)
            .onComplete = () =>
            {
                transform.parent = null;
                _collider.enabled = true;
            };
        isInited = true;

        if (IsRich)
        {
            _rend.color = Color.yellow;
        }
        return this;
    }

    private void Update()
    {
        if (PauseManager.Paused)
        {
            _rb.velocity = Vector3.zero;
        }
        if (transform.position.y < -30 && isInited)
        {
            TakeHit();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PauseManager.Paused) return;

        if (collision.gameObject.GetComponent<Box>() != null)
        {
            if (velocity > 1.4 && canTakeHit)
                TakeHit();
        }
    }

    public void TakeHit()
    {
        if (!IsStrong) endurance--;

        VibroHelper.PlayVibro();
        _audio.PlayOneShot(_clips[UnityEngine.Random.Range(0, _clips.Length)]);

        if (hitAnimsPath.Length > 0 && !IsStrong)
        {
            _spriteAnimator.LoadAndPlay(CurrentAnimPath);
        }


        if (endurance <= 0)
        {

            canTakeHit = false;


        }
        else
        {
            canTakeHit = false;

        }
    }

    public void Repair()
    {

        _rend.sprite = _chachedSprite;
        endurance = Math.Clamp(endurance + _maxEndurance / 3, 0, _maxEndurance);

    }

    public void SetDeadData()
    {
        if (endurance <= 0)
        {
            OnBoxDestroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
