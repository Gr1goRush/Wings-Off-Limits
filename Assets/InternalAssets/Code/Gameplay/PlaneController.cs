using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public static PlaneController instance;

    [SerializeField, HideInInspector] private Rigidbody2D rigidBody;
    [SerializeField] private float _panSpeed;
    private float _chachedPanSpeed;
    [SerializeField] private Animator _animator;
    private bool _isActive = false;
    private float _startX;

    public float _limitX;

    private void OnDrawGizmos()
    {
        Vector3 drawPosition = Vector3.zero;
        drawPosition.x = _limitX;
        Gizmos.DrawSphere(drawPosition, 1f);
        drawPosition.x = -_limitX;
        Gizmos.DrawSphere(drawPosition, 1f);
    }

    private void OnValidate()
    {
        rigidBody ??= GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Projectile.onSpeedProjecileHit += DoubleSpeed;
    }

    private void OnDisable()
    {
        Projectile.onSpeedProjecileHit -= DoubleSpeed;
    }

    public PlaneController Init()
    {
        instance = this;
        _chachedPanSpeed = _panSpeed;
        _isActive = true;

        return this;
    }

    private void Update()
    {
        if (!_isActive || PauseManager.Paused) { return; }
        _animator.SetFloat("XVelocity", rigidBody.velocity.x);
        HitIfCollideWall();
        if (Input.touchCount > 0 )
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                _startX = touch.position.x;
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
               if (touch.position.x < _startX)
                {
                    if (rigidBody.velocity.x > 0) RestoreVelocity();
                    rigidBody.AddForce(Vector2.left * Time.deltaTime * _panSpeed, ForceMode2D.Force);
                }
               else if (touch.position.x > _startX) 
                {
                    if (rigidBody.velocity.x < 0) RestoreVelocity();
                    rigidBody.AddForce(Vector2.right * Time.deltaTime * _panSpeed, ForceMode2D.Force);
                }
            }
        }
        else
        {
            RestoreVelocity();
        }
    }


    public void RestoreVelocity()
    {
        rigidBody.velocity = Vector2.zero;
    }

    public void HitIfCollideWall()
    {
        if (transform.position.x >= _limitX) 
        {
            RestoreVelocity();
            rigidBody.AddForce(Vector2.left * Time.deltaTime * 600, ForceMode2D.Impulse);
        }
        else if (transform.position.x <= -_limitX)
        {
            RestoreVelocity();
            rigidBody.AddForce(Vector2.right * Time.deltaTime * 600, ForceMode2D.Impulse);
        }
    }


    public void DoubleSpeed()
    {
        StartCoroutine(DoubleSpeedRoutine());
    }

    private IEnumerator DoubleSpeedRoutine()
    {
        _panSpeed = _chachedPanSpeed;
        _panSpeed *= 2;
        yield return new WaitForSeconds(5);
        _panSpeed = _chachedPanSpeed;
    }
}
