using System;
using UnityEngine;

public class Farmer : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Planter _planter;
    [SerializeField] private Movement _movement;
    [SerializeField] private Vector3 _startPosition = new Vector3(0f, 4f, 7f);

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
        _planter.PlantCabbage(transform.position);
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