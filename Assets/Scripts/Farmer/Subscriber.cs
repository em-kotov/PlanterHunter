using UnityEngine;

public class Subscriber : MonoBehaviour
{
    [SerializeField] private Farmer _farmer;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private CollisionRegister _collisionRegister;
    [SerializeField] private Planter _planter;

    private void OnEnable()
    {
        // _inputReader.ModeSwitched += _farmer.OnModeSwitched;
        // _inputReader.PlantClicked += _farmer.OnPlantClicked;
        // _inputReader.CollectStarted += _farmer.OnCollectStarted;
        // _inputReader.CollectReleased += _farmer.OnCollectReleased;

        // _collisionRegister.AuraFound += _farmer.OnAuraFound;
    }

    private void OnDisable()
    {
        // _inputReader.ModeSwitched -= _farmer.OnModeSwitched;
        // _inputReader.PlantClicked -= _farmer.OnPlantClicked;
        // _inputReader.CollectStarted -= _farmer.OnCollectStarted;
        // _inputReader.CollectReleased -= _farmer.OnCollectReleased;

        // _collisionRegister.AuraFound -= _farmer.OnAuraFound;
    }
}