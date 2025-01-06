using UnityEngine;
using UnityEngine.InputSystem;

public class ModeController : MonoBehaviour
{
    // private PlayerMode _currentMode = PlayerMode.Shooting;
    // public PlayerMode CurrentMode => _currentMode;
    // private FarmerInput _farmerInput;

    // private void Awake()
    // {
    //     _farmerInput = new FarmerInput();
    //     _farmerInput.Farmer.SwitchMode.performed += OnModeSwitched;
    // }

    // private void OnModeSwitched(InputAction.CallbackContext context)
    // {
    //     float scrollValue = context.ReadValue<float>();

    //     // Get total number of modes
    //     int modesCount = System.Enum.GetValues(typeof(PlayerMode)).Length;

    //     // Get current mode as integer
    //     int currentIndex = (int)_currentMode;

    //     if (scrollValue > 0)
    //     {
    //         // Scroll up, increment and wrap around
    //         currentIndex = (currentIndex + 1) % modesCount;
    //     }
    //     else if (scrollValue < 0)
    //     {
    //         // Scroll down, decrement and wrap around
    //         currentIndex = (currentIndex - 1 + modesCount) % modesCount;
    //     }

    //     _currentMode = (PlayerMode)currentIndex;
    //     Debug.Log($"Switched to {_currentMode} mode");
    // }

    // private void OnEnable()
    // {
    //     _farmerInput.Enable();
    // }

    // private void OnDisable()
    // {
    //     _farmerInput.Disable();
    // }

    // private void OnDestroy()
    // {
    //     _farmerInput.Dispose();
    // }

}
