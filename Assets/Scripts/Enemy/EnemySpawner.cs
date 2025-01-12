using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Farmer _player;

    [Header("Spawn Settings")]
    private float _minDistanceFromPlayer = 30f;
    private Vector2 _gridSize = new Vector2(66f, 56f);
    private float _cellSize = 1f;
    private int _regularCount = 8;
    private int _countToSpawn;
    private List<Enemy> _currentEnemies = new List<Enemy>();

    private void Update()
    {
        if (_currentEnemies.Count >= _regularCount)
            return;

        _countToSpawn = _regularCount - _currentEnemies.Count;
        Spawn(_countToSpawn);
    }

    public void Spawn(int count)
    {
        List<Vector2> availablePositions = GetAvailableSpawnPositions();

        for (int i = 0; i < count && availablePositions.Count > 0; i++)
        {
            // Get random position from pre-calculated available positions
            int randomIndex = UnityEngine.Random.Range(0, availablePositions.Count);

            Vector3 spawnPosition = new Vector3(
                availablePositions[randomIndex].x,
                availablePositions[randomIndex].y,
                0f
            );

            CreateEnemy(spawnPosition);
            availablePositions.RemoveAt(randomIndex);
        }
    }

    private void CreateEnemy(Vector3 position)
    {
        Enemy enemy;
        enemy = Instantiate(_enemyPrefab, position, Quaternion.identity);

        // enemy.Died += OnDeath;
        // enemy.Hit += OnHit;
        // enemy.Initialize(color, _alpha, _hero);
        _currentEnemies.Add(enemy);
    }

    private List<Vector2> GetAvailableSpawnPositions()
    {
        List<Vector2> positions = new List<Vector2>();
        Vector2 playerPosition = new Vector2(_player.transform.position.x, _player.transform.position.y);
        float minDistanceSqr = _minDistanceFromPlayer * _minDistanceFromPlayer;

        // Calculate grid boundaries
        int columnsCount = Mathf.CeilToInt(_gridSize.x / _cellSize);
        int rowsCount = Mathf.CeilToInt(_gridSize.y / _cellSize);

        // Start from center and expand outwards
        for (int x = -columnsCount / 2; x < columnsCount / 2; x++)
        {
            for (int y = -rowsCount / 2; y < rowsCount / 2; y++)
            {
                Vector2 position = new Vector2(x * _cellSize, y * _cellSize);

                // Check if position is far enough from hero
                if ((position - playerPosition).sqrMagnitude >= minDistanceSqr)
                {
                    positions.Add(position);
                }
            }
        }

        return positions;
    }
}
