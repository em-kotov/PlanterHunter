using System.Collections.Generic;
using UnityEngine;

public class Planter : MonoBehaviour
{
    [SerializeField] private GameObject _cabbagePrefab;

    public void PlantCabbage(Vector3 position)
    {
        GameObject cabbage = Instantiate(_cabbagePrefab, position, Quaternion.identity);
    }
}
