using UnityEngine;

public class Movement : MonoBehaviour
{
    private FarmerInput _farmerInput;
    private Vector3 _startPosition = new Vector3(0f, 0f, 0f);
    private Vector2 _moveDirection;

    private Vector2 _smoothedDirection;
    private float _moveSpeed = 7f;
    private float _deadzone = 0.1f;
    private float _smoothing = 0.1f;

    private void Awake()
    {
        _farmerInput = new FarmerInput();
    }

    private void Start()
    {
        transform.position = _startPosition;
    }

    private void Update()
    {
        Move(_farmerInput.Farmer.Move.ReadValue<Vector2>());
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

    public void Move(Vector2 direction)
    {
        _moveDirection = direction;

        // Apply smoothing
        _smoothedDirection = Vector2.Lerp(_smoothedDirection, _moveDirection, 1f / _smoothing);

        // More reliable deadzone check
        if (Mathf.Abs(_smoothedDirection.x) < _deadzone)
        {
            _smoothedDirection.x = 0f;
        }
        if (Mathf.Abs(_smoothedDirection.y) < _deadzone)
        {
            _smoothedDirection.y = 0f;
        }

        // Only move if we have actual input
        if (_smoothedDirection != Vector2.zero)
        {
            float scaledMoveSpeed = _moveSpeed * Time.deltaTime;
            Vector3 offset = new Vector3(_smoothedDirection.x, _smoothedDirection.y, 0f) * scaledMoveSpeed;
            transform.Translate(offset);
        }
    }
}