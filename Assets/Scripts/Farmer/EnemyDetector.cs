using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public bool TryGetEnemyDirection(Vector3 position, out Vector3 direction)
    {
        direction = Vector3.right;
        Enemy enemy = GetClosestEnemy(10, position);

        if (enemy != null)
        {
            direction = (enemy.transform.position - position).normalized;
            return true;
        }

        return false;
    }

    private Enemy GetClosestEnemy(float radius, Vector3 position)
    {
        float sqrDistance;
        float sqrClosestDistance = 50f * 50f;
        int enemyLayerIndex = 11;
        Enemy closestEnemy = null;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radius,
                                        LayerMaskConverter.GetLayerMask(enemyLayerIndex));

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out Enemy enemy))
            {
                sqrDistance = Vector3Extensions.GetSqrDistance(position, enemy.transform.position);

                if (sqrDistance <= sqrClosestDistance)
                {
                    sqrClosestDistance = sqrDistance;
                    closestEnemy = enemy;
                }
            }
        }

        return closestEnemy;
    }
}
