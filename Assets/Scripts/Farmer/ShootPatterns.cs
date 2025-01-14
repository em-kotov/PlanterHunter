using UnityEngine;

public class ShootPatterns : MonoBehaviour
{
    private float _circleSpawnRadius = 2f;

    public void ShootNormalPattern(Vector3 position, Bullet bulletPrefab,
            Color? bulletColor, int activeSlots, System.Action<Bullet> onBulletSpawned)
    {
        switch (activeSlots)
        {
            case 1:
                SpawnBullet(position, Vector3.right, bulletPrefab, bulletColor, onBulletSpawned);
                break;
            case 2:
                SpawnBullet(position, Quaternion.Euler(0, 0, -15) * Vector3.right, bulletPrefab, bulletColor, onBulletSpawned);
                SpawnBullet(position, Quaternion.Euler(0, 0, 15) * Vector3.right, bulletPrefab, bulletColor, onBulletSpawned);
                break;
            case 3:
                SpawnBullet(position, Quaternion.Euler(0, 0, -30) * Vector3.right, bulletPrefab, bulletColor, onBulletSpawned);
                SpawnBullet(position, Vector3.right, bulletPrefab, bulletColor, onBulletSpawned);
                SpawnBullet(position, Quaternion.Euler(0, 0, 30) * Vector3.right, bulletPrefab, bulletColor, onBulletSpawned);
                break;
        }
    }

    public void ShootCirclePattern(Vector3 position, Bullet bulletPrefab,
            Color comboColor, System.Action<Bullet> onBulletSpawned)
    {
        for (int i = 0; i < 8; i++)
        {
            float angle = i * 45f;
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.right;
            Vector3 spawnOffset = direction * _circleSpawnRadius;

            Bullet bullet = SpawnBullet(position + spawnOffset, direction, bulletPrefab, comboColor, onBulletSpawned);
            bullet.transform.localScale *= 1.5f;
        }
    }

    private Bullet SpawnBullet(Vector3 position, Vector3 direction, Bullet bulletPrefab,
                Color? bulletColor, System.Action<Bullet> onBulletSpawned)
    {
        Bullet bullet = Instantiate(bulletPrefab, position, Quaternion.identity);
        bullet.SetDirection(direction);

        if (bulletColor.HasValue)
        {
            bullet.GetComponent<SpriteRenderer>().color = bulletColor.Value;
        }

        onBulletSpawned?.Invoke(bullet);
        return bullet;
    }
}
