using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private FarmerInput _farmerInput;

    public Vector2 MoveDirection { get; private set; }

    public event Action PlantClicked;

    private void Awake()
    {
        _farmerInput = new FarmerInput();

        _farmerInput.Farmer.Plant.performed += OnPlanted;
    }

    private void Update()
    {
        MoveDirection = _farmerInput.Farmer.Move.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        _farmerInput.Enable();
    }

    private void OnDisable()
    {
        _farmerInput.Disable();
    }

    private void OnDestroy()
    {
        _farmerInput.Dispose();
    }

    private void OnPlanted(InputAction.CallbackContext context)
    {
        PlantClicked?.Invoke();
    }
}
