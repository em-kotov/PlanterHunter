using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform _player;
    private float _speed = 2f;
    private float _chaseDistance = 13f;
    private float _detectRadius = 13f;
    private bool _canChase = false;

    private void Update()
    {
        DetectPlayer();

        if (!_canChase)
            return;

        ChasePlayer();
        UpdateTarget();
    }

    private void DetectPlayer()
    {
        int playerLayerIndex = 8;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _detectRadius, LayerMaskConverter.GetLayerMask(playerLayerIndex));

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out Farmer player))
            {
                _player = player.transform;
                _canChase = true;
            }
        }
    }

    private void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
    }

    private void UpdateTarget()
    {
        if (Vector3Extensions.IsEnoughClose(transform.position, _player.position, _chaseDistance) == false)
        {
            _player = null;
            _canChase = false;
        }
    }
}
