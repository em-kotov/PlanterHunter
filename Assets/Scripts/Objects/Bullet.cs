using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed = 8f;
    private float _turnSpeed = 4f;
    private float _lifetime = 3f;
    // private Vector3 _direction;
    private Vector3 _currentDirection;
    private Vector3 _targetDirection;

    public event Action<Bullet> OnDestroyed;

    private void Start()
    {
        Destroy(gameObject, _lifetime);
    }

    private void Update()
    {
        // transform.position += _direction * _speed * Time.deltaTime;

        // Smoothly rotate current direction towards target
        _currentDirection = Vector3.Lerp(_currentDirection, _targetDirection, _turnSpeed * Time.deltaTime);
        transform.position += _currentDirection * _speed * Time.deltaTime;
    }

    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }

    public void SetDirection(Vector3 direction)
    {
        // When first shot, both current and target are the same
        _currentDirection = direction.normalized;
        _targetDirection = _currentDirection;
    }

    public void UpdateTargetDirection(Vector3 newDirection)
    {
        _targetDirection = newDirection.normalized;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
