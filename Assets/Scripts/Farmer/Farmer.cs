using System;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : MonoBehaviour
{
    // [SerializeField] private InputReader _inputReader;
    // [SerializeField] private Planter _planter;
    // [SerializeField] private Harvest _harvest;
    // [SerializeField] private Hunter _hunter;
    // [SerializeField] private AuraSlotFiller _auraSlotFiller;
    // [SerializeField] private Movement _movement;

    // private Vector3 _startPosition = new Vector3(0f, 0f, 0f);
    // private bool _isCuttingHarvest = false;

    // //shooting
    // private bool _hasAura = false;

    //health
    private float _health = 3f;

    public event Action<Vector3> Died;
    public event Action Hit;

    // private void Start()
    // {
    //     transform.position = _startPosition;
    //     // _hunter.SetActive(false);
    // }

    // private void Update()
    // {
    //     // CheckHealth();
    //     // _movement.Move(_inputReader.MoveDirection);

    //     // //shoot 
    //     // Shoot();

    //     // // harvest
    //     // if (_isCuttingHarvest)
    //     // {
    //     //     _harvest.CutHarvest();
    //     // }

    //     // // Update collection movement
    //     // _harvest.UpdatePlantCollection();
    // }

    // public void OnPlantClicked()
    // {
    //     if (_inputReader.CurrentMode != PlayerMode.Planting)
    //         return;

    //     _planter.PlantCabbage(transform.position);
    // }

    // public void OnCollectStarted()
    // {
    //     _isCuttingHarvest = true;
    // }

    // public void OnCollectReleased()
    // {
    //     _isCuttingHarvest = false;
    // }

    // public void OnModeSwitched()
    // {
    //     if (_inputReader.CurrentMode != PlayerMode.Planting)
    //     {
    //         _planter.SetActive(false);
    //         _hunter.SetActive(true);
    //         return;
    //     }

    //     _hunter.SetActive(false);
    //     _planter.SetActive(true);
    // }

    // public void OnAuraFound(Aura aura)
    // {
    //    Debug.Log(aura.Duration);
    //     _auraSlotFiller.SetAura(aura.GetColor(), aura.Duration);
    // }

    // #region Shooting
    // private void Shoot()
    // {
    //     if (_inputReader.CurrentMode != PlayerMode.Shooting && _hasAura == false)
    //         return;

    //     _hunter.Shoot();
    // }
    // #endregion


    #region Health
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
    #endregion
}