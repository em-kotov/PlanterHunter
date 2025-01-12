using System.Collections.Generic;
using UnityEngine;

public class AuraPicker : MonoBehaviour
{
    [SerializeField] private AuraSlot _auraSlot1;
    [SerializeField] private AuraSlot _auraSlot2;
    [SerializeField] private AuraSlot _auraSlot3;

    private List<AuraSlot> _auraSlots;
    private float _shiftDelay = 0.2f;
    private float _nextShiftTime;
    private bool _isShiftingInProgress = false;
    private float _auraSeconds = 3f;

    private void Start()
    {
        _auraSlots = new List<AuraSlot>{
            _auraSlot1,
            _auraSlot2,
            _auraSlot3
        };

        _nextShiftTime = Time.time + _shiftDelay;
    }

    private void Update()
    {
        if (NeedsShifting())
        {
            if (_isShiftingInProgress == false)
            {
                _isShiftingInProgress = true;
                _nextShiftTime = Time.time + _shiftDelay;
            }
            else if (Time.time >= _nextShiftTime)
            {
                ShiftAuras();
                _isShiftingInProgress = false;
            }
        }
        else
        {
            _isShiftingInProgress = false;
        }
    }

    public void SetAura(Aura aura)
    {
        // Find first empty slot
        var emptySlot = _auraSlots.Find(slot => slot.IsEmpty);

        if (emptySlot != null)
        {
            emptySlot.Initialize(aura.GetColor(), _auraSeconds);
        }
    }

    private bool NeedsShifting()
    {
        // Check if any slot is empty while a higher slot is filled
        for (int i = 0; i < _auraSlots.Count - 1; i++)
        {
            if (_auraSlots[i].IsEmpty && _auraSlots[i + 1].IsEmpty == false)
            {
                return true;
            }
        }

        return false;
    }

    private void ShiftAuras()
    {
        // Shift auras down to fill empty slots
        for (int i = 0; i < _auraSlots.Count - 1; i++)
        {
            if (_auraSlots[i].IsEmpty && _auraSlots[i + 1].IsEmpty == false)
            {
                _auraSlots[i].TransferFrom(_auraSlots[i + 1]);
            }
        }
    }
}
