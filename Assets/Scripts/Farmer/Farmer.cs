using System;
using UnityEngine;

public class Farmer : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Planter _planter;
    [SerializeField] private Movement _movement;

    private Vector3 _startPosition = new Vector3(0f, 0f, 0f);
    private float _health = 3f;

    public event Action<Vector3> Died;
    public event Action Hit;

    private void Start()
    {
        transform.position = _startPosition;
    }

    private void Update()
    {
        CheckHealth();
        _movement.Move(_inputReader.MoveDirection);
    }

    public void OnPlantClicked()
    {
        if (_inputReader.CurrentMode != PlayerMode.Planting)
            return;

        _planter.PlantCabbage(transform.position);
    }

    public void OnModeSwitched()
    {
        if (_inputReader.CurrentMode != PlayerMode.Planting)
        {
            _planter.enabled = false;
            return;
        }

        _planter.enabled = true;
    }

    private void Deactivate()
    {
        Died?.Invoke(transform.position);
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;
        gameObject.SetActive(false);
        Debug.Log("you died");
    }

    private void LooseHealth(float points)
    {
        Hit?.Invoke();
        _health -= points;
    }

    private void CheckHealth()
    {
        if (_health <= 0)
        {
            Deactivate();
        }
    }
}