using UnityEngine;

public class Subscriber : MonoBehaviour
{
    [SerializeField] private Farmer _farmer;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private CollisionRegister _collisionRegister;
    [SerializeField] private Planter _planter;

    private void OnEnable()
    {
        _inputReader.PlantClicked += _farmer.OnPlantClicked;
    }

    private void OnDisable()
    {
        _inputReader.PlantClicked -= _farmer.OnPlantClicked;
    }
}