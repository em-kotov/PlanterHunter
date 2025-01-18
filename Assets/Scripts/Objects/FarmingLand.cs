using UnityEngine;

public class FarmingLand : MonoBehaviour
{
    [SerializeField] private PlantableObject _plantPrefab;
    [SerializeField] private Aura _auraPrefab;

    private PlantableObject _currentPlant = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_currentPlant != null)
        {
            TrySpawnAura();
            return;
        }

        SpawnPlant();
    }

    private void TrySpawnAura()
    {
        if (_currentPlant.IsReadyToCut() == false)
            return;

        _currentPlant.gameObject.SetActive(false);
        _currentPlant = null;
        Aura aura = Instantiate(_auraPrefab, transform.position, Quaternion.identity);
        aura.WasCollected += SpawnPlant;
    }

    private void SpawnPlant()
    {
        _currentPlant = Instantiate(_plantPrefab, transform.position, Quaternion.identity);
    }
}
