using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AuraSlotFiller : MonoBehaviour
{
    [SerializeField] private AuraSlot _auraSlot1;
    [SerializeField] private AuraSlot _auraSlot2;
    [SerializeField] private AuraSlot _auraSlot3;

    private List<AuraSlot> _auraSlots = new List<AuraSlot>();

    private void Start()
    {
        _auraSlots = new List<AuraSlot>{
            _auraSlot1,
            _auraSlot2,
            _auraSlot3
        };
    }

    public void SetAura(Color color, float duration)
    {
        AuraSlot emptySlot = _auraSlots.Find(slot => slot.IsEmpty);

        if (emptySlot != null)
            emptySlot.Initialize(color, duration);
    }

    public float GetActiveAurasCount()
    {
        float count = 0;

        foreach (AuraSlot slot in _auraSlots)
        {
            if (slot.IsEmpty == false)
                count++;
        }

        return count;
    }
}
