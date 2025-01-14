using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    //continue refactor in shootPatterns

    [SerializeField] private AuraSlotFiller _auraManager;
    [SerializeField] private EnemyDetector _enemyDetector;
    [SerializeField] private ShootPatterns _shootPatterns;


    [Header("Visual Feedback")]
    [SerializeField] private SpriteRenderer _hatSprite;
    [SerializeField] private Bullet _bulletPrefab;

    private float _shootCooldown = 0.5f;
    private float _nextShootTime;
    private bool _isActive;
    private List<Bullet> _activeBullets = new List<Bullet>();

    private void Update()
    {
        // if (_isActive == false || _auraManager.HasActiveAuras == false)
        //     return;

        // Get direction to nearest enemy
        if (_enemyDetector.TryGetEnemyDirection(transform.position, out Vector3 enemyDirection))
        {
            // Update all active bullets
            foreach (var bullet in _activeBullets)
            {
                bullet.UpdateTargetDirection(enemyDirection);
            }
        }

        if (Time.time >= _nextShootTime)
        {
            Shoot();
            _nextShootTime = Time.time + _shootCooldown;
        }
    }

    public void SetActive(bool isActive)
    {
        _isActive = isActive;
        _hatSprite.enabled = isActive;
        // enabled = isActive;
        // if (_isActive == false)
        //     _nextShootTime = 0f;
    }

    private void Shoot()
    {
        // if (_auraManager.HasTripleCombo())
        // {
        //     // ShootCirclePattern();
        // }
        // else
        // {
        //     // ShootNormalPattern();
        // }
    }

    // private void ShootNormalPattern()
    // {
    //     switch (_auraManager.ActiveSlotsCount)
    //     {
    //         case 1:
    //             SpawnBullet(Vector3.right);
    //             break;
    //         case 2:
    //             SpawnBullet(Quaternion.Euler(0, 0, -15) * Vector3.right);
    //             SpawnBullet(Quaternion.Euler(0, 0, 15) * Vector3.right);
    //             break;
    //         case 3:
    //             SpawnBullet(Quaternion.Euler(0, 0, -30) * Vector3.right);
    //             SpawnBullet(Vector3.right);
    //             SpawnBullet(Quaternion.Euler(0, 0, 30) * Vector3.right);
    //             break;
    //     }
    // }

    // private void ShootCirclePattern()
    // {
    //     Color? comboColor = _auraManager.GetFirstActiveColor();

    //     if (!comboColor.HasValue)
    //         return;

    //         float spawnRadius = 2f;

    //     for (int i = 0; i < 8; i++)
    //     {
    //         float angle = i * 45f;
    //         Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.right;
    //         Vector3 spawnOffset = direction * spawnRadius;

    //         Bullet bullet = Instantiate(_bulletPrefab, transform.position + spawnOffset, Quaternion.identity);
    //         bullet.SetDirection(direction);
    //         bullet.OnDestroyed += RemoveBullet; // Subscribe to bullet destruction
    //         _activeBullets.Add(bullet);

    //         var renderer = bullet.GetComponent<SpriteRenderer>();
    //         renderer.color = comboColor.Value;
    //         bullet.transform.localScale *= 1.5f;
    //     }
    // }

    // private void SpawnBullet(Vector3 direction)
    // {
    //     // Vector3 direction;
    //     // TryGetEnemyDirection(out direction);

    //     Bullet bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
    //     bullet.SetDirection(direction);
    //     bullet.OnDestroyed += RemoveBullet; // Subscribe to bullet destruction
    //     _activeBullets.Add(bullet);

    //     Color? bulletColor = _auraManager.GetFirstActiveColor();

    //     if (bulletColor.HasValue)
    //     {
    //         var renderer = bullet.GetComponent<SpriteRenderer>();
    //         renderer.color = bulletColor.Value;
    //     }
    // }

    private void RemoveBullet(Bullet bullet)
    {
        _activeBullets.Remove(bullet);
    }
}
