using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private FarmerInput _farmerInput;
    private PlayerMode _currentMode = PlayerMode.Shooting;
    private bool _isCollectHeld = false;

    public PlayerMode CurrentMode => _currentMode;
    public Vector2 MoveDirection { get; private set; }

    public event Action ModeSwitched;
    public event Action PlantClicked;
    public event Action CollectStarted;
    public event Action CollectReleased;

    private void Awake()
    {
        _farmerInput = new FarmerInput();

        _farmerInput.Farmer.SwitchMode.performed += OnModeSwitched;
        _farmerInput.Farmer.Plant.performed += OnPlanted;
        _farmerInput.Farmer.CollectHarvest.performed += OnCollectPerformed;
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

    private void OnCollectPerformed(InputAction.CallbackContext context)
    {
        if(_isCollectHeld == false)
        {
            _isCollectHeld = true;
            CollectStarted?.Invoke();
            return;
        }

        _isCollectHeld = false;
        CollectReleased?.Invoke();
    }

    private void OnModeSwitched(InputAction.CallbackContext context)
    {
        Vector2 scrollValue = context.ReadValue<Vector2>();
        float scrollY = scrollValue.y;

        // Get total number of modes
        int modesCount = System.Enum.GetValues(typeof(PlayerMode)).Length;

        // Get current mode as integer
        int currentIndex = (int)_currentMode;

        if (scrollY > 0)
        {
            // Scroll up, increment and wrap around
            currentIndex = (currentIndex + 1) % modesCount;
        }
        else if (scrollY < 0)
        {
            // Scroll down, decrement and wrap around
            currentIndex = (currentIndex - 1 + modesCount) % modesCount;
        }

        _currentMode = (PlayerMode)currentIndex;
        ModeSwitched?.Invoke();
    }
}
