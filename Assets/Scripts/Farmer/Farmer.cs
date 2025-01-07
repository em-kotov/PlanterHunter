using System;
using UnityEngine;

public class Farmer : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Planter _planter;
    [SerializeField] private Hunter _hunter;
    [SerializeField] private Movement _movement;

    private Vector3 _startPosition = new Vector3(0f, 0f, 0f);
    private float _health = 3f;
    private bool _isCuttingHarvest = false;

    public event Action<Vector3> Died;
    public event Action Hit;

    private void Start()
    {
        transform.position = _startPosition;
        _hunter.SetActive(false);
    }

    private void Update()
    {
        CheckHealth();
        _movement.Move(_inputReader.MoveDirection);

        CutHarvest();
    }

    public void OnPlantClicked()
    {
        if (_inputReader.CurrentMode != PlayerMode.Planting)
            return;

        _planter.PlantCabbage(transform.position);
    }

    public void OnCollectStarted()
    {
        _isCuttingHarvest = true;
    }

    public void OnCollectReleased()
    {
        _isCuttingHarvest = false;
    }

    public void OnPlantFound(PlantableObject plant)
    {
        if (plant.IsCut)
        {
            plant.Deactivate();
        }
    }

    public void OnModeSwitched()
    {
        if (_inputReader.CurrentMode != PlayerMode.Planting)
        {
            _planter.SetActive(false);
            _hunter.SetActive(true);
            return;
        }

        _hunter.SetActive(false);
        _planter.SetActive(true);
    }

    private void CutHarvest()
    {
        if (_isCuttingHarvest)
        {
            int plantLayerIndex = 10;
            float collectDistance = 2.5f;
            float collectForce = 5f;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, collectDistance, LayerMaskConverter.GetLayerMask(plantLayerIndex));

            for (int i = 0; i < colliders.Length; i++)
            {
                PlantableObject plant = colliders[i].gameObject.GetComponent<PlantableObject>();

                if (plant.IsReadyToCut())
                {
                    colliders[i].attachedRigidbody.AddForce(
                        ((transform.position - plant.transform.position) * collectForce).normalized,
                        ForceMode2D.Impulse);
                    plant.SetCut();
                }
            }

            Debug.Log("Cutting Harvest.");
        }
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