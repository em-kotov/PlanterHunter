using UnityEngine;

public class Planter : MonoBehaviour
{
    [SerializeField] private GameObject _cabbagePrefab;

    private float _gridSize = 1f;
    private Vector2 _areaMin = new Vector2(-5f, -5f);
    private Vector2 _areaMax = new Vector2(5f, 5f);

    [Header("Visual Feedback")]
    [SerializeField] private SpriteRenderer _plantingIndicator;
    [SerializeField] private SpriteRenderer _hatSprite;

    // Optional: Visual feedback for planting position
    private void Update()
    {
        if (_plantingIndicator != null)
        {
            Vector3 gridPos = GetGridPosition(transform.position);
            _plantingIndicator.transform.position = gridPos;

            Color indicatorColor = Color.red;

            if (IsWithinPlantableArea(gridPos))
            {
                indicatorColor = IsSpotTaken(gridPos) ? Color.red : Color.green;
            }

            _plantingIndicator.color = indicatorColor;
        }
    }

    public void SetActive(bool isActive)
    {
        _plantingIndicator.enabled = isActive;
        _hatSprite.enabled = isActive;
        enabled = isActive;
    }

    public void PlantCabbage(Vector3 position)
    {
        Vector3 gridPosition = GetGridPosition(position);

        if (!IsWithinPlantableArea(gridPosition) || IsSpotTaken(gridPosition))
            return;

        GameObject cabbage = Instantiate(_cabbagePrefab, gridPosition, Quaternion.identity);
    }

    private Vector3 GetGridPosition(Vector3 playerPosition)
    {
        float x = Mathf.Round(playerPosition.x / _gridSize) * _gridSize;
        float y = Mathf.Round(playerPosition.y / _gridSize) * _gridSize;
        return new Vector3(x, y, 0);
    }

    private bool IsSpotTaken(Vector3 position)
    {
        int plantLayerIndex = 10;
        float plantCheckRadius = 0.1f;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, plantCheckRadius, LayerMaskConverter.GetLayerMask(plantLayerIndex));
        return colliders.Length > 0;
    }

    private bool IsWithinPlantableArea(Vector3 position)
    {
        return position.x >= _areaMin.x && position.x <= _areaMax.x &&
               position.y >= _areaMin.y && position.y <= _areaMax.y;
    }
}
