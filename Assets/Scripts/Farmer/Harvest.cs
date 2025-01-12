using System.Collections.Generic;
using UnityEngine;

public class Harvest : MonoBehaviour
{
    private float _collectDistance = 2.5f;
    private float _collectSpeed = 5f;
    private List<PlantableObject> _plantsBeingCollected = new List<PlantableObject>();

    public void CutHarvest()
    {
        int plantLayerIndex = 10;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _collectDistance, LayerMaskConverter.GetLayerMask(plantLayerIndex));

        foreach (var collider in colliders)
        {
            PlantableObject plant = collider.gameObject.GetComponent<PlantableObject>();

            if (plant != null && plant.IsReadyToCut() && !_plantsBeingCollected.Contains(plant))
            {
                StartCollectingPlant(plant);
            }
        }
    }

    public void UpdatePlantCollection()
    {
        for (int i = _plantsBeingCollected.Count - 1; i >= 0; i--)
        {
            PlantableObject plant = _plantsBeingCollected[i];
            if (plant == null)
            {
                _plantsBeingCollected.RemoveAt(i);
                continue;
            }

            // Move plant towards player
            Vector3 directionToPlayer = transform.position - plant.transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;

            if (distanceToPlayer < 0.5f) // Collection threshold
            {
                CollectPlant(plant);
                _plantsBeingCollected.RemoveAt(i);
            }
            else
            {
                // Smooth movement towards player
                plant.transform.position = Vector3.MoveTowards(
                    plant.transform.position,
                    transform.position,
                    _collectSpeed * Time.deltaTime
                );
            }
        }
    }

    private void StartCollectingPlant(PlantableObject plant)
    {
        // Disable physics on the plant
        if (plant.TryGetComponent<Rigidbody2D>(out var rb))
        {
            rb.simulated = false;
        }

        plant.SetCut();
        _plantsBeingCollected.Add(plant);
    }

    private void CollectPlant(PlantableObject plant)
    {
        // Add any collection effects here
        plant.Deactivate();
    }
}
